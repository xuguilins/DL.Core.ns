using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace 徐测试控制台.Migrations
{
    public partial class iniddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherInfo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    TeacherName = table.Column<string>(maxLength: 50, nullable: true),
                    TeacherAdderss = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherInfo");
        }
    }
}
