using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1.Migrations
{
    /// <inheritdoc />
    public partial class newprovider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Providers");

            migrationBuilder.RenameColumn(
                name: "VerificationCode",
                table: "Providers",
                newName: "Service");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Providers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Providers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpires",
                table: "Providers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpires",
                table: "Providers");

            migrationBuilder.RenameColumn(
                name: "Service",
                table: "Providers",
                newName: "VerificationCode");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
