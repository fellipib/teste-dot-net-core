using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_teste.Migrations
{
    public partial class updateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_BooksId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_BooksId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "BooksId",
                table: "Loans");

            migrationBuilder.AlterColumn<int>(
                name: "BookNSU",
                table: "Loans",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookId",
                table: "Loans",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Books_BookId",
                table: "Loans",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_BookId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_BookId",
                table: "Loans");

            migrationBuilder.AlterColumn<string>(
                name: "BookNSU",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BooksId",
                table: "Loans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BooksId",
                table: "Loans",
                column: "BooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Books_BooksId",
                table: "Loans",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
