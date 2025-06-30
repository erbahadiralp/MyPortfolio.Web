using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MyPortfolio.Web.Models.Entities;

public partial class Experience
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


    public string? Company_tr { get; set; }
    public string? Company_en { get; set; }
    [NotMapped]
    public string Company
    {
        get
        {
            var culture = CultureInfo.CurrentUICulture.Name.ToLower();
            if (culture.StartsWith("tr"))
                return Company_tr;
            return Company_en;
        }
    }


    public string? Location_tr { get; set; }
    public string? Location_en { get; set; }
    [NotMapped]
    public string? Location => CultureInfo.CurrentUICulture.Name.StartsWith("tr") ? Location_tr : Location_en;


    public string? DateRange_tr { get; set; }
    public string? DateRange_en { get; set; }

    [NotMapped]
    public string? DateRange => CultureInfo.CurrentUICulture.Name.StartsWith("tr") ? DateRange_tr : DateRange_en;

    public virtual ICollection<ExperienceResponsibility> ExperienceResponsibilities { get; set; } = new List<ExperienceResponsibility>();
}
