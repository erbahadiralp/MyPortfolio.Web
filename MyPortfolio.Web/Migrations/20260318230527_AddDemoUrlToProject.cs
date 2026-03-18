using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddDemoUrlToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DemoUrl",
                table: "Projects",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DemoUrl",
                table: "Projects");
        }
    }
}
