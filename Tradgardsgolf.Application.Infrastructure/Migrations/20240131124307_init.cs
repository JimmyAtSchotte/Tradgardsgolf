using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradgardsgolf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Holes = table.Column<int>(type: "int", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScoreReset = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tournament", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "round",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_round", x => x.Id);
                    table.ForeignKey(
                        name: "FK_round_course_CourseId",
                        column: x => x.CourseId,
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
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tournamentcoursedate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tournamentcoursedate_course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tournamentcoursedate_tournament_TournamentId",
                        column: x => x.TournamentId,
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
                    RoundId = table.Column<int>(type: "int", nullable: false),
                    Hole = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roundscore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roundscore_player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roundscore_round_RoundId",
                        column: x => x.RoundId,
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
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    RoundId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tournamentround", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tournamentround_round_RoundId",
                        column: x => x.RoundId,
                        principalTable: "round",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tournamentround_tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_round_CourseId",
                table: "round",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_roundscore_PlayerId",
                table: "roundscore",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_roundscore_RoundId",
                table: "roundscore",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_tournamentcoursedate_CourseId",
                table: "tournamentcoursedate",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tournamentcoursedate_TournamentId",
                table: "tournamentcoursedate",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_tournamentround_RoundId",
                table: "tournamentround",
                column: "RoundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tournamentround_TournamentId",
                table: "tournamentround",
                column: "TournamentId");
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
                name: "player");

            migrationBuilder.DropTable(
                name: "round");

            migrationBuilder.DropTable(
                name: "tournament");

            migrationBuilder.DropTable(
                name: "course");
        }
    }
}
