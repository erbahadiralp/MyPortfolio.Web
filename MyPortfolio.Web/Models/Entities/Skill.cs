using System;
using System.Collections.Generic;

namespace MyPortfolio.Web.Models.Entities;

public partial class Skill
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Category { get; set; }
}
