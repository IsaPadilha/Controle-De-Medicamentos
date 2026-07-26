using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloFornecedores;
using ControleDeMedicamentos.WebApp.ModuloFuncionario;
using ControleDeMedicamentos.WebApp.ModuloRequisicoes;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaOpcoes, ITelaCrud
{
    private readonly RepositorioFornecedorEmArquivo repositorioFornecedor;

    public TelaMedicamento(
        RepositorioMedicamentoEmArquivo repositorioMedicamento,
        RepositorioFornecedorEmArquivo repositorioFornecedor
    ) : base("Medicamento", repositorioMedicamento)
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            //Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Visualização de Medicamentos");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -20} | {4, -10} | {5, -10}",
            "Id", "Nome", "Fornecedor", "Descrição", "Estoque", "Status"
        );

        List<Medicamento> registros = repositorio.SelecionarTodos();

        foreach (Medicamento m in registros)
        {
            string status = m.QuantidadeEmEstoque <= 20 ? "EM FALTA" : "";

            Console.Write(
                "{0, -7} | {1, -20} | {2, -20} | {3, -20} | {4, -10} | ",
                m.Id, m.Nome, m.Fornecedor.Nome, m.Descricao, m.QuantidadeEmEstoque
            );

            if (status == "EM FALTA")
            {
                Console.ForegroundColor = ConsoleColor.Red; // só o texto em vermelho
                Console.WriteLine("{0, -10}", status);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("{0, -10}", status);
            }
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Medicamento ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do medicamento: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite a descrição do medicamento: ");
        string descricao = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("---------------------------------");

        VisualizarFornecedores();

        Console.WriteLine("---------------------------------");

        Console.Write("Digite o ID do fornecedor que deseja selecionar: ");
        int idFornecedor = Convert.ToInt32(Console.ReadLine());

        Fornecedor fornecedor = repositorioFornecedor.SelecionarPorId(idFornecedor)!;

        return new Medicamento(nome, descricao, fornecedor);
    }

    private void VisualizarFornecedores()
    {
        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -17}",
            "Id", "Nome", "Telefone", "CNPJ"
        );

        List<Fornecedor> registros = repositorioFornecedor.SelecionarTodos();

        foreach (Fornecedor f in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -17}",
                f.Id, f.Nome, f.Telefone, f.Cnpj
            );
        }
    }

    protected override bool ExisteRegistroComInformacoesExclusivas(Medicamento entidade, int? idIgnorado = null)
    {
        List<Medicamento> registros = repositorio.SelecionarTodos();

        foreach (Medicamento m in registros)
        {
            if (m.Id != idIgnorado && m.Nome.Equals(entidade.Nome, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("---------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Esse medicamento já existe, o estoque será atualizado.");
                Console.ResetColor();
                Console.WriteLine("---------------------------------");

                Console.Write("Digite a quantidade para adicionar ao estoque: ");
                int quantidade = Convert.ToInt32(Console.ReadLine());

                RequisicaoEntrada requisicao = new RequisicaoEntrada(m, quantidade);
                m.RegistrarRequisicao(requisicao);

                repositorio.Editar(m.Id, m);

                Console.WriteLine("---------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Estoque atualizado com sucesso!");
                Console.ResetColor();

                return true; //foi atualizada
            }
        }

        return false; //nao existia, pode cadastrar
    }
}