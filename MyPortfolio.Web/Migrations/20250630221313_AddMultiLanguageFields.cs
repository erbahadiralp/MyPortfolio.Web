using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddMultiLanguageFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRange",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "DateRange",
                table: "Educations");

            migrationBuilder.AddColumn<string>(
                name: "DateRange_en",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateRange_tr",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateRange_en",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateRange_tr",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRange_en",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "DateRange_tr",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "DateRange_en",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "DateRange_tr",
                table: "Educations");

            migrationBuilder.AddColumn<string>(
                name: "DateRange",
                table: "Experiences",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateRange",
                table: "Educations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
