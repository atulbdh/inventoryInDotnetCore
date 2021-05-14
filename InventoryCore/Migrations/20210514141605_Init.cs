using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryCore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemMasters",
                columns: table => new
                {
                    Item_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Add_By = table.Column<int>(type: "int", nullable: false),
                    Add_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Edit_By = table.Column<int>(type: "int", nullable: false),
                    Edit_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delete_By = table.Column<int>(type: "int", nullable: false),
                    Delete_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMasters", x => x.Item_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemMasters");
        }
    }
}
