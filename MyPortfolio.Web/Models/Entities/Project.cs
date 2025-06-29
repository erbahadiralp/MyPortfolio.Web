﻿using System;
using System.Collections.Generic;

namespace MyPortfolio.Web.Models.Entities;

public partial class Project
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Link { get; set; }

    public string? ImageUrl { get; set; }

    public string? Tools { get; set; }
}
