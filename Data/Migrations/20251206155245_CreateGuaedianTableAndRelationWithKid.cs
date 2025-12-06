using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuardingChild.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateGuaedianTableAndRelationWithKid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuardianId",
                table: "kids",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "guardians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SSN_Father = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Father_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SSN_Mother = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mother_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guardians", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_kids_GuardianId",
                table: "kids",
                column: "GuardianId");

            migrationBuilder.AddForeignKey(
                name: "FK_kids_guardians_GuardianId",
                table: "kids",
                column: "GuardianId",
                principalTable: "guardians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_kids_guardians_GuardianId",
                table: "kids");

            migrationBuilder.DropTable(
                name: "guardians");

            migrationBuilder.DropIndex(
                name: "IX_kids_GuardianId",
                table: "kids");

            migrationBuilder.DropColumn(
                name: "GuardianId",
                table: "kids");
        }
    }
}
