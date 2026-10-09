using System;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Apresentacao
{
    public record ListarMedicamentoPrescritoRequisicaoSaidaViewModel(
        Guid MedicamentoId,
        string NomeMedicamento,
        int Quantidade
    );

    public record ListarRequisicaoSaidaViewModel(
        Guid Id,
        string NomePaciente,
        DateTime Data,
        List<ListarMedicamentoPrescritoRequisicaoSaidaViewModel> MedicamentosPrescritos
    );

    public record PacienteRequisicaoSaidaViewModel(Guid Id, string Nome);

    public record MedicamentoPrescritoRequisicaoSaidaViewModel(
        Guid MedicamentoId,
        string NomeMedicamento,
        int EstoqueAtual,
        bool Selecionado,
        int Quantidade
    );

    public record CadastrarRequisicaoSaidaViewModel(Guid PacienteId)
    {
        public List<PacienteRequisicaoSaidaViewModel> Pacientes { get; init; } = [];
        public List<MedicamentoPrescritoRequisicaoSaidaViewModel> MedicamentosPrescritos { get; init; } = [];
    }
}
