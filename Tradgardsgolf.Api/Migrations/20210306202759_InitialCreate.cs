using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Tradgardsgolf.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    strEmail = table.Column<string>(type: "text", nullable: true),
                    strPassword = table.Column<string>(type: "text", nullable: true),
                    strKey = table.Column<string>(type: "text", nullable: true),
                    strName = table.Column<string>(type: "text", nullable: true),
                    dtmCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    strName = table.Column<string>(type: "text", nullable: true),
                    intHoles = table.Column<int>(type: "integer", nullable: false),
                    dblLongitude = table.Column<double>(type: "double precision", nullable: false),
                    dblLatitude = table.Column<double>(type: "double precision", nullable: false),
                    intCreatedBy = table.Column<int>(type: "integer", nullable: false),
                    dtmCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    intCourseId = table.Column<int>(type: "integer", nullable: false),
                    dtmDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                name: "roundscore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    intRoundId = table.Column<int>(type: "integer", nullable: false),
                    intHole = table.Column<int>(type: "integer", nullable: false),
                    intPlayerId = table.Column<int>(type: "integer", nullable: false),
                    intScore = table.Column<int>(type: "integer", nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roundscore");

            migrationBuilder.DropTable(
                name: "round");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "player");
        }
    }
}
