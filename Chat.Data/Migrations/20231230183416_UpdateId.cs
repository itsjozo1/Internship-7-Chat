using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chat.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers");

            migrationBuilder.AddColumn<int>(
                name: "GroupUserId",
                table: "GroupUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers",
                column: "GroupUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_UserId",
                table: "GroupUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers");

            migrationBuilder.DropIndex(
                name: "IX_GroupUsers_UserId",
                table: "GroupUsers");

            migrationBuilder.DropColumn(
                name: "GroupUserId",
                table: "GroupUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers",
                columns: new[] { "UserId", "GroupId" });
        }
    }
}
