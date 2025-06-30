using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Web.Models;
using MyPortfolio.Web.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolio.Web.Controllers
{
    [Authorize]
    public class EducationAdminController : Controller
    {
        private readonly MyPortfolioDbContext _context;

        public EducationAdminController(MyPortfolioDbContext context)
        {
            _context = context;
        }

        // GET: EducationAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Educations.ToListAsync());
        }

        // GET: EducationAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EducationAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,School_tr,School_en,Department_tr,Department_en,DateRange_tr,DateRange_en,Description_tr,Description_en")] Education education)
        {
            if (ModelState.IsValid)
            {
                _context.Add(education);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(education);
        }

        // GET: EducationAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.Educations.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            }
            return View(education);
        }

        // POST: EducationAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,School_tr,School_en,Department_tr,Department_en,DateRange_tr,DateRange_en,Description_tr,Description_en")] Education education)
        {
            if (id != education.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(education);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationExists(education.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(education);
        }

        // GET: EducationAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.Educations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // POST: EducationAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationExists(int id)
        {
            return _context.Educations.Any(e => e.Id == id);
        }
    }
} 