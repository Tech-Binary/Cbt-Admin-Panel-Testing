using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CbtAdminPanel.Migrations
{
    /// <inheritdoc />
    public partial class ASSignRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormPages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FormPages",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "FormId", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 8, 9, 18, 25, 42, 822, DateTimeKind.Local).AddTicks(5582), 101, "ProjectMaster" },
                    { 2, 1, new DateTime(2024, 8, 9, 18, 25, 42, 822, DateTimeKind.Local).AddTicks(5584), 102, "LocationMaster" },
                    { 3, 1, new DateTime(2024, 8, 9, 18, 25, 42, 822, DateTimeKind.Local).AddTicks(5586), 103, "ModuleMaster" },
                    { 4, 1, new DateTime(2024, 8, 9, 18, 25, 42, 822, DateTimeKind.Local).AddTicks(5587), 104, "Role" },
                    { 5, 1, new DateTime(2024, 8, 9, 18, 25, 42, 822, DateTimeKind.Local).AddTicks(5589), 105, "USER" },
                    { 6, 1, new DateTime(2024, 8, 9, 18, 25, 42, 822, DateTimeKind.Local).AddTicks(5590), 106, "USERPersmission" },
                    { 7, 1, new DateTime(2024, 8, 9, 18, 25, 42, 822, DateTimeKind.Local).AddTicks(5592), 107, "AssignLoaction" },
                    { 8, 1, new DateTime(2024, 8, 9, 18, 25, 42, 822, DateTimeKind.Local).AddTicks(5593), 108, "LocationSeries" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 9, 18, 25, 42, 822, DateTimeKind.Local).AddTicks(5423));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 9, 18, 25, 42, 822, DateTimeKind.Local).AddTicks(5554));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormPages");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 8, 17, 40, 15, 506, DateTimeKind.Local).AddTicks(2276));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 8, 8, 17, 40, 15, 506, DateTimeKind.Local).AddTicks(2600));
        }
    }
}
