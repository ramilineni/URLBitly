using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserRegistration.Migrations
{
    /// <inheritdoc />
    public partial class AddSubmissionDateToPageExpiration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BitlyUrl",
                table: "PageExpirations",
                newName: "FormUrl");

            migrationBuilder.AlterColumn<string>(
                name: "PageGuid",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SubmissionDate",
                table: "Patients",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmissionDate",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "FormUrl",
                table: "PageExpirations",
                newName: "BitlyUrl");

            migrationBuilder.AlterColumn<Guid>(
                name: "PageGuid",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
