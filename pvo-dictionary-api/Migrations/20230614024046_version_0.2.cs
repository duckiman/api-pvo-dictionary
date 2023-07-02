using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pvo_dictionary_api.Migrations
{
    public partial class version_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "otp",
                table: "users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2023, 6, 14, 2, 40, 46, 111, DateTimeKind.Utc).AddTicks(9610));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "otp",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "user_id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2023, 6, 14, 2, 31, 58, 380, DateTimeKind.Utc).AddTicks(4214));
        }
    }
}
