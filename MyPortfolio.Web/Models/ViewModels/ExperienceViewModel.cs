using MyPortfolio.Web.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace MyPortfolio.Web.Models.ViewModels
{
    public class ExperienceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Logo Yolu")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Logo Yükle")]
        public IFormFile? Logo { get; set; }

        [Required(ErrorMessage = "Pozisyon (TR) alanı boş bırakılamaz.")]
        [StringLength(100)]
        [Display(Name = "Pozisyon (TR)")]
        public string Title_tr { get; set; }

        [Required(ErrorMessage = "Pozisyon (EN) alanı boş bırakılamaz.")]
        [StringLength(100)]
        [Display(Name = "Pozisyon (EN)")]
        public string Title_en { get; set; }


        [Required(ErrorMessage = "Şirket (TR) alanı boş bırakılamaz.")]
        [StringLength(100)]
        [Display(Name = "Şirket (TR)")]
        public string Company_tr { get; set; }

        [StringLength(100)]
        [Display(Name = "Şirket (EN)")]
        public string? Company_en { get; set; }


        [Display(Name = "Konum (Türkçe)")]
        public string? Location_tr { get; set; }

        [StringLength(100)]
        [Display(Name = "Konum (EN)")]
        public string? Location_en { get; set; }


        [Display(Name = "Tarih Aralığı (Türkçe)")]
        public string? DateRange_tr { get; set; }

        [Display(Name = "Tarih Aralığı (İngilizce)")]
        public string? DateRange_en { get; set; }
        
        public List<ExperienceResponsibilityViewModel> Responsibilities { get; set; } = new List<ExperienceResponsibilityViewModel>();
    }
} 