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
       RepositorioRequisicaoSaidaEmArquivo repositorio,
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

        List<RequisicaoSaida> registros = repositorio.SelecionarTodos();

        foreach (RequisicaoSaida r in registros)
        {
            Console.WriteLine("Id: {0} | Paciente: {1} | Data: {2}",
                r.Id, r.Paciente.Nome, r.Data.ToShortDateString());

            Console.WriteLine(
                 "{0, -7} | {1, -20} | {2, -10}",
                "Id", "Medicamento", "Qtd"
            );

            foreach (MedicamentoPrescrito mp in r.MedicamentoPrescritos)
            {
                Console.WriteLine(
                    "{0, -7} | {1, -20} | {2, -10}",
                    mp.Medicamento.Id, mp.Medicamento.Nome, mp.Quantidade
                );
            }
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
        VisualizarPacientes();

        Console.WriteLine("---------------------------------");

        Console.WriteLine("Digite o ID do paciente que está realizando a entrada: ");
        int idPaciente = Convert.ToInt32(Console.ReadLine());

        Paciente paciente = repositorioPaciente.SelecionarPorId(idPaciente)!;
        List<MedicamentoPrescrito> medicamentoPrescritos = [];

        while (true)
        {
            VisualizarMedicamentos();

            Console.WriteLine("---------------------------------");

            Console.Write("Digite o ID do medicamento (0 para finalizar): ");
            int idMedicamento = Convert.ToInt32(Console.ReadLine());

            if (idMedicamento == 0)
                break;

            Medicamento medicamento = repositorioMedicamento.SelecionarPorId(idMedicamento)!;

            Console.Write("Digite a quantidade: ");
            int quantidade = Convert.ToInt32(Console.ReadLine());

            medicamentoPrescritos.Add(new MedicamentoPrescrito(medicamento, quantidade));
        }
        return new RequisicaoSaida(paciente, medicamentoPrescritos);
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
            "{0, -7} | {1, -20} | {2, -20} | {3, -20} | {4, -10}",
            "Id", "Nome", "Fornecedor", "Descrição", "Estoque"
        );

        List<Medicamento> registros = repositorioMedicamento.SelecionarTodos();

        foreach (Medicamento m in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -20} | {3, -20} | {4, -10}",
                m.Id, m.Nome, m.Fornecedor.Nome, m.Descricao, m.QuantidadeEmEstoque
            );
        }
    }

    protected override bool ExistemDependenciasAtivasDoRegistro(int idRegistro)
    {
        return false;
    }
}
