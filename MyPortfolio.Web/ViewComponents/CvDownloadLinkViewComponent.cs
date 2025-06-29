using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolio.Web.ViewComponents
{
    public class CvDownloadLinkViewComponent : ViewComponent
    {
        private readonly MyPortfolioDbContext _context;

        public CvDownloadLinkViewComponent(MyPortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var about = await _context.Abouts.FirstOrDefaultAsync();
            return View(about);
        }
    }
} 