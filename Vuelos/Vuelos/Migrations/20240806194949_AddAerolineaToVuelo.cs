using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vuelos.Migrations
{
    /// <inheritdoc />
    public partial class AddAerolineaToVuelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "delayed",
                table: "Vuelos",
                newName: "IsDelayed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDelayed",
                table: "Vuelos",
                newName: "delayed");
        }
    }
}
