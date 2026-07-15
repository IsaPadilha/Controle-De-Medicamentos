using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes;

public class MedicamentoPrescrito
{
    public Medicamento Medicamento { get; set; }
    public int Quantidade { get; set; }

    public MedicamentoPrescrito(Medicamento medicamento, int quantidade)
    {
        Medicamento = medicamento;
        Quantidade = quantidade;
    }
}
