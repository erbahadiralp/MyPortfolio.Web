using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MyPortfolio.Web.Models.Entities;

public partial class Project
{
    public int Id { get; set; }

    public string? Title_tr { get; set; }
    public string? Title_en { get; set; }
    [NotMapped]
    public string Title
    {
        get
        {
            var culture = CultureInfo.CurrentUICulture.Name.ToLower();
            if (culture.StartsWith("tr"))
                return Title_tr;
            return Title_en;
        }
    }


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


    public string? Link { get; set; }

    public string? ImageUrl { get; set; }

    public string? Tools { get; set; }
}
