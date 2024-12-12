using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace citybuilder_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateManyMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGames");

            migrationBuilder.AddColumn<string>(
                name: "usersId",
                table: "GameFields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "usersId",
                table: "GameFields");

            migrationBuilder.CreateTable(
                name: "UserGames",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GameFieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGames", x => new { x.UserId, x.GameFieldId });
                    table.ForeignKey(
                        name: "FK_UserGames_GameFields_GameFieldId",
                        column: x => x.GameFieldId,
                        principalTable: "GameFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGames_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGames_GameFieldId",
                table: "UserGames",
                column: "GameFieldId");
        }
    }
}
