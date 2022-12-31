using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceByStore_Computers_ComputerId",
                table: "PriceByStore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceByStore",
                table: "PriceByStore");

            migrationBuilder.RenameTable(
                name: "PriceByStore",
                newName: "PriceByStores");

            migrationBuilder.RenameIndex(
                name: "IX_PriceByStore_ComputerId",
                table: "PriceByStores",
                newName: "IX_PriceByStores_ComputerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceByStores",
                table: "PriceByStores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceByStores_Computers_ComputerId",
                table: "PriceByStores",
                column: "ComputerId",
                principalTable: "Computers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceByStores_Computers_ComputerId",
                table: "PriceByStores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceByStores",
                table: "PriceByStores");

            migrationBuilder.RenameTable(
                name: "PriceByStores",
                newName: "PriceByStore");

            migrationBuilder.RenameIndex(
                name: "IX_PriceByStores_ComputerId",
                table: "PriceByStore",
                newName: "IX_PriceByStore_ComputerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceByStore",
                table: "PriceByStore",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceByStore_Computers_ComputerId",
                table: "PriceByStore",
                column: "ComputerId",
                principalTable: "Computers",
                principalColumn: "Id");
        }
    }
}
