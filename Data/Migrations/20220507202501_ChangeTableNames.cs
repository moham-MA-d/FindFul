using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ChangeTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AppUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_TheRecieverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_TheSenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AppUserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsLiked_AspNetUsers_AppUserId",
                table: "PostsLiked");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPhotos_AspNetUsers_AppUserId",
                table: "UserPhotos");

            migrationBuilder.DropIndex(
                name: "IX_Messages_TheRecieverId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_TheSenderId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AppUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TheRecieverId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "TheSenderId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "UserPhotos",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPhotos_AppUserId",
                table: "UserPhotos",
                newName: "IX_UserPhotos_UserId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "PostsLiked",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PostsLiked_AppUserId",
                table: "PostsLiked",
                newName: "IX_PostsLiked_UserId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Posts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_AppUserId",
                table: "Posts",
                newName: "IX_Posts_UserId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "RecieverId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TheAppUserId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecieverId",
                table: "Messages",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TheAppUserId",
                table: "Comments",
                column: "TheAppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_TheAppUserId",
                table: "Comments",
                column: "TheAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_RecieverId",
                table: "Messages",
                column: "RecieverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostsLiked_AspNetUsers_UserId",
                table: "PostsLiked",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhotos_AspNetUsers_UserId",
                table: "UserPhotos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_TheAppUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_RecieverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsLiked_AspNetUsers_UserId",
                table: "PostsLiked");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPhotos_AspNetUsers_UserId",
                table: "UserPhotos");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RecieverId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SenderId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TheAppUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "RecieverId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "TheAppUserId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserPhotos",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPhotos_UserId",
                table: "UserPhotos",
                newName: "IX_UserPhotos_AppUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PostsLiked",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PostsLiked_UserId",
                table: "PostsLiked",
                newName: "IX_PostsLiked_AppUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Posts",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                newName: "IX_Posts_AppUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "AppUserId");

            migrationBuilder.AddColumn<int>(
                name: "TheRecieverId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TheSenderId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TheRecieverId",
                table: "Messages",
                column: "TheRecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TheSenderId",
                table: "Messages",
                column: "TheSenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AppUserId",
                table: "Comments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AppUserId",
                table: "Comments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_TheRecieverId",
                table: "Messages",
                column: "TheRecieverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_TheSenderId",
                table: "Messages",
                column: "TheSenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AppUserId",
                table: "Posts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostsLiked_AspNetUsers_AppUserId",
                table: "PostsLiked",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhotos_AspNetUsers_AppUserId",
                table: "UserPhotos",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
