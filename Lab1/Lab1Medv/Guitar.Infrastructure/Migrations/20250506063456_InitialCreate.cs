using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Guitar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    YearsExperience = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guitars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    StringCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ScaleLength = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<float>(type: "REAL", nullable: false),
                    PlayerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GuitarType = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    HasPiezo = table.Column<bool>(type: "INTEGER", nullable: true),
                    StringType = table.Column<int>(type: "INTEGER", nullable: true),
                    PickupCount = table.Column<int>(type: "INTEGER", nullable: true),
                    VibratoSystem = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guitars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guitars_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guitars_PlayerId",
                table: "Guitars",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guitars");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
