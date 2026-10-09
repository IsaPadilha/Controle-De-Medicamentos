using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Apresentacao;

public class RequisicaoEntradaController : Controller
{
    private readonly IRepositorioRequisicaoEntrada repositorio;
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioFuncionario repositorioFuncionario;

    public RequisicaoEntradaController(
        IRepositorioRequisicaoEntrada repositorio,
        IRepositorioMedicamento repositorioMedicamento,
        IRepositorioFuncionario repositorioFuncionario
    )
    {
        this.repositorio = repositorio;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        var viewModels = new List<ListarRequisicaoEntradaViewModel>();

        foreach (var requisicao in repositorio.SelecionarTodos())
        {
            var viewModel = new ListarRequisicaoEntradaViewModel(
                requisicao.Id,
                requisicao.Medicamento.Nome,
                requisicao.Funcionario.Nome,
                requisicao.Quantidade,
                requisicao.Data
            );

            viewModels.Add(viewModel);
        }

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        var viewModel = new CadastrarRequisicaoEntradaViewModel(
            Guid.Empty,
            Guid.Empty,
            0
        )
        {
            Medicamentos = ObterMedicamentos(),
            Funcionarios = ObterFuncionarios()
        };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarRequisicaoEntradaViewModel viewModel)
    {
        var medicamento = repositorioMedicamento.SelecionarPorId(viewModel.MedicamentoId);
        if (medicamento == null)
            return NotFound();

        var funcionario = repositorioFuncionario.SelecionarPorId(viewModel.FuncionarioId);
        if (funcionario == null)
            return NotFound();

        var requisicaoEntrada = new RequisicaoEntrada(
            medicamento,
            viewModel.Quantidade,
            funcionario
        );

        repositorio.Cadastrar(requisicaoEntrada);

        return RedirectToAction(nameof(Listar));
    }

    private List<MedicamentoRequisicaoEntradaViewModel> ObterMedicamentos()
    {
        var viewModels = new List<MedicamentoRequisicaoEntradaViewModel>();

        foreach (var medicamento in repositorioMedicamento.SelecionarTodos())
        {
            var viewModel = new MedicamentoRequisicaoEntradaViewModel(
                medicamento.Id,
                medicamento.Nome
            );

            viewModels.Add(viewModel);
        }

        return viewModels;
    }

    private List<FuncionarioRequisicaoEntradaViewModel> ObterFuncionarios()
    {
        var viewModels = new List<FuncionarioRequisicaoEntradaViewModel>();

        foreach (var funcionario in repositorioFuncionario.SelecionarTodos())
        {
            var viewModel = new FuncionarioRequisicaoEntradaViewModel(
                funcionario.Id,
                funcionario.Nome
            );

            viewModels.Add(viewModel);
        }

        return viewModels;
    }
}
