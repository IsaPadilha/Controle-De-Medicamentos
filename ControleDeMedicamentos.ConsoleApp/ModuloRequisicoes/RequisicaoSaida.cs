using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes.RequisicaoSaida;

public class RequisicaoSaida : EntidadeBase
{
    public Medicamento Medicamento { get; set; } = null!;
    public int Quantidade { get; set; }
    public Paciente Paciente { get; set; } = null!;
    public DateTime Data { get; set; } = DateTime.Now;

    public RequisicaoSaida() { }

    public RequisicaoSaida(Medicamento medicamento, int quantidade, Paciente paciente) : this()
    {
        Medicamento = medicamento;
        Quantidade = quantidade;
        Paciente = paciente;

        medicamento.RegistrarSaida(this);
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (Medicamento == null)
            erros.Add("O campo \"Medicamento\" deve ser preenchido.");

        if (Quantidade <= 0)
            erros.Add("A \"Quantidade\" deve ser maior que zero.");

        if (Paciente == null)
            erros.Add("O campo \"Paciente\" deve ser preenchido.");

        return erros;
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        throw new NotImplementedException();
    }
}
