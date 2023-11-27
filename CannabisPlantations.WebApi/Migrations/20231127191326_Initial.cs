using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CannabisPlantations.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agronomists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Agronomi__3214EC07A55AD8E7", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Applicat__3214EC07EFBF25C1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessTrips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Business__3214EC07B94F5A1B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CannabisTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cannabis__3214EC07D6CC9167", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__3214EC072C779624", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Scopes__3214EC07B3FD58E6", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationCredentials",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Applicat__3214EC07170F979D", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Applicati__Appli__60A75C0F",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessTripAgronomists",
                columns: table => new
                {
                    AgronomistId = table.Column<int>(type: "int", nullable: false),
                    BusinessTripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Business__B2C1A236EAEEEEF7", x => new { x.AgronomistId, x.BusinessTripId });
                    table.ForeignKey(
                        name: "FK__BusinessT__Agron__44FF419A",
                        column: x => x.AgronomistId,
                        principalTable: "Agronomists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__BusinessT__Busin__5441852A",
                        column: x => x.BusinessTripId,
                        principalTable: "BusinessTrips",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Harvest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgronomistId = table.Column<int>(type: "int", nullable: false),
                    CannabisTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Harvest__3214EC07BD39CE95", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Harvest__Agronom__46E78A0C",
                        column: x => x.AgronomistId,
                        principalTable: "Agronomists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Harvest__Cannabi__5165187F",
                        column: x => x.CannabisTypeId,
                        principalTable: "CannabisTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CannabisTypeId = table.Column<int>(type: "int", nullable: false),
                    AgronomistId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Products__3214EC075B649FFA", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Products__Agrono__47DBAE45",
                        column: x => x.AgronomistId,
                        principalTable: "Agronomists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Products__Cannab__52593CB8",
                        column: x => x.CannabisTypeId,
                        principalTable: "CannabisTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feedback__3214EC077A9455CB", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Feedback__Custom__4BAC3F29",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgronomistId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__3214EC074183CBE2", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Orders__Agronomi__48CFD27E",
                        column: x => x.AgronomistId,
                        principalTable: "Agronomists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Orders__Customer__4CA06362",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Returns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgronomistId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Returns__3214EC07CFA7A054", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Returns__Agronom__45F365D3",
                        column: x => x.AgronomistId,
                        principalTable: "Agronomists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Returns__Custome__4AB81AF0",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScopeApplicationCredentials",
                columns: table => new
                {
                    ScopeId = table.Column<int>(type: "int", nullable: false),
                    ApplicationCredentialId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ScopeApp__4FAE9254E9E84959", x => new { x.ScopeId, x.ApplicationCredentialId });
                    table.ForeignKey(
                        name: "FK__ScopeAppl__Appli__6FE99F9F",
                        column: x => x.ApplicationCredentialId,
                        principalTable: "ApplicationCredentials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__ScopeAppl__Scope__6E01572D",
                        column: x => x.ScopeId,
                        principalTable: "Scopes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductStorage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductS__3214EC07177E5FEF", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ProductSt__Produ__4E88ABD4",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tastings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AgronomistId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tastings__3214EC07D41C760C", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Tastings__Agrono__440B1D61",
                        column: x => x.AgronomistId,
                        principalTable: "Agronomists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Tastings__Produc__4D94879B",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__5835C37179EEE31C", x => new { x.ProductId, x.OrderId });
                    table.ForeignKey(
                        name: "FK__OrderDeta__Order__5535A963",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__OrderDeta__Produ__4F7CD00D",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReturnDetails",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ReturnId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ReturnDe__2B4898572FB78776", x => new { x.ProductId, x.ReturnId });
                    table.ForeignKey(
                        name: "FK__ReturnDet__Produ__5070F446",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__ReturnDet__Retur__5629CD9C",
                        column: x => x.ReturnId,
                        principalTable: "Returns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TastingCustomers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TastingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TastingC__4EF05ECA74ADE805", x => new { x.CustomerId, x.TastingId });
                    table.ForeignKey(
                        name: "FK__TastingCu__Custo__49C3F6B7",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__TastingCu__Tasti__534D60F1",
                        column: x => x.TastingId,
                        principalTable: "Tastings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Agronomi__737584F63DC65F9E",
                table: "Agronomists",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationCredentials_ApplicationId",
                table: "ApplicationCredentials",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "UQ_Secret",
                table: "ApplicationCredentials",
                column: "Secret",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_AgronomistId",
                table: "BusinessTripAgronomists",
                column: "AgronomistId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTripAgronomists_BusinessTripId",
                table: "BusinessTripAgronomists",
                column: "BusinessTripId");

            migrationBuilder.CreateIndex(
                name: "UQ__Cannabis__737584F6527CBB50",
                table: "CannabisTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__737584F6EF9194F2",
                table: "Customers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_CustomerId",
                table: "Feedback",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Harvest_AgronomistId",
                table: "Harvest",
                column: "AgronomistId");

            migrationBuilder.CreateIndex(
                name: "IX_Harvest_CannabisTypeId",
                table: "Harvest",
                column: "CannabisTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "idx_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AgronomistId",
                table: "Orders",
                column: "AgronomistId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AgronomistId",
                table: "Products",
                column: "AgronomistId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CannabisTypeId",
                table: "Products",
                column: "CannabisTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__ProductS__B40CC6CC9825AA36",
                table: "ProductStorage",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnDetails_ReturnId",
                table: "ReturnDetails",
                column: "ReturnId");

            migrationBuilder.CreateIndex(
                name: "idx_CustomerId",
                table: "Returns",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_AgronomistId",
                table: "Returns",
                column: "AgronomistId");

            migrationBuilder.CreateIndex(
                name: "IX_ScopeApplicationCredentials_ApplicationCredentialId",
                table: "ScopeApplicationCredentials",
                column: "ApplicationCredentialId");

            migrationBuilder.CreateIndex(
                name: "UQ__Scopes__737584F68222FCAD",
                table: "Scopes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_CustomerId",
                table: "TastingCustomers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TastingCustomers_TastingId",
                table: "TastingCustomers",
                column: "TastingId");

            migrationBuilder.CreateIndex(
                name: "idx_AgronomistId",
                table: "Tastings",
                column: "AgronomistId");

            migrationBuilder.CreateIndex(
                name: "IX_Tastings_ProductId",
                table: "Tastings",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessTripAgronomists");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Harvest");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductStorage");

            migrationBuilder.DropTable(
                name: "ReturnDetails");

            migrationBuilder.DropTable(
                name: "ScopeApplicationCredentials");

            migrationBuilder.DropTable(
                name: "TastingCustomers");

            migrationBuilder.DropTable(
                name: "BusinessTrips");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Returns");

            migrationBuilder.DropTable(
                name: "ApplicationCredentials");

            migrationBuilder.DropTable(
                name: "Scopes");

            migrationBuilder.DropTable(
                name: "Tastings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Agronomists");

            migrationBuilder.DropTable(
                name: "CannabisTypes");
        }
    }
}
