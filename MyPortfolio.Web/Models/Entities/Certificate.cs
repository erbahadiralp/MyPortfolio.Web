using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MyPortfolio.Web.Models.Entities;

public class Certificate
{
    public int Id { get; set; }

    public string? Title_tr { get; set; }
    public string? Title_en { get; set; }
    [NotMapped]
    public string? Title => CultureInfo.CurrentUICulture.Name.StartsWith("tr") ? Title_tr : Title_en;

    public string? Issuer_tr { get; set; }
    public string? Issuer_en { get; set; }
    [NotMapped]
    public string? Issuer => CultureInfo.CurrentUICulture.Name.StartsWith("tr") ? Issuer_tr : Issuer_en;

    public string? DateRange_tr { get; set; }
    public string? DateRange_en { get; set; }
    [NotMapped]
    public string? DateRange => CultureInfo.CurrentUICulture.Name.StartsWith("tr") ? DateRange_tr : DateRange_en;

    // PDF dosyasının /static/certificates/ altındaki adı
    public string? PdfUrl { get; set; }

    public int DisplayOrder { get; set; } = 0;
}
