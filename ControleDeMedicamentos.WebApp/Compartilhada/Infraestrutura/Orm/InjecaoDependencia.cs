using ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura.Orm;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionario.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloPaciente.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloPaciente.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Infraestrutura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura
{
    public static class InjecaoDependenciaInfraestrutura
    {
        public static IServiceCollection AddInfraRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do DbContext com SQL Server
            services.AddDbContext<ControleDeMedicamentosDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositórios ORM
            services.AddScoped<IRepositorioFuncionario, RepositorioFuncionarioEmOrm>();
            services.AddScoped<IRepositorioFornecedor, RepositorioFornecedorEmOrm>();
            services.AddScoped<IRepositorioMedicamento, RepositorioMedicamentoEmOrm>();
            services.AddScoped<IRepositorioPaciente, RepositorioPacienteEmOrm>();
            services.AddScoped<IRepositorioRequisicaoEntrada, RepositorioRequisicaoEntradaEmOrm>();
            services.AddScoped<IRepositorioRequisicaoSaida, RepositorioRequisicaoSaidaEmOrm>();

            return services;
        }
    }
}
