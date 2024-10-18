using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTechStore.Migrations
{
    /// <inheritdoc />
    public partial class fghjkl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "carts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "carts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
