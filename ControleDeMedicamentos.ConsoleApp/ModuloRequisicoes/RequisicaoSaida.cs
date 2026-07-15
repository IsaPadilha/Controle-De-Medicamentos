using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes.RequisicaoSaida;

public class RequisicaoSaida : EntidadeBase
{
    public List<MedicamentoPrescrito> MedicamentoPrescritos { get; set; } = [];
    public Paciente Paciente { get; set; } = null!;
    public DateTime Data { get; set; } = DateTime.Now;

    public RequisicaoSaida() { }

    public RequisicaoSaida(Paciente paciente, List<MedicamentoPrescrito> medicamentoPrescritos) : this()
    {
        MedicamentoPrescritos = MedicamentoPrescritos;
        Paciente = paciente;

        foreach (MedicamentoPrescrito mp in MedicamentoPrescritos)
            mp.Medicamento.RegistrarSaida(this);
    }

    public int ObterQuantidade(Medicamento medicamento)
    {
        foreach (MedicamentoPrescrito mp in MedicamentoPrescritos)
        {
            if (mp.Medicamento.Id == medicamento.Id)
                return mp.Quantidade;
        }

        return 0;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (MedicamentoPrescritos.Count == 0)
            erros.Add("É necessário selecionar ao menos um medicamento.");

        if (Paciente == null)
            erros.Add("O campo \"Paciente\" deve ser preenchido.");

        foreach (MedicamentoPrescrito mp in MedicamentoPrescritos)
        {
            if (mp.Medicamento == null)
            {
                erros.Add("O campo \"Medicamento\" deve ser preenchido.");
            }
            else
            {
                if (mp.Quantidade <= 0)
                    erros.Add($"A \"Quantidade\" do medicamento \"{mp.Medicamento.Nome}\" deve ser maior que zero.");

                if (mp.Medicamento.QuantidadeEmEstoque < 0)
                    erros.Add($"Não há estoque suficiente para o medicamento \"{mp.Medicamento.Nome}\".");
            }
        }
        return erros;
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        RequisicaoSaida requisicaoAtualizada = (RequisicaoSaida)entidadeAtualizada;

        Paciente = requisicaoAtualizada.Paciente;
        MedicamentoPrescritos = requisicaoAtualizada.MedicamentoPrescritos;
    }
}
