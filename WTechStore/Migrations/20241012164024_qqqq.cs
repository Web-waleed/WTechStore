using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTechStore.Migrations
{
    /// <inheritdoc />
    public partial class qqqq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SliderId",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_products_SliderId",
                table: "products",
                column: "SliderId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Sliders_SliderId",
                table: "products",
                column: "SliderId",
                principalTable: "Sliders",
                principalColumn: "SliderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Sliders_SliderId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_SliderId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "SliderId",
                table: "products");
        }
    }
}
