using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTechStore.Migrations
{
    /// <inheritdoc />
    public partial class gyhj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "products");
        }
    }
}
