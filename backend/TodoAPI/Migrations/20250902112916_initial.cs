using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDone = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "CreatedDate", "Description", "DueDate", "IsDone", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "this is the first item in the todo list.", new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Todo item number one" },
                    { 2, new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "this is the second item in the todo list.", new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Second todo item" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_DueDate",
                table: "Todos",
                column: "DueDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");
        }
    }
}
