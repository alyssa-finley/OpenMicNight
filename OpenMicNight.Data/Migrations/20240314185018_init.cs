using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenMicNight.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Performance",
                columns: table => new
                {
                    PerformerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerformerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datetime = table.Column<DateTime>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performance", x => x.PerformerId);
                });

            migrationBuilder.CreateTable(
                name: "Music",
                columns: table => new
                {
                    MusicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformerId = table.Column<int>(type: "int", nullable: false),
                    PerformerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Music", x => x.MusicId);
                    table.ForeignKey(
                        name: "FK_Music_Performance_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Performance",
                        principalColumn: "PerformerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformerId = table.Column<int>(type: "int", nullable: false),
                    MusicId = table.Column<int>(type: "int", nullable: false),
                    SongName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOriginal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song", x => x.SongId);
                    table.ForeignKey(
                        name: "FK_Song_Music_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Music",
                        principalColumn: "MusicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Song_Performance_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Performance",
                        principalColumn: "PerformerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Music_PerformerId",
                table: "Music",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_Song_MusicId",
                table: "Song",
                column: "MusicId");

            migrationBuilder.CreateIndex(
                name: "IX_Song_PerformerId",
                table: "Song",
                column: "PerformerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropTable(
                name: "Music");

            migrationBuilder.DropTable(
                name: "Performance");
        }
    }
}
