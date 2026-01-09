using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetMatch.Migrations
{
    /// <inheritdoc />
    public partial class AddCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Animal",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_CategoryID",
                table: "Animal",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Category_CategoryID",
                table: "Animal",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Category_CategoryID",
                table: "Animal");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Animal_CategoryID",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Animal");
        }
    }
}
