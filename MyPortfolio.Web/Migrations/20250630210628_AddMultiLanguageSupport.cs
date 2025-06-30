using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddMultiLanguageSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "School",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Abouts");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Projects",
                newName: "Title_tr");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ExperienceResponsibilities",
                newName: "Description_tr");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Educations",
                newName: "School_tr");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Abouts",
                newName: "ShortDescription_tr");

            migrationBuilder.AddColumn<string>(
                name: "Category_en",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category_tr",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_en",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_tr",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description_en",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description_tr",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title_en",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company_en",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company_tr",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location_en",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location_tr",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title_en",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title_tr",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description_en",
                table: "ExperienceResponsibilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department_en",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department_tr",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description_en",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description_tr",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "School_en",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "ContactMessages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "ContactMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "ContactMessages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ContactMessages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description_en",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description_tr",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription_en",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category_en",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Category_tr",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Name_en",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Name_tr",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Description_en",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Description_tr",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Title_en",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Company_en",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "Company_tr",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "Location_en",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "Location_tr",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "Title_en",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "Title_tr",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "Description_en",
                table: "ExperienceResponsibilities");

            migrationBuilder.DropColumn(
                name: "Department_en",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "Department_tr",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "Description_en",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "Description_tr",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "School_en",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "Description_en",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "Description_tr",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "ShortDescription_en",
                table: "Abouts");

            migrationBuilder.RenameColumn(
                name: "Title_tr",
                table: "Projects",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Description_tr",
                table: "ExperienceResponsibilities",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "School_tr",
                table: "Educations",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "ShortDescription_tr",
                table: "Abouts",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Skills",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Skills",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Projects",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Experiences",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Experiences",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Experiences",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Educations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "School",
                table: "Educations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "ContactMessages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "ContactMessages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "ContactMessages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ContactMessages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Abouts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
