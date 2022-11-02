using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuiThiKimNganBTH2.Migrations
{
    public partial class Create_Table_Employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Employees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Employees");
        }
    }
}
