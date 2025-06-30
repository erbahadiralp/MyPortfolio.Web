using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Web.Models.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Proje başlığı (TR) boş bırakılamaz.")]
        [StringLength(150)]
        [Display(Name = "Proje Başlığı (TR)")]
        public string Title_tr { get; set; }

        [Required(ErrorMessage = "Proje başlığı (EN) boş bırakılamaz.")]
        [StringLength(150)]
        [Display(Name = "Proje Başlığı (EN)")]
        public string Title_en { get; set; }

        [Display(Name = "Açıklama (TR)")]
        public string? Description_tr { get; set; }

        [Display(Name = "Açıklama (EN)")]
        public string? Description_en { get; set; }

        [Display(Name = "Proje Linki")]
        public string? Link { get; set; }
        
        [Display(Name = "Kullanılan Teknolojiler")]
        public string? Tools { get; set; }

        // Mevcut görseli göstermek için
        public string? ExistingImageUrl { get; set; }

        // Yeni görsel yüklemek için
        [Display(Name = "Proje Görseli")]
        public IFormFile? ImageFile { get; set; }
    }
} 