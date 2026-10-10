using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio;

public class RequisicaoEntrada : EntidadeBase<RequisicaoEntrada>
{
    public Medicamento Medicamento { get; set; } = null!;
    public int Quantidade { get; set; }
    public Funcionario Funcionario { get; set; } = null!;
    public DateTime Data { get; set; } = DateTime.Now;

    public RequisicaoEntrada() { }

    public RequisicaoEntrada(Medicamento medicamento, int quantidade, Funcionario funcionario) : this()
    {
        Medicamento = medicamento;
        Quantidade = quantidade;
        Funcionario = funcionario;

        medicamento.RegistrarRequisicao(this);
    }

    public override List<string> Validar()
    {
        var erros = new List<string>();

        if (Medicamento == null)
            erros.Add("O campo \"Medicamento\" deve ser preenchido.");

        if (Quantidade <= 0)
            erros.Add("A \"Quantidade\" deve ser maior que zero.");

        if (Funcionario == null)
            erros.Add("O campo \"Funcionário\" deve ser preenchido.");

        return erros;
    }

    public override void Atualizar(RequisicaoEntrada entidadeAtualizada)
    {
        Medicamento = entidadeAtualizada.Medicamento;
        Quantidade = entidadeAtualizada.Quantidade;
        Funcionario = entidadeAtualizada.Funcionario;
        Data = entidadeAtualizada.Data;
    }
}
