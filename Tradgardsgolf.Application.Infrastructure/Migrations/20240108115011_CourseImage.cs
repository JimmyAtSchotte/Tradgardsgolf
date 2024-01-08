using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradgardsgolf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CourseImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "strImage",
                table: "course",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "strImage",
                table: "course");
        }
    }
}
