using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMDB.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class actors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Actors_ActorId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_ActorId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "Ratings");

            migrationBuilder.AlterColumn<int>(
                name: "Movies_Capacity",
                table: "Actors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Actors");

            migrationBuilder.AddColumn<int>(
                name: "ActorId",
                table: "Ratings",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Movies_Capacity",
                table: "Actors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ActorId",
                table: "Ratings",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Actors_ActorId",
                table: "Ratings",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id");
        }
    }
}
