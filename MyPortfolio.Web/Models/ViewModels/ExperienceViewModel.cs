using MyPortfolio.Web.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Web.Models.ViewModels
{
    public class ExperienceViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pozisyon alanı boş bırakılamaz.")]
        [StringLength(100)]
        [Display(Name = "Pozisyon")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Şirket alanı boş bırakılamaz.")]
        [StringLength(100)]
        [Display(Name = "Şirket")]
        public string Company { get; set; }

        [StringLength(100)]
        [Display(Name = "Konum")]
        public string Location { get; set; }

        [StringLength(50)]
        [Display(Name = "Tarih Aralığı")]
        public string DateRange { get; set; }
        
        public List<ExperienceResponsibility> Responsibilities { get; set; } = new List<ExperienceResponsibility>();
    }
} 