using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class PostsAndCommentEdit00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body1",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Body2",
                table: "Posts",
                newName: "Body");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDateTime",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Posts",
                newName: "Body2");

            migrationBuilder.AddColumn<string>(
                name: "Body1",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
