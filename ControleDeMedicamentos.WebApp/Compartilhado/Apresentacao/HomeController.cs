using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Compartilhado;

public class HomeController : Controller
{
    public ActionResult Index()
    {
        // O Program.cs já sabe procurar nas pastas compartilhadas
        return View();
    }
}