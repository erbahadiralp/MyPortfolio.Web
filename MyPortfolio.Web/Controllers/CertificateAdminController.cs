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
    public class CertificateAdminController : Controller
    {
        private readonly MyPortfolioDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CertificateAdminController(MyPortfolioDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: CertificateAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Certificates.OrderBy(c => c.DisplayOrder).ToListAsync());
        }

        // GET: CertificateAdmin/Create
        public IActionResult Create()
        {
            return View(new CertificateViewModel());
        }

        // POST: CertificateAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CertificateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string pdfFileName = null;
                if (viewModel.PdfFile != null)
                {
                    var saveResult = await SavePdfFile(viewModel.PdfFile);
                    if (saveResult == null)
                    {
                        ModelState.AddModelError("PdfFile", "Sadece PDF dosyası yüklenebilir.");
                        return View(viewModel);
                    }
                    pdfFileName = saveResult;
                }

                var certificate = new Certificate
                {
                    Title_tr = viewModel.Title_tr,
                    Title_en = viewModel.Title_en,
                    Issuer_tr = viewModel.Issuer_tr,
                    Issuer_en = viewModel.Issuer_tr,
                    DateRange_tr = viewModel.DateRange_tr,
                    DateRange_en = viewModel.DateRange_en,
                    DisplayOrder = viewModel.DisplayOrder,
                    PdfUrl = pdfFileName
                };

                _context.Add(certificate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: CertificateAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var certificate = await _context.Certificates.FindAsync(id);
            if (certificate == null) return NotFound();

            var viewModel = new CertificateViewModel
            {
                Id = certificate.Id,
                Title_tr = certificate.Title_tr,
                Title_en = certificate.Title_en,
                Issuer_tr = certificate.Issuer_tr,
                Issuer_en = certificate.Issuer_en,
                DateRange_tr = certificate.DateRange_tr,
                DateRange_en = certificate.DateRange_en,
                DisplayOrder = certificate.DisplayOrder,
                ExistingPdfUrl = certificate.PdfUrl
            };

            return View(viewModel);
        }

        // POST: CertificateAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CertificateViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                var current = await _context.Certificates.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
                if (current != null) viewModel.ExistingPdfUrl = current.PdfUrl;
                return View(viewModel);
            }

            var certToUpdate = await _context.Certificates.FindAsync(id);
            if (certToUpdate == null) return NotFound();

            certToUpdate.Title_tr = viewModel.Title_tr;
            certToUpdate.Title_en = viewModel.Title_en;
            certToUpdate.Issuer_tr = viewModel.Issuer_tr;
            certToUpdate.Issuer_en = viewModel.Issuer_tr;
            certToUpdate.DateRange_tr = viewModel.DateRange_tr;
            certToUpdate.DateRange_en = viewModel.DateRange_en;
            certToUpdate.DisplayOrder = viewModel.DisplayOrder;

            if (viewModel.PdfFile != null)
            {
                var saveResult = await SavePdfFile(viewModel.PdfFile);
                if (saveResult == null)
                {
                    ModelState.AddModelError("PdfFile", "Sadece PDF dosyası yüklenebilir.");
                    viewModel.ExistingPdfUrl = certToUpdate.PdfUrl;
                    return View(viewModel);
                }

                // Delete old PDF
                if (!string.IsNullOrEmpty(certToUpdate.PdfUrl))
                {
                    var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "static", "certificates", certToUpdate.PdfUrl);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                certToUpdate.PdfUrl = saveResult;
            }
            else
            {
                certToUpdate.PdfUrl = viewModel.ExistingPdfUrl;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Certificates.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: CertificateAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var certificate = await _context.Certificates.FirstOrDefaultAsync(m => m.Id == id);
            if (certificate == null) return NotFound();

            ViewData["CertificateTitle"] = certificate.Title_tr;
            return View();
        }

        // POST: CertificateAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certificate = await _context.Certificates.FindAsync(id);
            if (certificate == null) return NotFound();

            if (!string.IsNullOrEmpty(certificate.PdfUrl))
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "static", "certificates", certificate.PdfUrl);
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }

            _context.Certificates.Remove(certificate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SavePdfFile(IFormFile file)
        {
            // Only allow PDF
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (ext != ".pdf") return null;

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "static", "certificates");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = "cert-" + Guid.NewGuid().ToString() + ".pdf";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return uniqueFileName;
        }
    }
}
