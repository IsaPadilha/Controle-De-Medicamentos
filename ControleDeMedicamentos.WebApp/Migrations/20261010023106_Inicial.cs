using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeMedicamentos.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBFornecedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBFornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBFuncionario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBFuncionario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBPaciente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CartaoSus = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBPaciente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBMedicamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMedicamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBMedicamento_TBFornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "TBFornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBRequisicaoEntrada",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    FuncionarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicamentoId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBRequisicaoEntrada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBRequisicaoEntrada_TBFuncionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "TBFuncionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBRequisicaoEntrada_TBMedicamento_MedicamentoId",
                        column: x => x.MedicamentoId,
                        principalTable: "TBMedicamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBRequisicaoEntrada_TBMedicamento_MedicamentoId1",
                        column: x => x.MedicamentoId1,
                        principalTable: "TBMedicamento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TBRequisicaoSaida",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBRequisicaoSaida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBRequisicaoSaida_TBMedicamento_MedicamentoId",
                        column: x => x.MedicamentoId,
                        principalTable: "TBMedicamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TBRequisicaoSaida_TBPaciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "TBPaciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicamentoPrescrito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    RequisicaoSaidaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicamentoPrescrito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicamentoPrescrito_TBMedicamento_MedicamentoId",
                        column: x => x.MedicamentoId,
                        principalTable: "TBMedicamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicamentoPrescrito_TBRequisicaoSaida_RequisicaoSaidaId",
                        column: x => x.RequisicaoSaidaId,
                        principalTable: "TBRequisicaoSaida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentoPrescrito_MedicamentoId",
                table: "MedicamentoPrescrito",
                column: "MedicamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentoPrescrito_RequisicaoSaidaId",
                table: "MedicamentoPrescrito",
                column: "RequisicaoSaidaId");

            migrationBuilder.CreateIndex(
                name: "IX_TBFornecedor_Cnpj",
                table: "TBFornecedor",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBFuncionario_Cpf",
                table: "TBFuncionario",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBMedicamento_FornecedorId",
                table: "TBMedicamento",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_TBMedicamento_Nome",
                table: "TBMedicamento",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBPaciente_CartaoSus",
                table: "TBPaciente",
                column: "CartaoSus",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBPaciente_Cpf",
                table: "TBPaciente",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBRequisicaoEntrada_FuncionarioId",
                table: "TBRequisicaoEntrada",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TBRequisicaoEntrada_MedicamentoId",
                table: "TBRequisicaoEntrada",
                column: "MedicamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_TBRequisicaoEntrada_MedicamentoId1",
                table: "TBRequisicaoEntrada",
                column: "MedicamentoId1");

            migrationBuilder.CreateIndex(
                name: "IX_TBRequisicaoSaida_MedicamentoId",
                table: "TBRequisicaoSaida",
                column: "MedicamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_TBRequisicaoSaida_PacienteId",
                table: "TBRequisicaoSaida",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicamentoPrescrito");

            migrationBuilder.DropTable(
                name: "TBRequisicaoEntrada");

            migrationBuilder.DropTable(
                name: "TBRequisicaoSaida");

            migrationBuilder.DropTable(
                name: "TBFuncionario");

            migrationBuilder.DropTable(
                name: "TBMedicamento");

            migrationBuilder.DropTable(
                name: "TBPaciente");

            migrationBuilder.DropTable(
                name: "TBFornecedor");
        }
    }
}
