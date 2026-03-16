using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddLogoToExperienceEducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Experiences",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Educations",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Educations");
        }
    }
}
