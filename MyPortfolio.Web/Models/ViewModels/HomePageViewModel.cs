using MyPortfolio.Web.Models.Entities;
using System.Collections.Generic;

namespace MyPortfolio.Web.Models.ViewModels
{
    public class HomePageViewModel
    {
        public List<About> Abouts { get; set; }
        public List<Experience> Experiences { get; set; }
        public List<Education> Educations { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Project> Projects { get; set; }
        public ContactMessage ContactMessage { get; set; }
    }
} 