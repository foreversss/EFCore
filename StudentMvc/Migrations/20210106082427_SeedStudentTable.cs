using Microsoft.EntityFrameworkCore.Migrations;

namespace A.StudentMvc.Migrations
{
    public partial class SeedStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Grade", "Mailbox", "Name" },
                values: new object[] { 1, 1, "121432@qq.com", "哈哈哈哈哈哈哈" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
