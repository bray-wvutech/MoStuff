using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCartChange1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_StoreItems_StoreItemId",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_StoreItemId",
                table: "ShoppingCartItems");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ShoppingCartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PictureUri",
                table: "ShoppingCartItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ShoppingCartItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "PictureUri",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ShoppingCartItems");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_StoreItemId",
                table: "ShoppingCartItems",
                column: "StoreItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_StoreItems_StoreItemId",
                table: "ShoppingCartItems",
                column: "StoreItemId",
                principalTable: "StoreItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
