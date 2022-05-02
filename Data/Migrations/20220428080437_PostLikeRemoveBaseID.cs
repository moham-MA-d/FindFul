using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class PostLikeRemoveBaseID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuId",
                table: "PostsLiked");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PostsLiked");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GuId",
                table: "PostsLiked",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PostsLiked",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
