using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiForLabs.Migrations
{
    /// <inheritdoc />
    public partial class ICollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Table2_Table1_Table1Id",
                table: "Table2");

            migrationBuilder.DropIndex(
                name: "IX_Table2_Table1Id",
                table: "Table2");

            migrationBuilder.DropColumn(
                name: "Table1Id",
                table: "Table2");

            migrationBuilder.AddColumn<int>(
                name: "Table2Id",
                table: "Table1",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Table1_Table2Id",
                table: "Table1",
                column: "Table2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Table1_Table2_Table2Id",
                table: "Table1",
                column: "Table2Id",
                principalTable: "Table2",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Table1_Table2_Table2Id",
                table: "Table1");

            migrationBuilder.DropIndex(
                name: "IX_Table1_Table2Id",
                table: "Table1");

            migrationBuilder.DropColumn(
                name: "Table2Id",
                table: "Table1");

            migrationBuilder.AddColumn<int>(
                name: "Table1Id",
                table: "Table2",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Table2_Table1Id",
                table: "Table2",
                column: "Table1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Table2_Table1_Table1Id",
                table: "Table2",
                column: "Table1Id",
                principalTable: "Table1",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
