using ControleDeMedicamentos.WebApp.Compartilhado.Apresentacao;
using ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura;
using ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura.Orm;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do container de injeção de dependência
builder.Services.AddInfraRepositories(builder.Configuration);
builder.Services.AddPresentationConfig();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ControleDeMedicamentosDbContext>();

    if (dbContext.Database.IsSqlServer())
        dbContext.Database.Migrate();
}

//Middlewares de roteamento
app.UseRouting();
app.MapDefaultControllerRoute();

//Execução do Servidor
app.Run();