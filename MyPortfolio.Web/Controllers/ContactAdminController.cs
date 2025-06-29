using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Web.Models;
using System.Threading.Tasks;

namespace MyPortfolio.Web.Controllers
{
    [Authorize]
    public class ContactAdminController : Controller
    {
        private readonly MyPortfolioDbContext _context;

        public ContactAdminController(MyPortfolioDbContext context)
        {
            _context = context;
        }

        // GET: ContactAdmin
        public async Task<IActionResult> Index()
        {
            var messages = await _context.ContactMessages.ToListAsync();
            return View(messages);
        }
    }
} 