using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Web.Models;
using MyPortfolio.Web.Models.Entities;
using MyPortfolio.Web.Models.ViewModels;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace MyPortfolio.Web.Controllers
{
    [Authorize]
    public class ProjectAdminController : Controller
    {
        private readonly MyPortfolioDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectAdminController(MyPortfolioDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: ProjectAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.ToListAsync());
        }

        // GET: ProjectAdmin/Create
        public IActionResult Create()
        {
            return View(new ProjectViewModel());
        }

        // POST: ProjectAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string newFileName = null;
                if (viewModel.ImageFile != null)
                {
                    newFileName = await SaveFile(viewModel.ImageFile);
                }

                var project = new Project
                {
                    Title_tr = viewModel.Title_tr,
                    Title_en = viewModel.Title_en,
                    Description_tr = viewModel.Description_tr,
                    Description_en = viewModel.Description_en,
                    Link = viewModel.Link,
                    Tools = viewModel.Tools,
                    ImageUrl = newFileName
                };

                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: ProjectAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            var viewModel = new ProjectViewModel
            {
                Id = project.Id,
                Title_tr = project.Title_tr,
                Title_en = project.Title_en,
                Description_tr = project.Description_tr,
                Description_en = project.Description_en,
                Link = project.Link,
                Tools = project.Tools,
                ExistingImageUrl = project.ImageUrl
            };

            return View(viewModel);
        }

        // POST: ProjectAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                // If model validation fails, we must reload the ExistingImageUrl because it's not part of the form post for IFormFile
                var currentProject = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (currentProject != null)
                {
                    viewModel.ExistingImageUrl = currentProject.ImageUrl;
                }
                return View(viewModel);
            }
            
            var projectToUpdate = await _context.Projects.FindAsync(id);
            if (projectToUpdate == null) return NotFound();

            projectToUpdate.Title_tr = viewModel.Title_tr;
            projectToUpdate.Title_en = viewModel.Title_en;
            projectToUpdate.Description_tr = viewModel.Description_tr;
            projectToUpdate.Description_en = viewModel.Description_en;
            projectToUpdate.Link = viewModel.Link;
            projectToUpdate.Tools = viewModel.Tools;

            if (viewModel.ImageFile != null)
            {
                // If a new image is uploaded, delete the old one
                if (!string.IsNullOrEmpty(viewModel.ExistingImageUrl))
                {
                    var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "static", viewModel.ExistingImageUrl);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                // Save the new file and update the ImageUrl on our new object
                projectToUpdate.ImageUrl = await SaveFile(viewModel.ImageFile);
            }
            else
            {
                // Ensure the existing image url is carried over if no new file is uploaded
                projectToUpdate.ImageUrl = viewModel.ExistingImageUrl;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Projects.Any(e => e.Id == projectToUpdate.Id))
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

        // GET: ProjectAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            // Pass project info to the view via ViewData or a simple ViewModel if preferred
            ViewData["ProjectTitle"] = project.Title_tr;
            return View();
        }

        // POST: ProjectAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            // Delete the image file from wwwroot/static
            if (!string.IsNullOrEmpty(project.ImageUrl))
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "static", project.ImageUrl);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "static");
            var uniqueFileName = "project-" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return uniqueFileName;
        }
    }
} 