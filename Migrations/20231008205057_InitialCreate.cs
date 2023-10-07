using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MovieTicketer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "buyer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    dni = table.Column<string>(type: "text", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_buyer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movie",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    format = table.Column<int>(type: "integer", nullable: false),
                    categories = table.Column<int[]>(type: "integer[]", nullable: false),
                    rate = table.Column<int>(type: "integer", nullable: false),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movie", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "room",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rows_amount = table.Column<int>(type: "integer", nullable: false),
                    columns_amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "starrer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_starrer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "show",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    is_premiering = table.Column<bool>(type: "boolean", nullable: false),
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_id = table.Column<int>(type: "integer", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_show", x => x.id);
                    table.ForeignKey(
                        name: "fk_show_movie_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_show_room_room_id",
                        column: x => x.room_id,
                        principalTable: "room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie_starrer",
                columns: table => new
                {
                    movies_id = table.Column<Guid>(type: "uuid", nullable: false),
                    starrers_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movie_starrer", x => new { x.movies_id, x.starrers_id });
                    table.ForeignKey(
                        name: "fk_movie_starrer_movie_movies_id",
                        column: x => x.movies_id,
                        principalTable: "movie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_movie_starrer_starrer_starrers_id",
                        column: x => x.starrers_id,
                        principalTable: "starrer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    show_id = table.Column<Guid>(type: "uuid", nullable: false),
                    buyer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    row_identifier = table.Column<char>(type: "character(1)", nullable: false),
                    column_identifier = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_buyer_buyer_id",
                        column: x => x.buyer_id,
                        principalTable: "buyer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ticket_show_show_id",
                        column: x => x.show_id,
                        principalTable: "show",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_movie_starrer_starrers_id",
                table: "movie_starrer",
                column: "starrers_id");

            migrationBuilder.CreateIndex(
                name: "ix_show_movie_id",
                table: "show",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "ix_show_room_id",
                table: "show",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_buyer_id",
                table: "ticket",
                column: "buyer_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_show_id",
                table: "ticket",
                column: "show_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movie_starrer");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "starrer");

            migrationBuilder.DropTable(
                name: "buyer");

            migrationBuilder.DropTable(
                name: "show");

            migrationBuilder.DropTable(
                name: "movie");

            migrationBuilder.DropTable(
                name: "room");
        }
    }
}
