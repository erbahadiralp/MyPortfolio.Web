using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Web.Models.Entities;

public partial class ContactMessage
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Lütfen adınızı ve soyadınızı giriniz.")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "Lütfen e-posta adresinizi giriniz.")]
    [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Lütfen konu giriniz.")]
    public string? Subject { get; set; }

    [Required(ErrorMessage = "Lütfen mesajınızı giriniz.")]
    public string? Message { get; set; }

    public DateTime? SentDate { get; set; }
}
