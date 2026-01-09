using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetMatch.Migrations
{
    /// <inheritdoc />
    public partial class AddRescueDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Age",
                table: "Animal",
                type: "decimal(4,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)");

            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShelterID",
                table: "Animal",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Shelter",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelter", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AdoptionRequest",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnimalID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AdoptionRequest_Animal_AnimalID",
                        column: x => x.AnimalID,
                        principalTable: "Animal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdoptionRequest_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_ShelterID",
                table: "Animal",
                column: "ShelterID");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequest_AnimalID",
                table: "AdoptionRequest",
                column: "AnimalID");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequest_MemberID",
                table: "AdoptionRequest",
                column: "MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Shelter_ShelterID",
                table: "Animal",
                column: "ShelterID",
                principalTable: "Shelter",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Shelter_ShelterID",
                table: "Animal");

            migrationBuilder.DropTable(
                name: "AdoptionRequest");

            migrationBuilder.DropTable(
                name: "Shelter");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Animal_ShelterID",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Breed",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "ShelterID",
                table: "Animal");

            migrationBuilder.AlterColumn<decimal>(
                name: "Age",
                table: "Animal",
                type: "decimal(6,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)");
        }
    }
}
