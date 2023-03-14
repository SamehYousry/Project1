using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Migrations
{
    /// <inheritdoc />
    public partial class SR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Report_ServiceId",
                table: "Report",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Services_ServiceId",
                table: "Report",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Services_ServiceId",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_ServiceId",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Report");
        }
    }
}
