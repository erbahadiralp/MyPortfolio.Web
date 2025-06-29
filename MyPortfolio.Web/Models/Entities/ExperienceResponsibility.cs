using System;
using System.Collections.Generic;

namespace MyPortfolio.Web.Models.Entities;

public partial class ExperienceResponsibility
{
    public int Id { get; set; }

    public int ExperienceId { get; set; }

    public string? Description { get; set; }

    public virtual Experience? Experience { get; set; }
}
