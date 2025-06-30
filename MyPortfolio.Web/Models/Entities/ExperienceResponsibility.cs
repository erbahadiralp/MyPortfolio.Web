using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MyPortfolio.Web.Models.Entities;

public partial class ExperienceResponsibility
{
    public int Id { get; set; }

    public int ExperienceId { get; set; }

    public string? Description_tr { get; set; }
    public string? Description_en { get; set; }
    [NotMapped]
    public string Description
    {
        get
        {
            var culture = CultureInfo.CurrentUICulture.Name.ToLower();
            if (culture.StartsWith("tr"))
                return Description_tr;
            return Description_en;
        }
    }

    public virtual Experience? Experience { get; set; }
}
