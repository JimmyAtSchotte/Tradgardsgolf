using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradgardsgolf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtmCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tournament",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tournament", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    intHoles = table.Column<int>(type: "int", nullable: false),
                    dblLongitude = table.Column<double>(type: "float", nullable: false),
                    dblLatitude = table.Column<double>(type: "float", nullable: false),
                    intCreatedBy = table.Column<int>(type: "int", nullable: false),
                    dtmCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtmScoreReset = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_course_player_intCreatedBy",
                        column: x => x.intCreatedBy,
                        principalTable: "player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "round",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intCourseId = table.Column<int>(type: "int", nullable: false),
                    dtmDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_round", x => x.Id);
                    table.ForeignKey(
                        name: "FK_round_course_intCourseId",
                        column: x => x.intCourseId,
                        principalTable: "course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tournamentcoursedate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intTournamentId = table.Column<int>(type: "int", nullable: false),
                    intCourseId = table.Column<int>(type: "int", nullable: false),
                    dtmDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "roundscore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intRoundId = table.Column<int>(type: "int", nullable: false),
                    intHole = table.Column<int>(type: "int", nullable: false),
                    intPlayerId = table.Column<int>(type: "int", nullable: false),
                    intScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roundscore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roundscore_player_intPlayerId",
                        column: x => x.intPlayerId,
                        principalTable: "player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roundscore_round_intRoundId",
                        column: x => x.intRoundId,
                        principalTable: "round",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tournamentround",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intTournamentId = table.Column<int>(type: "int", nullable: false),
                    intCourseId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_course_intCreatedBy",
                table: "course",
                column: "intCreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_round_intCourseId",
                table: "round",
                column: "intCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_roundscore_intPlayerId",
                table: "roundscore",
                column: "intPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_roundscore_intRoundId",
                table: "roundscore",
                column: "intRoundId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roundscore");

            migrationBuilder.DropTable(
                name: "tournamentcoursedate");

            migrationBuilder.DropTable(
                name: "tournamentround");

            migrationBuilder.DropTable(
                name: "round");

            migrationBuilder.DropTable(
                name: "tournament");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "player");
        }
    }
}
