using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MyPortfolio.Web.Models.Entities;

public partial class Skill
{
    public int Id { get; set; }

    public string? Name_tr { get; set; }
    public string? Name_en { get; set; }
    [NotMapped]
    public string Name
    {
        get
        {
            var culture = CultureInfo.CurrentUICulture.Name.ToLower();
            if (culture.StartsWith("tr"))
                return Name_tr;
            return Name_en;
        }
    }


    public string? Category_tr { get; set; }
    public string? Category_en { get; set; }
    [NotMapped]
    public string Category
    {
        get
        {
            var culture = CultureInfo.CurrentUICulture.Name.ToLower();
            if (culture.StartsWith("tr"))
                return Category_tr;
            return Category_en;
        }
    }
}
