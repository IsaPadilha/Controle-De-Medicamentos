namespace ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Apresentacao;

public record ListarFuncionarioViewModel(Guid Id, string Nome, string Telefone);

public record CadastrarFuncionarioViewModel(
    string Nome,
    string Telefone,
    string Cpf
);

public record EditarFuncionarioViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf
);

public record ExcluirFuncionarioViewModel(
    Guid Id,
    string Nome
);