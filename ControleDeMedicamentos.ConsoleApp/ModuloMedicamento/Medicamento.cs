using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes.RequisicaoSaida;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

public class Medicamento : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public Fornecedor Fornecedor { get; set; } = null!;
    public List<RequisicaoEntrada> Requisicoes { get; set; } = [];
    public List<RequisicaoSaida> Saidas { get; set; } = [];

    public Medicamento() { }

    public Medicamento(string nome, string descricao, Fornecedor fornecedor) : this()
    {
        Nome = nome;
        Descricao = descricao;
        Fornecedor = fornecedor;
    }

    public int QuantidadeEmEstoque
    {
        get
        {
            int total = 0;

            foreach (RequisicaoEntrada req in Requisicoes)
                total += req.Quantidade;

            foreach (RequisicaoSaida saida in Saidas)
                total -= saida.ObterQuantidade(this);

            return total;
        }
    }

    public void RegistrarRequisicao(RequisicaoEntrada requisicao)
    {
        Requisicoes.Add(requisicao);
    }

    public void RegistrarSaida(RequisicaoSaida requisicao)
    {
        Saidas.Add(requisicao);
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Descricao) || Descricao.Length < 5 || Descricao.Length > 255)
            erros.Add("O campo \"Descrição\" deve conter entre 5 e 255 caracteres.");

        if (Fornecedor == null)
            erros.Add("O campo \"Fornecedor\" deve ser preenchido.");

        foreach (var req in Requisicoes)
        {
            if (req.Quantidade <= 0)
                erros.Add("A quantidade da requisição deve ser maior que zero.");
        }

        return erros;
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Medicamento medicamentoAtualizado = (Medicamento)entidadeAtualizada;

        Nome = medicamentoAtualizado.Nome;
        Descricao = medicamentoAtualizado.Descricao;
        Fornecedor = medicamentoAtualizado.Fornecedor;
    }
}