using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradgardsgolf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OwnerGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "strOwnerEmail",
                table: "course");

            migrationBuilder.AddColumn<Guid>(
                name: "strOwnerGuid",
                table: "course",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "strOwnerGuid",
                table: "course");

            migrationBuilder.AddColumn<string>(
                name: "strOwnerEmail",
                table: "course",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
