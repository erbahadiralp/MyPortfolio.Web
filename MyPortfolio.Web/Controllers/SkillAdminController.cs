using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Web.Models;
using MyPortfolio.Web.Models.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyPortfolio.Web.Controllers
{
    [Authorize]
    public class SkillAdminController : Controller
    {
        private readonly MyPortfolioDbContext _context;

        public SkillAdminController(MyPortfolioDbContext context)
        {
            _context = context;
        }

        // GET: SkillAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skills.ToListAsync());
        }

        // GET: SkillAdmin/Create
        public IActionResult Create()
        {
            ViewBag.Kategoriler = GetCategorySelectList();
            return View();
        }

        // POST: SkillAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill model)
        {
            if (ModelState.IsValid)
            {
                var skill = new Skill
                {
                    Name_tr = model.Name_tr,
                    Name_en = model.Name_en,
                    Category_tr = model.Category_tr,
                    Category_en = model.Category_en
                };
                _context.Add(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Kategoriler = GetCategorySelectList(model.Category_tr);
            return View(model);
        }

        // GET: SkillAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            ViewBag.Kategoriler = GetCategorySelectList(skill.Category_tr);
            return View(skill);
        }

        // POST: SkillAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Skill model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var skillToUpdate = await _context.Skills.FindAsync(id);
                    if(skillToUpdate == null) return NotFound();

                    skillToUpdate.Name_tr = model.Name_tr;
                    skillToUpdate.Name_en = model.Name_en;
                    skillToUpdate.Category_tr = model.Category_tr;
                    skillToUpdate.Category_en = model.Category_en;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Skills.Any(e => e.Id == model.Id))
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
            ViewBag.Kategoriler = GetCategorySelectList(model.Category_tr);
            return View(model);
        }

        // GET: SkillAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: SkillAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill != null) _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private SelectList GetCategorySelectList(string? selectedValue = null)
        {
            // This is now problematic as categories are bilingual.
            // This is a temporary solution. A better approach would be a separate table for categories.
            var categories = new Dictionary<string, string>
            {
                { "Programming Languages", "Programlama Dilleri" },
                { "Frameworks & Libraries", "Frameworkler & Kütüphaneler" },
                { "Databases", "Veritabanları" },
                { "Tools & Platforms", "Araçlar & Platformlar" },
                { "Other", "Diğer" }
            };
            
            // For the SelectList, we need to decide which language to show. We'll use the English name for the value.
            var selectListItems = categories.Select(c => new SelectListItem
            {
                Value = c.Key,
                Text = $"{c.Value} / {c.Key}"
            });

            return new SelectList(selectListItems, "Value", "Text", selectedValue);
        }
    }
} 