using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Dominio;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Apresentacao;

public sealed class MedicamentoController : Controller
{
    private readonly RepositorioMedicamentoEmOrm repositorioMedicamento;
    private readonly RepositorioFornecedorEmOrm repositorioFornecedor;

    public MedicamentoController(
        RepositorioMedicamentoEmOrm repositorioMedicamento,
        RepositorioFornecedorEmOrm repositorioFornecedor
        )
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFornecedor = repositorioFornecedor;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

        List<ListarMedicamentoViewModel> viewModels = [];

        foreach (Medicamento med in medicamentos)
        {
            ListarMedicamentoViewModel viewModel = new ListarMedicamentoViewModel(
                med.Id,
                med.Nome,
                med.Descricao,
                med.Fornecedor.Nome,
                med.QuantidadeEmEstoque
            );

            viewModels.Add(viewModel);
        }

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarMedicamentoViewModel viewModel = new CadastrarMedicamentoViewModel(
            string.Empty,
            string.Empty,
            Guid.Empty
        ) with
        { Fornecedores = ObterFornecedores() };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMedicamentoViewModel viewModel)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(viewModel.FornecedorId);

        if (fornecedor == null)
            return NotFound();

        Medicamento medicamento = new Medicamento(viewModel.Nome, viewModel.Descricao, fornecedor);

        repositorioMedicamento.Cadastrar(medicamento);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);

        if (medicamento == null)
            return NotFound();

        EditarMedicamentoViewModel viewModel = new EditarMedicamentoViewModel(
            id,
            medicamento.Nome,
            medicamento.Descricao,
            medicamento.Fornecedor.Id
        ) with
        {
            Fornecedores = ObterFornecedores()
        };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarMedicamentoViewModel viewModel)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(viewModel.FornecedorId);

        if (fornecedor == null)
            return NotFound();

        Medicamento medicamentoAtualizado = new Medicamento(viewModel.Nome, viewModel.Descricao, fornecedor);

        bool conseguiuEditar = repositorioMedicamento.Editar(viewModel.Id, medicamentoAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);

        if (medicamento == null)
            return NotFound();

        return View(medicamento);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExclusao(Guid id)
    {
        bool conseguiuExcluir = repositorioMedicamento.Excluir(id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    private List<FornecedorMedicamentoViewModel> ObterFornecedores()
    {
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarTodos();

        List<FornecedorMedicamentoViewModel> fornecedoresVms = [];

        foreach (Fornecedor f in fornecedores)
        {
            FornecedorMedicamentoViewModel fornecedorVm = new FornecedorMedicamentoViewModel(
                f.Id,
                f.Nome
            );

            fornecedoresVms.Add(fornecedorVm);
        }

        return fornecedoresVms;
    }
}