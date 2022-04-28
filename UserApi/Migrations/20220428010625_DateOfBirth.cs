using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.Migrations
{
    public partial class DateOfBirth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e21c353c-2715-48eb-abdd-a47a79b75944");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b09bee4a-f3a1-489c-bc74-696d117216d2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0ce8826-a3ac-42a6-a08a-5204ed94406e", "AQAAAAEAACcQAAAAEAfsmfRpZtq/Ipdv4TAz8NKfl3WwpZAE6TdYO4bxFQ4lTT3/seSvwzlR5W+527Brkg==", "9dd8b1f3-58cc-4f77-9887-a7f50dcc3135" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "75f52f21-d448-4f0b-8395-92930343e483");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "129cc750-bf65-47d4-972f-968b77316ed5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4cc0937-4030-4e2f-af35-63ef6ef6a52f", "AQAAAAEAACcQAAAAEAUKlCvrv5txGLWaonJcO+KR2Nfi6H4o3IMHj35UvFBiqsQLz4fCmEN2o5bJp5IJyQ==", "ef48d369-0669-41b1-9ce9-dd41d7de9766" });
        }
    }
}
