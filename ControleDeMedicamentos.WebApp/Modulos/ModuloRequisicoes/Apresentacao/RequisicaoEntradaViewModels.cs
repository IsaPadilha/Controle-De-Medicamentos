using System;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Apresentacao
{
    public record MedicamentoRequisicaoEntradaViewModel(
        Guid Id,
        string Nome
    );

    public record FuncionarioRequisicaoEntradaViewModel(
        Guid Id,
        string Nome
    );

    public record ListarRequisicaoEntradaViewModel(
        Guid Id,
        string NomeMedicamento,
        string NomeFuncionario,
        int Quantidade,
        DateTime Data
    );

    public record CadastrarRequisicaoEntradaViewModel(
        Guid MedicamentoId,
        Guid FuncionarioId,
        int Quantidade
    )
    {
        public List<MedicamentoRequisicaoEntradaViewModel> Medicamentos { get; init; } = [];
        public List<FuncionarioRequisicaoEntradaViewModel> Funcionarios { get; init; } = [];
    }
}
