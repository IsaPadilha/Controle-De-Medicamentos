using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

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
