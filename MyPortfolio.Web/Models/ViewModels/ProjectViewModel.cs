using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Web.Models.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Proje başlığı boş bırakılamaz.")]
        [StringLength(150)]
        [Display(Name = "Proje Başlığı")]
        public string Title { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

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