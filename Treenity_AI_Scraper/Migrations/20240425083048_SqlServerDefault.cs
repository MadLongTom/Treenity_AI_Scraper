using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Treenity_AI_Scraper.Migrations
{
    /// <inheritdoc />
    public partial class SqlServerDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnswerStores",
                columns: table => new
                {
                    questionId = table.Column<long>(type: "bigint", nullable: false),
                    answers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerStores", x => x.questionId);
                });

            migrationBuilder.CreateTable(
                name: "AppRuntimeConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lastGetTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRuntimeConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    courseIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    coursdIds = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cookie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CookieExpired = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entityStoreId = table.Column<long>(type: "bigint", nullable: false),
                    channel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    finished = table.Column<bool>(type: "bit", nullable: false),
                    orderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    startTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    finishTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Entities_entityStoreId",
                        column: x => x.entityStoreId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "Id", "coursdIds", "courseIds" },
                values: new object[,]
                {
                    { 1L, "[4000001593]", "[4000001593]" },
                    { 2L, "[4000001588,4000001589]", "[4000001588,4000001589]" },
                    { 3L, "[4000001598]", "[4000001598]" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_entityStoreId",
                table: "Tickets",
                column: "entityStoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerStores");

            migrationBuilder.DropTable(
                name: "AppRuntimeConfig");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Entities");
        }
    }
}
