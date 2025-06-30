using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MyPortfolio.Web.Models.Entities;

public partial class Education
{
    public int Id { get; set; }

    public string? School_tr { get; set; }
    public string? School_en { get; set; }
    [NotMapped]
    public string? School => CultureInfo.CurrentUICulture.Name.StartsWith("tr") ? School_tr : School_en;

    public string? Department_tr { get; set; }
    public string? Department_en { get; set; }
    [NotMapped]
    public string? Department => CultureInfo.CurrentUICulture.Name.StartsWith("tr") ? Department_tr : Department_en;

    public string? Description_tr { get; set; }
    public string? Description_en { get; set; }
    [NotMapped]
    public string? Description => CultureInfo.CurrentUICulture.Name.StartsWith("tr") ? Description_tr : Description_en;

    public string? DateRange_tr { get; set; }
    public string? DateRange_en { get; set; }
    [NotMapped]
    public string? DateRange => CultureInfo.CurrentUICulture.Name.StartsWith("tr") ? DateRange_tr : DateRange_en;
}
