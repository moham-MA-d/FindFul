using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddMessageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRecieved = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateReaded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SnderDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RecieverDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    SenderUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TheSenderId = table.Column<int>(type: "int", nullable: true),
                    RecieverId = table.Column<int>(type: "int", nullable: false),
                    RecieverUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TheRecieverId = table.Column<int>(type: "int", nullable: true),
                    GuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_TheRecieverId",
                        column: x => x.TheRecieverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_TheSenderId",
                        column: x => x.TheSenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TheRecieverId",
                table: "Messages",
                column: "TheRecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TheSenderId",
                table: "Messages",
                column: "TheSenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
