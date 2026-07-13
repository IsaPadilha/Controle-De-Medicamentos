using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class Paciente : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CartaoSus { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;

    public Paciente() { }
    public Paciente(string nome, string telefone, string cartaoSus, string cpf) : this()
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSus = cartaoSus;
        Cpf = cpf;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (!Regex.IsMatch(Telefone, @"^\(\d{2}\)\s?\d{4,5}-\d{4}$"))
            erros.Add("O campo \"Telefone\" deve estar no formato (DDD) 90000-0000.");

        if (string.IsNullOrWhiteSpace(CartaoSus) || CartaoSus.Length != 15)
            erros.Add("O campo \"CartaoSus\" deve conter 15 dígitos.");

        if (string.IsNullOrWhiteSpace(Cpf) || Cpf.Length != 11)
            erros.Add("O campo \"Cpf\" deve conter 11 dígitos");

        return erros;
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Paciente pacienteAtualizado = (Paciente)entidadeAtualizada;

        Nome = pacienteAtualizado.Nome;
        Telefone = pacienteAtualizado.Telefone;
        CartaoSus = pacienteAtualizado.CartaoSus;
        Cpf = pacienteAtualizado.Cpf;
    }
}
