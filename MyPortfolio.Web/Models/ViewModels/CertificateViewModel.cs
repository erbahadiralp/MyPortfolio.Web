using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Web.Models.ViewModels
{
    public class CertificateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sertifika başlığı (TR) boş bırakılamaz.")]
        [StringLength(200)]
        [Display(Name = "Sertifika Başlığı (TR)")]
        public string Title_tr { get; set; }

        [Required(ErrorMessage = "Sertifika başlığı (EN) boş bırakılamaz.")]
        [StringLength(200)]
        [Display(Name = "Sertifika Başlığı (EN)")]
        public string Title_en { get; set; }

        [Display(Name = "Yayıncı Kurum (TR)")]
        public string? Issuer_tr { get; set; }

        [Display(Name = "Yayıncı Kurum (EN)")]
        public string? Issuer_en { get; set; }

        [Display(Name = "Tarih Aralığı (TR)")]
        public string? DateRange_tr { get; set; }

        [Display(Name = "Tarih Aralığı (EN)")]
        public string? DateRange_en { get; set; }

        [Display(Name = "Sıralama")]
        public int DisplayOrder { get; set; } = 0;

        // Mevcut PDF'i göstermek için
        public string? ExistingPdfUrl { get; set; }

        // Yeni PDF yüklemek için
        [Display(Name = "Sertifika PDF Dosyası")]
        public IFormFile? PdfFile { get; set; }
    }
}
