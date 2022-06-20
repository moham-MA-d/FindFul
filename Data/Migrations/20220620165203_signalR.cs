using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class signalR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SignalRGroups",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignalRGroups", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "SignalRConnections",
                columns: table => new
                {
                    ConnectionId = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    SignalRGroupName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignalRConnections", x => x.ConnectionId);
                    table.ForeignKey(
                        name: "FK_SignalRConnections_SignalRGroups_SignalRGroupName",
                        column: x => x.SignalRGroupName,
                        principalTable: "SignalRGroups",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SignalRConnections_SignalRGroupName",
                table: "SignalRConnections",
                column: "SignalRGroupName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignalRConnections");

            migrationBuilder.DropTable(
                name: "SignalRGroups");
        }
    }
}
