using System;
using System.Collections.Generic;

namespace MyPortfolio.Web.Models.Entities;

public partial class About
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string? ShortDescription { get; set; }

    public string? CvDocumentUrl { get; set; }
}
