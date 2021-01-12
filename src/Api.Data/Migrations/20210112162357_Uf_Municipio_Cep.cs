using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Uf_Municipio_Cep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("dd89045c-fa9b-4662-923c-829b575992f4"));

            migrationBuilder.CreateTable(
                name: "uf",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: false),
                    Sigla = table.Column<string>(maxLength: 2, nullable: false),
                    Nome = table.Column<string>(maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "municipio",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(maxLength: 60, nullable: false),
                    CodIBGE = table.Column<int>(nullable: false),
                    UfId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_municipio_uf_UfId",
                        column: x => x.UfId,
                        principalTable: "uf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cep",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: false),
                    Cep = table.Column<string>(maxLength: 10, nullable: false),
                    Logradouro = table.Column<string>(maxLength: 60, nullable: false),
                    Numero = table.Column<string>(maxLength: 10, nullable: true),
                    MunicipioId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cep_municipio_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateAt", "Email", "Name", "UpdateAt" },
                values: new object[] { new Guid("182ea703-91d5-45b6-9c21-d35221fc1e2c"), new DateTime(2021, 1, 12, 16, 23, 57, 19, DateTimeKind.Utc).AddTicks(7760), "adm@mail.com", "Administrador", new DateTime(2021, 1, 12, 16, 23, 57, 19, DateTimeKind.Utc).AddTicks(8856) });

            migrationBuilder.InsertData(
                table: "uf",
                columns: new[] { "Id", "CreateAt", "Nome", "Sigla", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2392), "São Paulo", "SP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fe8ca516-034f-4249-bc5a-31c85ef220ea"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2389), "Sergipe", "SE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b81f95e0-f226-4afd-9763-290001637ed4"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2386), "Santa Catarina", "SC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("88970a32-3a2a-4a95-8a18-2087b65f59d1"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2384), "Rio Grande do Sul", "RS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9fd3c97a-dc68-4af5-bc65-694cca0f2869"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2381), "Roraima", "RR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("924e7250-7d39-4e8b-86bf-a8578cbf4002"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2379), "Rondônia", "RO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("542668d1-50ba-4fca-bbc3-4b27af108ea3"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2377), "Rio Grande do Norte", "RN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2374), "Rio de Janeiro", "RJ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("1dd25850-6270-48f8-8b77-2f0f079480ab"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2371), "Paraná", "PR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f85a6cd0-2237-46b1-a103-d3494ab27774"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2369), "Piauí", "PI", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ad5969bd-82dc-4e23-ace2-d8495935dd2e"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2366), "Pernambuco", "PE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("1109ab04-a3a5-476e-bdce-6c3e2c2badee"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2364), "Paraíba", "PB", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8411e9bc-d3b2-4a9b-9d15-78633d64fc7c"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2361), "Pará", "PA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("29eec4d3-b061-427d-894f-7f0fecc7f65f"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2359), "Mato Grosso", "MT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3739969c-fd8a-4411-9faa-3f718ca85e70"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2356), "Mato Grosso do Sul", "MS", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("27f7a92b-1979-4e1c-be9d-cd3bb73552a8"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2353), "Minas Gerais", "MG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("57a9e9f7-9aea-40fe-a783-65d4feb59fa8"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2350), "Maranhão", "MA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("837a64d3-c649-4172-a4e0-2b20d3c85224"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2347), "Goiás", "GO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c623f804-37d8-4a19-92c1-67fd162862e6"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2260), "Espírito Santo", "ES", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bd08208b-bfca-47a4-9cd0-37e4e1fa5006"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2258), "Distrito Federal", "DF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5ff1b59e-11e7-414d-827e-609dc5f7e333"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2255), "Ceará", "CE", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5abca453-d035-4766-a81b-9f73d683a54b"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2253), "Bahia", "BA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("409b9043-88a4-4e86-9cca-ca1fb0d0d35b"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2250), "Amapá", "AP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cb9e6888-2094-45ee-bc44-37ced33c693a"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2247), "Amazonas", "AM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7cc33300-586e-4be8-9a4d-bd9f01ee9ad8"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2242), "Alagoas", "AL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("971dcb34-86ea-4f92-989d-064f749e23c9"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2394), "Tocantins", "TO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("22ffbd18-cdb9-45cc-97b0-51e97700bf71"), new DateTime(2021, 1, 12, 16, 23, 57, 22, DateTimeKind.Utc).AddTicks(2192), "Acre", "AC", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cep_Cep",
                table: "Cep",
                column: "Cep");

            migrationBuilder.CreateIndex(
                name: "IX_Cep_MunicipioId",
                table: "Cep",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_municipio_CodIBGE",
                table: "municipio",
                column: "CodIBGE");

            migrationBuilder.CreateIndex(
                name: "IX_municipio_UfId",
                table: "municipio",
                column: "UfId");

            migrationBuilder.CreateIndex(
                name: "IX_uf_Sigla",
                table: "uf",
                column: "Sigla",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cep");

            migrationBuilder.DropTable(
                name: "municipio");

            migrationBuilder.DropTable(
                name: "uf");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("182ea703-91d5-45b6-9c21-d35221fc1e2c"));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateAt", "Email", "Name", "UpdateAt" },
                values: new object[] { new Guid("dd89045c-fa9b-4662-923c-829b575992f4"), new DateTime(2021, 1, 6, 17, 55, 5, 958, DateTimeKind.Utc).AddTicks(8109), "adm@mail.com", "Administrador", new DateTime(2021, 1, 6, 17, 55, 5, 958, DateTimeKind.Utc).AddTicks(9249) });
        }
    }
}
