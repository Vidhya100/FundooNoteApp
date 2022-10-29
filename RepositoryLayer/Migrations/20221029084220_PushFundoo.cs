using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class PushFundoo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usertable",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usertable", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "NotesTable",
                columns: table => new
                {
                    NoteID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Reminder = table.Column<DateTime>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Archive = table.Column<bool>(nullable: false),
                    Pin = table.Column<bool>(nullable: false),
                    Trash = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Edited = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesTable", x => x.NoteID);
                    table.ForeignKey(
                        name: "FK_NotesTable_Usertable_UserId",
                        column: x => x.UserId,
                        principalTable: "Usertable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotesTable_UserId",
                table: "NotesTable",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotesTable");

            migrationBuilder.DropTable(
                name: "Usertable");
        }
    }
}
