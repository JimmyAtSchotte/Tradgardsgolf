using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Tradgardsgolf.Api.Migrations
{
    public partial class Tournaments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tournament",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    strName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tournament", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tournamentcoursedate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    intTournamentId = table.Column<int>(type: "integer", nullable: false),
                    intCourseId = table.Column<int>(type: "integer", nullable: false),
                    dtmDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tournamentcoursedate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tournamentcoursedate_course_intCourseId",
                        column: x => x.intCourseId,
                        principalTable: "course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tournamentcoursedate_tournament_intTournamentId",
                        column: x => x.intTournamentId,
                        principalTable: "tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tournamentround",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    intTournamentId = table.Column<int>(type: "integer", nullable: false),
                    intCourseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tournamentround", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tournamentround_round_intCourseId",
                        column: x => x.intCourseId,
                        principalTable: "round",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tournamentround_tournament_intTournamentId",
                        column: x => x.intTournamentId,
                        principalTable: "tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tournamentcoursedate_intCourseId",
                table: "tournamentcoursedate",
                column: "intCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tournamentcoursedate_intTournamentId",
                table: "tournamentcoursedate",
                column: "intTournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_tournamentround_intCourseId",
                table: "tournamentround",
                column: "intCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tournamentround_intTournamentId",
                table: "tournamentround",
                column: "intTournamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tournamentcoursedate");

            migrationBuilder.DropTable(
                name: "tournamentround");

            migrationBuilder.DropTable(
                name: "tournament");
        }
    }
}
