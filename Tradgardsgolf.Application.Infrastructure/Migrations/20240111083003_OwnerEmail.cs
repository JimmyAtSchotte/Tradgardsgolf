using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradgardsgolf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OwnerEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_course_player_intCreatedBy",
                table: "course");

            migrationBuilder.DropIndex(
                name: "IX_course_intCreatedBy",
                table: "course");

            migrationBuilder.DropColumn(
                name: "intCreatedBy",
                table: "course");

            migrationBuilder.AddColumn<string>(
                name: "strOwnerEmail",
                table: "course",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "strOwnerEmail",
                table: "course");

            migrationBuilder.AddColumn<int>(
                name: "intCreatedBy",
                table: "course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_course_intCreatedBy",
                table: "course",
                column: "intCreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_course_player_intCreatedBy",
                table: "course",
                column: "intCreatedBy",
                principalTable: "player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
