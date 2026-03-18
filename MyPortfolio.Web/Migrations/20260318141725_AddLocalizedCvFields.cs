using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalizedCvFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CvOriginalFileName",
                table: "Abouts",
                newName: "CvOriginalFileName_tr");

            migrationBuilder.RenameColumn(
                name: "CvDocumentURL",
                table: "Abouts",
                newName: "CvDocumentURL_tr");

            migrationBuilder.AddColumn<string>(
                name: "CvDocumentURL_en",
                table: "Abouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CvOriginalFileName_en",
                table: "Abouts",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvDocumentURL_en",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "CvOriginalFileName_en",
                table: "Abouts");

            migrationBuilder.RenameColumn(
                name: "CvOriginalFileName_tr",
                table: "Abouts",
                newName: "CvOriginalFileName");

            migrationBuilder.RenameColumn(
                name: "CvDocumentURL_tr",
                table: "Abouts",
                newName: "CvDocumentURL");
        }
    }
}
