using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    public partial class AddBonusColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "bonus",
                table: "employee",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "bonus",
                table: "client",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_account_client_id",
                table: "account",
                column: "client_id");

            migrationBuilder.AddForeignKey(
                name: "FK_account_client_client_id",
                table: "account",
                column: "client_id",
                principalTable: "client",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_account_client_client_id",
                table: "account");

            migrationBuilder.DropIndex(
                name: "IX_account_client_id",
                table: "account");

            migrationBuilder.DropColumn(
                name: "bonus",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "bonus",
                table: "client");
        }
    }
}
