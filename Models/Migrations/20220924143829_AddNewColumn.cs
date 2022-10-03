using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    public partial class AddNewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_client_clientId",
                table: "Account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_clientId",
                table: "Account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_client",
                table: "client");

            migrationBuilder.RenameTable(
                name: "client",
                newName: "Сlient");

            migrationBuilder.RenameColumn(
                name: "clientId",
                table: "Account",
                newName: "client_id");

            migrationBuilder.AlterColumn<string>(
                name: "currency_name",
                table: "Account",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "client_id",
                table: "Account",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "Account",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Сlient",
                table: "Сlient",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Сlient",
                table: "Сlient");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Сlient",
                newName: "client");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "Account",
                newName: "clientId");

            migrationBuilder.AlterColumn<string>(
                name: "currency_name",
                table: "Account",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "clientId",
                table: "Account",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "currency_name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_client",
                table: "client",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Account_clientId",
                table: "Account",
                column: "clientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_client_clientId",
                table: "Account",
                column: "clientId",
                principalTable: "client",
                principalColumn: "id");
        }
    }
}
