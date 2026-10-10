using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloPacientes.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Infraestrutura;

public static class InjecaoDependencia
{
    public static void AddInfraestruturaEmJson(this IServiceCollection services)
    {
        services.AddScoped(_ =>
        {
            ContextoJson contexto = new ContextoJson();

            contexto.Carregar();

            return contexto;
        });

        services.AddScoped<RepositorioMedicamentoEmOrm>();
        services.AddScoped<RepositorioFornecedorEmOrm>();
        services.AddScoped<RepositorioFuncionarioEmOrm>();
        services.AddScoped<RepositorioPacienteEmOrm>();
        services.AddScoped<RepositorioRequisicaoEntradaEmOrm>();
        services.AddScoped<RepositorioRequisicaoSaidaEmOrm>();
    }
}