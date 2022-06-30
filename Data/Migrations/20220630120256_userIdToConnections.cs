using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class userIdToConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "SignalRConnections",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SignalRConnections",
                newName: "Username");
        }
    }
}
