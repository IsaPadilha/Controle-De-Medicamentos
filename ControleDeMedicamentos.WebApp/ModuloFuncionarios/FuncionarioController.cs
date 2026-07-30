using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario;

public sealed class FuncionarioController : Controller
{
    private readonly RepositorioFuncionarioEmArquivos repositorioFuncionario;

    public FuncionarioController()
    {
        ContextoJson contexto = new ContextoJson();

        contexto.Carregar();

        repositorioFuncionario = new RepositorioFuncionarioEmArquivos(contexto);
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
    public ActionResult Editar(int id)
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
}