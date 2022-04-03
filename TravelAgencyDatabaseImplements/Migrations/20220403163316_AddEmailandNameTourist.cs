using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelAgencyDatabaseImplements.Migrations
{
    public partial class AddEmailandNameTourist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TouristLogin",
                table: "Trips",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Tourists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tourists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TouristLogin",
                table: "Places",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TouristLogin",
                table: "Excursions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TouristLogin",
                table: "Trips",
                column: "TouristLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Places_TouristLogin",
                table: "Places",
                column: "TouristLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_TouristLogin",
                table: "Excursions",
                column: "TouristLogin");

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_Tourists_TouristLogin",
                table: "Excursions",
                column: "TouristLogin",
                principalTable: "Tourists",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Tourists_TouristLogin",
                table: "Places",
                column: "TouristLogin",
                principalTable: "Tourists",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Tourists_TouristLogin",
                table: "Trips",
                column: "TouristLogin",
                principalTable: "Tourists",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_Tourists_TouristLogin",
                table: "Excursions");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_Tourists_TouristLogin",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Tourists_TouristLogin",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_TouristLogin",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Places_TouristLogin",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Excursions_TouristLogin",
                table: "Excursions");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Tourists");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tourists");

            migrationBuilder.AlterColumn<string>(
                name: "TouristLogin",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TouristLogin",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TouristLogin",
                table: "Excursions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
