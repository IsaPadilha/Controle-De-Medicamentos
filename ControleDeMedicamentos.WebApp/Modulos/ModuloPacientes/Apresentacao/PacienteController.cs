using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.Modulos.ModuloPacientes.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloPacientes.Dominio;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloPacientes.Apresentacao;

public class PacienteController : Controller
{
    private readonly IRepositorioPaciente repositorio;

    public PacienteController(IRepositorioPaciente repositorio)
    {
        this.repositorio = repositorio;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarPacienteViewModel> viewModels = [];

        foreach (Paciente p in repositorio.SelecionarTodos())
        {
            ListarPacienteViewModel viewModel = new ListarPacienteViewModel(
                p.Id,
                p.Nome,
                p.Telefone,
                p.CartaoSus,
                p.Cpf
            );

            viewModels.Add(viewModel);
        }

        return View(
            "~/Modulos/ModuloPacientes/Apresentacao/Views/Listar.cshtml",
            viewModels
        );
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarPacienteViewModel viewModel = new CadastrarPacienteViewModel(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty
        );

        return View(
    "~/Modulos/ModuloPacientes/Apresentacao/Views/Cadastrar.cshtml",
    viewModel
);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarPacienteViewModel viewModel)
    {
        string cpfSemMascara = Regex.Replace(viewModel.Cpf, @"\D", "");

        Paciente paciente = new Paciente(
            viewModel.Nome,
            viewModel.Telefone,
            viewModel.CartaoSus,
            cpfSemMascara
        );

        repositorio.Cadastrar(paciente);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Paciente? paciente = repositorio.SelecionarPorId(id);

        if (paciente == null)
            return NotFound();

        EditarPacienteViewModel viewModel = new EditarPacienteViewModel(
            id,
            paciente.Nome,
            paciente.Telefone,
            paciente.CartaoSus,
            paciente.Cpf
        );

        return View(
    "~/Modulos/ModuloPacientes/Apresentacao/Views/Editar.cshtml",
    viewModel
);
    }

    [HttpPost]
    public ActionResult Editar(EditarPacienteViewModel viewModel)
    {
        string cpfSemMascara = Regex.Replace(viewModel.Cpf, @"\D", "");

        Paciente pacienteAtualizado = new Paciente(
            viewModel.Nome,
            viewModel.Telefone,
            viewModel.CartaoSus,
            cpfSemMascara
        );

        bool conseguiuEditar = repositorio.Editar(
            viewModel.Id,
            pacienteAtualizado
        );

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Paciente? paciente = repositorio.SelecionarPorId(id);

        if (paciente == null)
            return NotFound();

        ExcluirPacienteViewModel viewModel = new ExcluirPacienteViewModel(
            id,
            paciente.Nome
        );

        return View(viewModel);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExclusao(Guid id)
    {
        bool conseguiuExcluir = repositorio.Excluir(id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}