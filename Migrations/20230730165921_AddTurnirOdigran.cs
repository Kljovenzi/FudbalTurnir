using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FudbalskiTurnir_FilipNikolic.Migrations
{
    public partial class AddTurnirOdigran : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TurnirOdigran",
                table: "Turniri",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TurnirOdigran",
                table: "Turniri");
        }
    }
}
