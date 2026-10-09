using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio;

public class MedicamentoPrescrito
{
    public Medicamento Medicamento { get; set; } = null!;
    public int Quantidade { get; set; }

    public RequisicaoSaida RequisicaoSaida { get; set; } = null!;
    public MedicamentoPrescrito(Medicamento medicamento, int quantidade)
    {
        Medicamento = medicamento;
        Quantidade = quantidade;
    }
}
