using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TDSTecnologia.FaceAlbum.Web.Migrations
{
    public partial class CRIANDO_BANCO_DE_DADOS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb01_album",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    titulo = table.Column<string>(maxLength: 50, nullable: false),
                    descricao = table.Column<string>(maxLength: 50, nullable: false),
                    capa = table.Column<string>(nullable: true),
                    dt_inicio = table.Column<DateTime>(nullable: false),
                    dt_fim = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb01_album", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb02_imagem",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    link = table.Column<string>(nullable: true),
                    album = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb02_imagem", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb02_imagem_tb01_album_album",
                        column: x => x.album,
                        principalTable: "tb01_album",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb02_imagem_album",
                table: "tb02_imagem",
                column: "album");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb02_imagem");

            migrationBuilder.DropTable(
                name: "tb01_album");
        }
    }
}
