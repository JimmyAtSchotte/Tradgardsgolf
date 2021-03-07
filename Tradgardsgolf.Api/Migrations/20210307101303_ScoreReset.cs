using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tradgardsgolf.Api.Migrations
{
    public partial class ScoreReset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime?>("dtmScoreReset", "course", nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("dtmScoreReset", "course");
        }
    }
}
