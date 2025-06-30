using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Web.Models;
using MyPortfolio.Web.Models.Entities;
using MyPortfolio.Web.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolio.Web.Controllers
{
    [Authorize]
    public class ExperienceAdminController : Controller
    {
        private readonly MyPortfolioDbContext _context;

        public ExperienceAdminController(MyPortfolioDbContext context)
        {
            _context = context;
        }

        // GET: ExperienceAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Experiences.ToListAsync());
        }

        // GET: ExperienceAdmin/Create
        public IActionResult Create()
        {
            var viewModel = new ExperienceViewModel
            {
                Responsibilities = new() { new() } // Start with one empty responsibility
            };
            return View(viewModel);
        }

        // POST: ExperienceAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExperienceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var experience = new Experience
                {
                    Title_tr = viewModel.Title_tr,
                    Title_en = viewModel.Title_en,
                    Company_tr = viewModel.Company_tr,
                    Company_en = viewModel.Company_en,
                    Location_tr = viewModel.Location_tr,
                    Location_en = viewModel.Location_en,
                    DateRange_tr = viewModel.DateRange_tr,
                    DateRange_en = viewModel.DateRange_en,
                    ExperienceResponsibilities = viewModel.Responsibilities.Select(r => new ExperienceResponsibility
                    {
                        Description_tr = r.Description_tr,
                        Description_en = r.Description_en
                    }).ToList()
                };

                _context.Experiences.Add(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: ExperienceAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences
                .Include(e => e.ExperienceResponsibilities)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (experience == null)
            {
                return NotFound();
            }

            var viewModel = new ExperienceViewModel
            {
                Id = experience.Id,
                Title_tr = experience.Title_tr,
                Title_en = experience.Title_en,
                Company_tr = experience.Company_tr,
                Company_en = experience.Company_en,
                Location_tr = experience.Location_tr,
                Location_en = experience.Location_en,
                DateRange_tr = experience.DateRange_tr,
                DateRange_en = experience.DateRange_en,
                Responsibilities = experience.ExperienceResponsibilities.Select(r => new ExperienceResponsibilityViewModel
                {
                    Id = r.Id,
                    Description_tr = r.Description_tr,
                    Description_en = r.Description_en
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: ExperienceAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExperienceViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var experience = await _context.Experiences
                        .Include(e => e.ExperienceResponsibilities)
                        .FirstOrDefaultAsync(e => e.Id == id);

                    if (experience == null)
                    {
                        return NotFound();
                    }

                    experience.Title_tr = viewModel.Title_tr;
                    experience.Title_en = viewModel.Title_en;
                    experience.Company_tr = viewModel.Company_tr;
                    experience.Company_en = viewModel.Company_en;
                    experience.Location_tr = viewModel.Location_tr;
                    experience.Location_en = viewModel.Location_en;
                    experience.DateRange_tr = viewModel.DateRange_tr;
                    experience.DateRange_en = viewModel.DateRange_en;

                    // Update responsibilities
                    // Simple approach: remove all and add new ones
                    experience.ExperienceResponsibilities.Clear();
                    foreach (var respViewModel in viewModel.Responsibilities)
                    {
                        experience.ExperienceResponsibilities.Add(new ExperienceResponsibility
                        {
                            Description_tr = respViewModel.Description_tr,
                            Description_en = respViewModel.Description_en
                        });
                    }

                    _context.Update(experience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienceExists(viewModel.Id))
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
            return View(viewModel);
        }

        // GET: ExperienceAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // POST: ExperienceAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperienceExists(int id)
        {
            return _context.Experiences.Any(e => e.Id == id);
        }
    }
} 