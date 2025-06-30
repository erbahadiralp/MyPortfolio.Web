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

namespace MyPortfolio.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyPortfolioDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(MyPortfolioDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
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
                ContactMessage = new ContactMessage()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactMessage contactForm)
        {
            _logger.LogInformation("Contact POST method invoked with standard model binding.");

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Model state is valid.");
                try
                {
                    contactForm.SentDate = DateTime.Now;
                    _context.ContactMessages.Add(contactForm);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi! En kısa sürede size dönüş yapacağım.";
                    return Redirect(Url.Action("Index", "Home") + "#iletisim");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving contact message to database.");
                    TempData["ErrorMessage"] = "Mesajınız gönderilirken bir veritabanı hatası oluştu.";
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
                ContactMessage = contactForm 
            };

            return View("Index", viewModel);
        }

        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
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
