using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InMindLab1.Migrations.Librarydb
{
    /// <inheritdoc />
    public partial class AlteredTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("authors_pkey", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "borrowers",
                columns: table => new
                {
                    borrower_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("borrowers_pkey", x => x.borrower_id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    isbn = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    published_year = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("books_pkey", x => x.book_id);
                    table.ForeignKey(
                        name: "fk_author",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "author_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loans",
                columns: table => new
                {
                    loan_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    book_id = table.Column<int>(type: "integer", nullable: false),
                    borrower_id = table.Column<int>(type: "integer", nullable: false),
                    loan_date = table.Column<DateOnly>(type: "date", nullable: false),
                    return_date = table.Column<DateOnly>(type: "date", nullable: true),
                    returned = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("loans_pkey", x => x.loan_id);
                    table.ForeignKey(
                        name: "fk_book",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_borrower",
                        column: x => x.borrower_id,
                        principalTable: "borrowers",
                        principalColumn: "borrower_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "books_isbn_key",
                table: "books",
                column: "isbn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_books_author_id",
                table: "books",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "borrowers_email_key",
                table: "borrowers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "borrowers_phone_key",
                table: "borrowers",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_loans_book_id",
                table: "loans",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_loans_borrower_id",
                table: "loans",
                column: "borrower_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "loans");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "borrowers");

            migrationBuilder.DropTable(
                name: "authors");
        }
    }
}
