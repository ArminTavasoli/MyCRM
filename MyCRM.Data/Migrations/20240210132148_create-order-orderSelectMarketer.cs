using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCRM.Data.Migrations
{
    /// <inheritdoc />
    public partial class createorderorderSelectMarketer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSale = table.Column<bool>(type: "bit", nullable: false),
                    IsFinish = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    OrderType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderSelectedMarketers",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    MarketerId = table.Column<long>(type: "bigint", nullable: false),
                    ModifyUserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSelectedMarketers", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderSelectedMarketers_Marketers_MarketerId",
                        column: x => x.MarketerId,
                        principalTable: "Marketers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderSelectedMarketers_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderSelectedMarketers_Users_ModifyUserId",
                        column: x => x.ModifyUserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSelectedMarketers_MarketerId",
                table: "OrderSelectedMarketers",
                column: "MarketerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSelectedMarketers_ModifyUserId",
                table: "OrderSelectedMarketers",
                column: "ModifyUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderSelectedMarketers");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
