namespace ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Apresentacao;

public record FornecedorMedicamentoViewModel(
    Guid Id,
    string Nome
);

public record ListarMedicamentoViewModel(
    Guid Id,
    string Nome,
    string Descricao,
    string NomeFornecedor,
    int QuantidadeEmEstoque
);

public record CadastrarMedicamentoViewModel(
    string Nome,
    string Descricao,
    Guid FornecedorId
)
{
    public List<FornecedorMedicamentoViewModel> Fornecedores { get; init; } = [];
}

public record EditarMedicamentoViewModel(
    Guid Id,
    string Nome,
    string Descricao,
    Guid FornecedorId
)
{
    public List<FornecedorMedicamentoViewModel> Fornecedores { get; init; } = [];
}

public record ExcluirMedicamentoViewModel(
    Guid Id,
    string Nome
);