using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CannabisPlantations.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class DeletedTablesForAuthority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationCredentialScopes");

            migrationBuilder.DropTable(
                name: "ApplicationCredentials");

            migrationBuilder.DropTable(
                name: "Scopes");

            migrationBuilder.DropTable(
                name: "Applications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationCredentials",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationCredentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationCredentials_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationCredentialScopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationCredentialId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ScopeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationCredentialScopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationCredentialScopes_ApplicationCredentials_ApplicationCredentialId",
                        column: x => x.ApplicationCredentialId,
                        principalTable: "ApplicationCredentials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationCredentialScopes_Scopes_ScopeId",
                        column: x => x.ScopeId,
                        principalTable: "Scopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationCredentials_ApplicationId",
                table: "ApplicationCredentials",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationCredentialScopes_ApplicationCredentialId",
                table: "ApplicationCredentialScopes",
                column: "ApplicationCredentialId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationCredentialScopes_ScopeId",
                table: "ApplicationCredentialScopes",
                column: "ScopeId");
        }
    }
}
