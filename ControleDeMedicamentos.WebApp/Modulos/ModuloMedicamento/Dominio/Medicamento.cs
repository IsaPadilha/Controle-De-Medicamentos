using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes;
using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.RequisicaoSaida;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Dominio
{
    public class Medicamento : EntidadeBase<Medicamento>
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public Fornecedor Fornecedor { get; set; } = null!;
        public List<RequisicaoEntrada> Requisicoes { get; set; } = new();
        public List<RequisicaoSaida> Saidas { get; set; } = new();

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

                foreach (var req in Requisicoes)
                    total += req.Quantidade;

                foreach (var saida in Saidas)
                    // aqui o cast garante que o tipo bate com o esperado
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
            var erros = new List<string>();

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

        public override void Atualizar(Medicamento entidadeAtualizada)
        {
            Nome = entidadeAtualizada.Nome;
            Descricao = entidadeAtualizada.Descricao;
            Fornecedor = entidadeAtualizada.Fornecedor;
        }
    }
}
