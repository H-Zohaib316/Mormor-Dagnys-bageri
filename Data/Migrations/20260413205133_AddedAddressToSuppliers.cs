using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MormorBageri.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAddressToSuppliers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Suppliers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Suppliers");
        }
    }
}
