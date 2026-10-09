using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloPaciente.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio;
using Microsoft.EntityFrameworkCore;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura.Orm;

public sealed class ControleDeMedicamentosDbContext : DbContext
{
    public DbSet<Fornecedor> Fornecedores => Set<Fornecedor>();
    public DbSet<Funcionario> Funcionarios => Set<Funcionario>();
    public DbSet<Medicamento> Medicamentos => Set<Medicamento>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<RequisicaoEntrada> RequisicoesEntrada => Set<RequisicaoEntrada>();
    public DbSet<RequisicaoSaida> RequisicoesSaida => Set<RequisicaoSaida>();

    public ControleDeMedicamentosDbContext(DbContextOptions<ControleDeMedicamentosDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FornecedorConfiguration());

        // Aplica automaticamente todas as configurações de mapeamento que implementem IEntityTypeConfiguration no assembly atual
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ControleDeMedicamentosDbContext).Assembly);
    }
}