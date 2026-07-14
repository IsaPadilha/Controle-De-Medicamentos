using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes.RequisicaoSaida;

public class TelaRequisicaoSaida : TelaBase<RequisicaoSaida>, ITelaOpcoes, ITelaCrud
{
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
    private readonly RepositorioPacienteEmArquivo repositorioPaciente;

    public TelaRequisicaoSaida(
        RepositorioBaseEmArquivo<RequisicaoSaida> repositorio,
        RepositorioMedicamentoEmArquivo repositorioMedicamento,
        RepositorioPacienteEmArquivo repositorioPaciente)
        : base("Requisição de Saída", repositorio)
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioPaciente = repositorioPaciente;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            //Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Visualização de Requisições de Saída");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -10} | {3, -15} | {4, -15}",
            "Id", "Medicamento", "Qtd", "Paciente", "Data"
        );

        List<RequisicaoSaida> registros = repositorio.SelecionarTodos();

        foreach (RequisicaoSaida r in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -10} | {3, -15} | {4, -15}",
                r.Id, r.Medicamento.Nome, r.Quantidade, r.Paciente.Nome, r.Data.ToShortDateString()
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override RequisicaoSaida ObterDadosCadastrais()
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

        VisualizarPacientes();

        Console.WriteLine("---------------------------------");

        Console.WriteLine("Digite o ID do paciente que está realizando a entrada: ");
        int idPaciente = Convert.ToInt32(Console.ReadLine());

        Paciente paciente = repositorioPaciente.SelecionarPorId(idPaciente)!;

        if (paciente == null)
        {
            Console.WriteLine("Paciente não encontrado.");
            Console.ReadLine();
            return null!;
        }

        Console.Write("Digite a quantidade que deseja requisitae: ");
        int quantidade = Convert.ToInt32(Console.ReadLine());

        if (quantidade > medicamento.QuantidadeEmEstoque)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro: Quantidade requisitada ({quantidade}) excede o estoque disponível ({medicamento.QuantidadeEmEstoque}).");
            Console.ResetColor();
            Console.ReadLine();
            return null!;
        }

        RequisicaoSaida novaRequisicao = new RequisicaoSaida(medicamento, quantidade, paciente);
        medicamento.RegistrarSaida(novaRequisicao); //atualiza o calculo de estoque
        repositorioMedicamento.Editar(medicamento.Id, medicamento); //salva o medicamento com a nova requisicao

        return novaRequisicao;
    }

    private void VisualizarPacientes()
    {
        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -15} | {4, -15}",
            "Id", "Nome", "Telefone", "Cartão do Sus", "CPF"
        );

        List<Paciente> registros = repositorioPaciente.SelecionarTodos();

        foreach (Paciente p in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -20} | {3, -15} | {4, -15}",
                p.Id, p.Nome, p.Telefone, p.CartaoSus, p.Cpf
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
