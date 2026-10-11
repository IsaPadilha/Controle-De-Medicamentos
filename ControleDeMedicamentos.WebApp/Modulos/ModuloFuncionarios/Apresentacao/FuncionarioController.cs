using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Apresentacao;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Apresentacao;

public sealed class FuncionarioController : Controller
{
    private readonly IRepositorioFuncionario repositorioFuncionario;

    public FuncionarioController(IRepositorioFuncionario repositorioFuncionario)
    {
        this.repositorioFuncionario = repositorioFuncionario;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarTodos();

        List<ListarFuncionarioViewModel> viewModels = new List<ListarFuncionarioViewModel>();

        foreach (Funcionario f in funcionarios)
        {
            ListarFuncionarioViewModel vm = new ListarFuncionarioViewModel(
                f.Id,
                f.Nome,
                f.Telefone,
                f.Cpf
            );

            viewModels.Add(vm);
        }

        return View(
    "~/Modulos/ModuloFuncionarios/Apresentacao/Views/Listar.cshtml",
    viewModels
);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View("~/Modulos/ModuloFuncionarios/Apresentacao/Views/Cadastrar.cshtml");
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarFuncionarioViewModel cadastrarVm)
    {
        string cpfSemMascara = Regex.Replace(cadastrarVm.Cpf, @"\D", "");

        Funcionario funcionario = new Funcionario(
            cadastrarVm.Nome,
            cadastrarVm.Telefone,
            cpfSemMascara
        );

        repositorioFuncionario.Cadastrar(funcionario);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Funcionario? funcionarioSelecionado = repositorioFuncionario.SelecionarPorId(id);

        if (funcionarioSelecionado == null)
            return NotFound();

        EditarFuncionarioViewModel viewModel = new EditarFuncionarioViewModel(
            id,
            funcionarioSelecionado.Nome,
            funcionarioSelecionado.Telefone,
            funcionarioSelecionado.Cpf
        );

        return View(
    "~/Modulos/ModuloFuncionarios/Apresentacao/Views/Editar.cshtml",
    viewModel
);
    }

    [HttpPost]
    public ActionResult Editar(EditarFuncionarioViewModel editarVm)
    {
        string cpfSemMascara = Regex.Replace(editarVm.Cpf, @"\D", "");

        Funcionario funcionarioAtualizado = new Funcionario(
            editarVm.Nome,
            editarVm.Telefone,
            cpfSemMascara
        );

        bool conseguiuEditar = repositorioFuncionario.Editar(
            editarVm.Id,
            funcionarioAtualizado
        );

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Funcionario? funcionarioSelecionado = repositorioFuncionario.SelecionarPorId(id);

        if (funcionarioSelecionado == null)
            return NotFound();

        ExcluirFuncionarioViewModel viewModel = new ExcluirFuncionarioViewModel(
            id,
            funcionarioSelecionado.Nome
        );

        return View("~/Modulos/ModuloFuncionarios/Apresentacao/Views/Excluir.cshtml", viewModel);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirFuncionarioViewModel excluirVm)
    {
        bool conseguiuExcluir = repositorioFuncionario.Excluir(excluirVm.Id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}