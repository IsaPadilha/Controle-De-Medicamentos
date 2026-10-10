namespace ControleDeMedicamentos.WebApp.Modulos.ModuloPacientes.Apresentacao;

public record ListarPacienteViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSus
);

public record CadastrarPacienteViewModel(
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);

public record EditarPacienteViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);

public record ExcluirPacienteViewModel(
    Guid Id,
    string Nome
);