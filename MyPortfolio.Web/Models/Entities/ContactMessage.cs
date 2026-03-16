using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Web.Models.Entities;

public partial class ContactMessage
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Lütfen adınızı ve soyadınızı giriniz.")]
    [MaxLength(100, ErrorMessage = "Ad Soyad 100 karakterden uzun olamaz.")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "Lütfen e-posta adresinizi giriniz.")]
    [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz.")]
    [MaxLength(100, ErrorMessage = "E-posta 100 karakterden uzun olamaz.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Lütfen konu giriniz.")]
    [MaxLength(200, ErrorMessage = "Konu 200 karakterden uzun olamaz.")]
    public string? Subject { get; set; }

    [Required(ErrorMessage = "Lütfen mesajınızı giriniz.")]
    [MaxLength(2000, ErrorMessage = "Mesaj 2000 karakterden uzun olamaz.")]
    public string? Message { get; set; }

    public DateTime? SentDate { get; set; }
}
