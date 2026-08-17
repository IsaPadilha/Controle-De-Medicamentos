// Objetivo: Rodar um servidor web
// Servidor web: um programa que executa na rede local/remota
// ... e espera por requisições externas ...
// ... geralmente responde com arquivos HTML / CSS / JS (Páginas Web)

// Objeto de configuração do servidor
using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFornecedores;
using ControleDeMedicamentos.WebApp.ModuloFuncionario;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using ControleDeMedicamentos.WebApp.ModuloPacientes;
using ControleDeMedicamentos.WebApp.ModuloRequisicoes;
using ControleDeMedicamentos.WebApp.ModuloRequisicoes.RequisicaoSaida;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Adiciona e injeta uma instância por requisição/conexão
// Func<IServiceProvider, ContextoJson> implementationFactory

builder.Services.AddScoped<ContextoJson>(ContextoJson.InjetarContexto);
builder.Services.AddScoped<RepositorioMedicamentoEmArquivo>();
builder.Services.AddScoped<RepositorioFornecedorEmArquivo>();
builder.Services.AddScoped<RepositorioFuncionarioEmArquivos>();
builder.Services.AddScoped<RepositorioPacienteEmArquivo>();
builder.Services.AddScoped<RepositorioRequisicaoEntradaEmArquivo>();
builder.Services.AddScoped<RepositorioRequisicaoSaidaEmArquivo>();

// Habilita o MVC = Model - View - Controller
builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

// Middlewares - funções que executam à cada requisição e resposta
app.UseRouting();
app.MapDefaultControllerRoute();

app.UseStaticFiles();

// Executa o servidor
app.Run();