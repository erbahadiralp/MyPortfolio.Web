using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MyPortfolio.Web.Models.Entities;

public partial class About
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

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

    public string? ImageUrl { get; set; }

    public string? ShortDescription_tr { get; set; }
    public string? ShortDescription_en { get; set; }

    [NotMapped]
    public string ShortDescription
    {
        get
        {
            var culture = CultureInfo.CurrentUICulture.Name.ToLower();
            if (culture.StartsWith("tr"))
                return ShortDescription_tr;
            return ShortDescription_en;
        }
    }

    public string? CvDocumentUrl { get; set; }
}
