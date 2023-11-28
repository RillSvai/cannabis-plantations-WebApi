using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CannabisPlantations.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldForAgronom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Agronomists",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Agronomists");
        }
    }
}
