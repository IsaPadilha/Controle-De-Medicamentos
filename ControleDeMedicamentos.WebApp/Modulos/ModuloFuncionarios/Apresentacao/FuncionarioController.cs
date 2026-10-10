using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Apresentacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Apresentacao;

public sealed class FuncionarioController : Controller
{
    private readonly RepositorioFuncionarioEmOrm repositorioFuncionario;

    public FuncionarioController(RepositorioFuncionarioEmOrm repositorioFuncionario)
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
            // Records são objetos imutáveis
            ListarFuncionarioViewModel vm = new ListarFuncionarioViewModel(
                f.Id,
                f.Nome,
                f.Telefone
            );

            viewModels.Add(vm);
        }

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarFuncionarioViewModel cadastrarVm)
    {
        Funcionario funcionario = new Funcionario(
            cadastrarVm.Nome,
            cadastrarVm.Telefone,
            cadastrarVm.Cpf
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

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarFuncionarioViewModel editarVm)
    {
        Funcionario funcionarioAtualizado = new Funcionario(
            editarVm.Nome,
            editarVm.Telefone,
            editarVm.Cpf
        );

        bool conseguiuEditar = repositorioFuncionario.Editar(editarVm.Id, funcionarioAtualizado);

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

        return View(viewModel);
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