using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RobinWeb.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class migAddBloggerToBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Blogger",
                table: "BlogsTbl",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BlogsTbl",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BlogsTbl_UserId",
                table: "BlogsTbl",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogsTbl_UsersTbl_UserId",
                table: "BlogsTbl",
                column: "UserId",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogsTbl_UsersTbl_UserId",
                table: "BlogsTbl");

            migrationBuilder.DropIndex(
                name: "IX_BlogsTbl_UserId",
                table: "BlogsTbl");

            migrationBuilder.DropColumn(
                name: "Blogger",
                table: "BlogsTbl");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BlogsTbl");
        }
    }
}
