using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using ControleDeMedicamentos.WebApp.ModuloFuncionario;
using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class TelaRequisicaoEntrada : TelaBase<RequisicaoEntrada>, ITelaOpcoes, ITelaCrud
{
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
    private readonly RepositorioFuncionarioEmArquivos repositorioFuncionario;

    public TelaRequisicaoEntrada(
        RepositorioRequisicaoEntradaEmArquivo repositorioRequisicao,
        RepositorioMedicamentoEmArquivo repositorioMedicamento,
        RepositorioFuncionarioEmArquivos repositorioFuncionario
    ) : base("Requisição de Entrada", repositorioRequisicao)
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho = true)
    {
        if (deveExibirCabecalho)
        {
            //Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Visualização de Requisições de Entrada");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -10} | {3, -15} | {4, -15}",
            "Id", "Medicamento", "Qtd", "Funcionario", "Data"
        );

        List<RequisicaoEntrada> registros = repositorio.SelecionarTodos();

        foreach (RequisicaoEntrada r in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -10} | {3, -15} | {4, -15}",
                r.Id, r.Medicamento.Nome, r.Quantidade, r.Funcionario.Nome, r.Data.ToShortDateString()
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override RequisicaoEntrada ObterDadosCadastrais()
    {
        VisualizarMedicamentos();

        Console.WriteLine("---------------------------------");

        Console.Write("Digite o ID do medicamento que deseja requisitar: ");
        int idMedicamento = Convert.ToInt32(Console.ReadLine());

        Medicamento medicamento = repositorioMedicamento.SelecionarPorId(idMedicamento)!;

        if (medicamento == null)
        {
            Console.WriteLine("Medicamento não encontrado.");
            Console.ReadLine();
            return null!;
        }

        VisualizarFuncionarios();

        Console.WriteLine("---------------------------------");

        Console.WriteLine("Digite o ID do funcionário que está realizando a entrada: ");
        int idFuncionario = Convert.ToInt32(Console.ReadLine());

        Funcionario funcionario = repositorioFuncionario.SelecionarPorId(idFuncionario)!;

        if (funcionario == null)
        {
            Console.WriteLine("Funcionário não encontrado.");
            Console.ReadLine();
            return null!;
        }

        Console.Write("Digite a quantidade que deseja requisitar: ");
        int quantidade = Convert.ToInt32(Console.ReadLine());

        RequisicaoEntrada novaRequisicao = new RequisicaoEntrada(medicamento, quantidade, funcionario);
        medicamento.RegistrarRequisicao(novaRequisicao); //atualiza o calculo de estoque
        repositorioMedicamento.Editar(medicamento.Id, medicamento); //salva o medicamento com a nova requisicao

        return novaRequisicao;
    }

    private void VisualizarFuncionarios()
    {
        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -15}",
            "Id", "Nome", "Telefone", "CPF"
        );

        List<Funcionario> registros = repositorioFuncionario.SelecionarTodos();

        foreach (Funcionario f in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -20} | {3, -15}",
                f.Id, f.Nome, f.Telefone, f.Cpf
            );
        }
    }

    private void VisualizarMedicamentos()
    {
        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -20}",
            "Id", "Nome", "Fornecedor", "Descrição"
        );

        List<Medicamento> registros = repositorioMedicamento.SelecionarTodos();

        foreach (Medicamento m in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -20} | {3, -20}",
                m.Id, m.Nome, m.Fornecedor.Nome, m.Descricao
            );
        }
    }

    protected override bool ExistemDependenciasAtivasDoRegistro(int idRegistro)
    {
        return false;
    }
}