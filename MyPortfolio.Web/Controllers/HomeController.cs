using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Web.Models;
using MyPortfolio.Web.Models.Entities;
using MyPortfolio.Web.Models.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace MyPortfolio.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyPortfolioDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public HomeController(MyPortfolioDbContext context, ILogger<HomeController> logger, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _logger = logger;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var viewModel = new HomePageViewModel
            {
                Abouts = _context.Abouts.ToList(),
                Experiences = _context.Experiences.Include(e => e.ExperienceResponsibilities).ToList(),
                Educations = _context.Educations.ToList(),
                Skills = _context.Skills.ToList(),
                Projects = _context.Projects.ToList(),
                Certificates = _context.Certificates.OrderBy(c => c.DisplayOrder).ToList(),
                ContactMessage = new ContactMessage()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMessage contactForm)
        {
            _logger.LogInformation("Contact POST method invoked with Turnstile verification.");

            // 1. Turnstile Verification
            var turnstileResponse = Request.Form["cf-turnstile-response"];
            var turnstileSecret = Environment.GetEnvironmentVariable("TURNSTILE_SECRET_KEY");

            if (string.IsNullOrEmpty(turnstileResponse))
            {
                _logger.LogWarning("Turnstile token is missing.");
                TempData["ErrorMessage"] = "Güvenlik doğrulaması yapılamadı.";
                return Redirect(Url.Action("Index", "Home") + "#iletisim");
            }

            using (var client = new HttpClient())
            {
                var verifyResult = await client.PostAsync("https://challenges.cloudflare.com/turnstile/v0/siteverify", 
                    new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "secret", turnstileSecret ?? "" },
                        { "response", turnstileResponse! }
                    }));

                var jsonResponse = await verifyResult.Content.ReadAsStringAsync();
                var resultDoc = JsonDocument.Parse(jsonResponse);
                bool success = resultDoc.RootElement.GetProperty("success").GetBoolean();

                if (!success)
                {
                    _logger.LogWarning("Turnstile verification failed: {Response}", jsonResponse);
                    TempData["ErrorMessage"] = "Bot doğrulaması başarısız oldu.";
                    return Redirect(Url.Action("Index", "Home") + "#iletisim");
                }
            }

            // 2. Standard Validation & Save
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Model state is valid.");
                try
                {
                    contactForm.SentDate = DateTime.Now;
                    _context.ContactMessages.Add(contactForm);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = _localizer["ContactSuccess"].Value;
                    return Redirect(Url.Action("Index", "Home") + "#iletisim");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving contact message to database.");
                    TempData["ErrorMessage"] = _localizer["ContactError"].Value;
                    return Redirect(Url.Action("Index", "Home") + "#iletisim");
                }
            }
            
            _logger.LogWarning("Model state is invalid. Re-rendering Index view.");

            var viewModel = new HomePageViewModel
            {
                Abouts = _context.Abouts.ToList(),
                Experiences = _context.Experiences.Include(e => e.ExperienceResponsibilities).ToList(),
                Educations = _context.Educations.ToList(),
                Skills = _context.Skills.ToList(),
                Projects = _context.Projects.ToList(),
                Certificates = _context.Certificates.OrderBy(c => c.DisplayOrder).ToList(),
                ContactMessage = contactForm 
            };

            return View("Index", viewModel);
        }

        private static readonly string[] _supportedCultures = ["tr-TR", "en-US"];

        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(culture) || !_supportedCultures.Contains(culture))
                    return BadRequest();

                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddYears(1),
                        HttpOnly = true,
                        SameSite = SameSiteMode.Lax,
                        Secure = false  // HTTP ortamı için false
                    }
                );

                return LocalRedirect(returnUrl ?? "/");
            }
            catch
            {
                return Redirect("/");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
