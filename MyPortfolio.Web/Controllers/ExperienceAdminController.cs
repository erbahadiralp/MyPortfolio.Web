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
                    Title = viewModel.Title,
                    Company = viewModel.Company,
                    Location = viewModel.Location,
                    DateRange = viewModel.DateRange
                };

                // Filter out empty responsibilities and add valid ones
                viewModel.Responsibilities?
                    .Where(r => !string.IsNullOrWhiteSpace(r.Description))
                    .ToList()
                    .ForEach(r => experience.ExperienceResponsibilities.Add(new ExperienceResponsibility { Description = r.Description }));
                
                _context.Add(experience);
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
                Title = experience.Title,
                Company = experience.Company,
                Location = experience.Location,
                DateRange = experience.DateRange,
                Responsibilities = experience.ExperienceResponsibilities.ToList()
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
                var experienceToUpdate = await _context.Experiences
                    .Include(e => e.ExperienceResponsibilities)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (experienceToUpdate == null)
                {
                    return NotFound();
                }

                experienceToUpdate.Title = viewModel.Title;
                experienceToUpdate.Company = viewModel.Company;
                experienceToUpdate.Location = viewModel.Location;
                experienceToUpdate.DateRange = viewModel.DateRange;

                // Update, Add, and Delete Responsibilities
                var submittedResponsibilities = viewModel.Responsibilities?.Where(r => !string.IsNullOrWhiteSpace(r.Description)).ToList() ?? new();
                
                // Delete responsibilities that are no longer in the submitted list
                var responsibilitiesToDelete = experienceToUpdate.ExperienceResponsibilities
                    .Where(rDb => !submittedResponsibilities.Any(rVm => rVm.Id == rDb.Id)).ToList();
                _context.ExperienceResponsibilities.RemoveRange(responsibilitiesToDelete);

                // Update existing or Add new responsibilities
                foreach (var respVm in submittedResponsibilities)
                {
                    var respDb = experienceToUpdate.ExperienceResponsibilities.FirstOrDefault(r => r.Id == respVm.Id);
                    if (respDb != null) // It's an existing one, update it
                    {
                        respDb.Description = respVm.Description;
                    }
                    else // It's a new one, add it
                    {
                        experienceToUpdate.ExperienceResponsibilities.Add(new ExperienceResponsibility { Description = respVm.Description });
                    }
                }
                
                await _context.SaveChangesAsync();
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
    }
} 