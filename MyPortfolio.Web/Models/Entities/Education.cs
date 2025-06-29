using System;
using System.Collections.Generic;

namespace MyPortfolio.Web.Models.Entities;

public partial class Education
{
    public int Id { get; set; }

    public string? School { get; set; }

    public string? Department { get; set; }

    public string? DateRange { get; set; }

    public string? Description { get; set; }
}
