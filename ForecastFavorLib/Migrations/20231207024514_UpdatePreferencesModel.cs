using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForecastFavorApp.Migrations
{
    public partial class UpdatePreferencesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationTriggers",
                table: "Preferences");

            migrationBuilder.AddColumn<bool>(
                name: "NotifyOnClouds",
                table: "Preferences",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotifyOnRain",
                table: "Preferences",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotifyOnSnow",
                table: "Preferences",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotifyOnSun",
                table: "Preferences",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotifyOnClouds",
                table: "Preferences");

            migrationBuilder.DropColumn(
                name: "NotifyOnRain",
                table: "Preferences");

            migrationBuilder.DropColumn(
                name: "NotifyOnSnow",
                table: "Preferences");

            migrationBuilder.DropColumn(
                name: "NotifyOnSun",
                table: "Preferences");

            migrationBuilder.AddColumn<string>(
                name: "NotificationTriggers",
                table: "Preferences",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
