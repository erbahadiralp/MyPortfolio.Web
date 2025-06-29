using System;
using System.Collections.Generic;

namespace MyPortfolio.Web.Models.Entities;

public partial class Experience
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Company { get; set; }

    public string? Location { get; set; }

    public string? DateRange { get; set; }

    public virtual ICollection<ExperienceResponsibility> ExperienceResponsibilities { get; set; } = new List<ExperienceResponsibility>();
}
