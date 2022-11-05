using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuiThiKimNganBTH2.Migrations
{
    public partial class Creae_Table_Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Customers");
        }
    }
}
