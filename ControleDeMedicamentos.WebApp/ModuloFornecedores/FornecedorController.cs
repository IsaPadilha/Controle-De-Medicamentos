using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores;

public sealed class FornecedorController : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        return View();
    }

}