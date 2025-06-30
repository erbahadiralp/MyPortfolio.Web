using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Web.Models;
using MyPortfolio.Web.Models.Entities;
using MyPortfolio.Web.Models.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolio.Web.Controllers
{
    [Authorize]
    public class AboutAdminController : Controller
    {
        private readonly MyPortfolioDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<Admin> _userManager;

        public AboutAdminController(MyPortfolioDbContext context, IWebHostEnvironment hostEnvironment, UserManager<Admin> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        // GET: AboutAdmin
        public async Task<IActionResult> Index()
        {
            var about = await _context.Abouts.FirstOrDefaultAsync();
            if (about == null)
            {
                // If no record exists, present a form for a new one.
                return View(new AboutEditViewModel());
            }

            var viewModel = new AboutEditViewModel
            {
                Id = about.Id,
                FirstName = about.FirstName,
                LastName = about.LastName,
                Description_tr = about.Description_tr,
                Description_en = about.Description_en,
                ShortDescription_tr = about.ShortDescription_tr,
                ShortDescription_en = about.ShortDescription_en,
                Address = about.Address,
                Phone = about.Phone,
                Email = about.Email,
                ImageUrl = about.ImageUrl,
                CvDocumentUrl = about.CvDocumentUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(AboutEditViewModel viewModel)
        {
            // We only want to validate the password part of the model.
            // Clear all other model state errors.
            foreach (var key in ModelState.Keys.Where(k => !k.StartsWith("PasswordChange")).ToList())
            {
                ModelState.Remove(key);
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, viewModel.PasswordChange.OldPassword, viewModel.PasswordChange.NewPassword);
                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirildi.";
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı.");
                }
            }

            // If we got this far, something failed, redisplay form
            // We need to repopulate the main part of the view model
            var about = await _context.Abouts.FirstOrDefaultAsync();
            if (about != null)
            {
                viewModel.Id = about.Id;
                viewModel.FirstName = about.FirstName;
                viewModel.LastName = about.LastName;
                viewModel.Description_tr = about.Description_tr;
                viewModel.Description_en = about.Description_en;
                viewModel.ShortDescription_tr = about.ShortDescription_tr;
                viewModel.ShortDescription_en = about.ShortDescription_en;
                viewModel.Address = about.Address;
                viewModel.Phone = about.Phone;
                viewModel.Email = about.Email;
                viewModel.ImageUrl = about.ImageUrl;
                viewModel.CvDocumentUrl = about.CvDocumentUrl;
            }
    
            TempData["ErrorMessage"] = "Şifre değiştirilemedi. Lütfen hataları kontrol edip tekrar deneyin.";
            return View("Index", viewModel);
        }

        // POST: AboutAdmin/Edit/5 or Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AboutEditViewModel viewModel)
        {
            // We only want to validate the main profile info, not the password change fields.
            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("PasswordChange")).ToList())
            {
                ModelState.Remove(key);
            }

            if (!ModelState.IsValid)
            {
                // If model state is invalid, return the view with the same view model to show validation errors
                return View("Index", viewModel);
            }

            About aboutEntity;

            if (viewModel.Id == 0) // It's a new record
            {
                aboutEntity = new About();
                _context.Abouts.Add(aboutEntity);
            }
            else // It's an existing record
            {
                aboutEntity = await _context.Abouts.FindAsync(viewModel.Id);
                if (aboutEntity == null)
                {
                    return NotFound();
                }
            }

            // Map ViewModel properties to the entity
            aboutEntity.FirstName = viewModel.FirstName;
            aboutEntity.LastName = viewModel.LastName;
            aboutEntity.Description_tr = viewModel.Description_tr;
            aboutEntity.Description_en = viewModel.Description_en;
            aboutEntity.ShortDescription_tr = viewModel.ShortDescription_tr;
            aboutEntity.ShortDescription_en = viewModel.ShortDescription_en;
            aboutEntity.Address = viewModel.Address;
            aboutEntity.Phone = viewModel.Phone;
            aboutEntity.Email = viewModel.Email;

            // Handle file uploads
            if (viewModel.ImageUrlFile != null)
            {
                aboutEntity.ImageUrl = await SaveFile(viewModel.ImageUrlFile, aboutEntity.ImageUrl, "profile");
            }
            if (viewModel.CvDocumentUrlFile != null)
            {
                aboutEntity.CvDocumentUrl = await SaveFile(viewModel.CvDocumentUrlFile, aboutEntity.CvDocumentUrl, "cv");
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Bilgiler başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveFile(IFormFile file, string existingPath, string prefix)
        {
            // Delete old file if it exists
            if (!string.IsNullOrEmpty(existingPath))
            {
                var oldFilePath = Path.Combine(_hostEnvironment.WebRootPath, existingPath.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            // Save new file
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = $"{prefix}-{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(wwwRootPath, "static", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return "/static/" + fileName;
        }
    }
} 