using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace web_programlama.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salonlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    CalismaSaatleri = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salonlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    UzmanlikAlani = table.Column<string>(type: "text", nullable: false),
                    UygunlukSaatleri = table.Column<string>(type: "text", nullable: false),
                    SalonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calisanlar_Salonlar_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salonlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Islemler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    Sure = table.Column<int>(type: "integer", nullable: false),
                    Ucret = table.Column<decimal>(type: "numeric", nullable: false),
                    SalonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Islemler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Islemler_Salonlar_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salonlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalisanIslem",
                columns: table => new
                {
                    CalisanlarId = table.Column<int>(type: "integer", nullable: false),
                    IslemlerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanIslem", x => new { x.CalisanlarId, x.IslemlerId });
                    table.ForeignKey(
                        name: "FK_CalisanIslem_Calisanlar_CalisanlarId",
                        column: x => x.CalisanlarId,
                        principalTable: "Calisanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalisanIslem_Islemler_IslemlerId",
                        column: x => x.IslemlerId,
                        principalTable: "Islemler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RandevuZamani = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IslemId = table.Column<int>(type: "integer", nullable: false),
                    CalisanId = table.Column<int>(type: "integer", nullable: false),
                    KullaniciId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Randevular_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevular_Islemler_IslemId",
                        column: x => x.IslemId,
                        principalTable: "Islemler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalisanIslem_IslemlerId",
                table: "CalisanIslem",
                column: "IslemlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_SalonId",
                table: "Calisanlar",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Islemler_SalonId",
                table: "Islemler",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_CalisanId",
                table: "Randevular",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_IslemId",
                table: "Randevular",
                column: "IslemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalisanIslem");

            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "Calisanlar");

            migrationBuilder.DropTable(
                name: "Islemler");

            migrationBuilder.DropTable(
                name: "Salonlar");
        }
    }
}
