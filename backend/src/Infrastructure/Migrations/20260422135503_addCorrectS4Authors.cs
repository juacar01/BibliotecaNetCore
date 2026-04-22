using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCorrectS4Authors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Authors",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Authors",
                newName: "Country");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Authors",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Authors",
                newName: "country");
        }
    }
}
