namespace ControleDeMedicamentos.WebApp.Compartilhado.Apresentacao;

public static class InjecaoDependenciaApresentacao
{
    public static void AddPresentationConfig(this IServiceCollection services)
    {
        services.AddControllersWithViews().AddRazorOptions(options =>
        {
            options.ViewLocationFormats.Clear();

            // Módulos
            options.ViewLocationFormats.Add(
                "/Modulos/Modulo{1}/Apresentacao/Views/{0}.cshtml");

            options.ViewLocationFormats.Add(
                "/Modulos/ModuloFornecedores/Apresentacao/Views/{0}.cshtml");

            options.ViewLocationFormats.Add(
                "/Modulos/ModuloFuncionarios/Apresentacao/Views/{0}.cshtml");

            // Views compartilhadas
            options.ViewLocationFormats.Add(
                "/Compartilhado/Apresentacao/Views/{0}.cshtml");

            options.ViewLocationFormats.Add(
                "/Compartilhado/Apresentacao/Views/Home/{0}.cshtml");
        });
    }
}