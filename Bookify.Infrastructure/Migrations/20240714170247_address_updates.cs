using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class address_updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "address_province",
                table: "apartments",
                newName: "address_zip_code");

            migrationBuilder.RenameColumn(
                name: "address_postal_code",
                table: "apartments",
                newName: "address_state");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "address_zip_code",
                table: "apartments",
                newName: "address_province");

            migrationBuilder.RenameColumn(
                name: "address_state",
                table: "apartments",
                newName: "address_postal_code");
        }
    }
}
