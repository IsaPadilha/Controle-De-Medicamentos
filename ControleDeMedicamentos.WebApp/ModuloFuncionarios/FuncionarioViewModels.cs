using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario;

public record ListarFuncionarioViewModel(int Id, string Nome, string Telefone);

public record CadastrarFuncionarioViewModel(
    string Nome,
    string Telefone,
    string Cpf
);