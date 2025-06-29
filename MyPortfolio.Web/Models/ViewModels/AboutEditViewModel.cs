using Microsoft.AspNetCore.Http;
using MyPortfolio.Web.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Web.Models.ViewModels
{
    public class AboutEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı boş bırakılamaz.")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad alanı boş bırakılamaz.")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Display(Name = "Açıklama (Ana Sayfa)")]
        public string Description { get; set; }
        
        [Display(Name = "Kısa Açıklama (Sidebar)")]
        public string ShortDescription { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi girin.")]
        [Display(Name = "Mail Adresi")]
        public string Email { get; set; }

        // This will hold the path of the existing image
        public string? ImageUrl { get; set; }

        // This will hold the path of the existing CV
        public string? CvDocumentUrl { get; set; }

        // This will be used to upload a new image
        [Display(Name = "Profil Resmi Dosyası")]
        public IFormFile? ImageUrlFile { get; set; }

        // This will be used to upload a new CV
        [Display(Name = "CV Belgesi Dosyası")]
        public IFormFile? CvDocumentUrlFile { get; set; }

        public PasswordChangeViewModel PasswordChange { get; set; } = new PasswordChangeViewModel();
    }
} 