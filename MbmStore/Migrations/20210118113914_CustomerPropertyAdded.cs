using Microsoft.EntityFrameworkCore.Migrations;

namespace MbmStore.Migrations
{
    public partial class CustomerPropertyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isNewCustomer",
                table: "Customer",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isNewCustomer",
                table: "Customer");
        }
    }
}
