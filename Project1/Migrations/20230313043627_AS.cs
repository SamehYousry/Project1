using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Migrations
{
    /// <inheritdoc />
    public partial class AS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Admin_AdminId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_AdminId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Services");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Services_AdminId",
                table: "Services",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Admin_AdminId",
                table: "Services",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "AdminId");
        }
    }
}
