// Objetivo: Rodar um servidor web
// Servidor web: um programa que executa na rede local/remota
// ... e espera por requisições externas ...
// ... geralmente responde com arquivos HTML / CSS / JS (Páginas Web)

// Objeto de configuração do servidor
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Habilita o MVC = Model - View - Controller
builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

// Middlewares - funções que executam à cada requisição e resposta
app.UseRouting();
app.MapDefaultControllerRoute();

app.UseStaticFiles();

// Executa o servidor
app.Run();