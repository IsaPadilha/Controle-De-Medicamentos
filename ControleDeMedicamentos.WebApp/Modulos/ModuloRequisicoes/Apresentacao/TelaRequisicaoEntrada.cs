using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Apresentacao;

public class TelaRequisicaoEntrada : ITelaOpcoes, ITelaCrud
{
    private readonly IRepositorio<RequisicaoEntrada> repositorioRequisicao;
    private readonly IRepositorio<Medicamento> repositorioMedicamento;
    private readonly IRepositorio<Funcionario> repositorioFuncionario;

    public TelaRequisicaoEntrada(
        IRepositorio<RequisicaoEntrada> repositorioRequisicao,
        IRepositorio<Medicamento> repositorioMedicamento,
        IRepositorio<Funcionario> repositorioFuncionario
    )
    {
        this.repositorioRequisicao = repositorioRequisicao;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public string ObterOpcaoMenu() => "Requisição de Entrada";

    public void Cadastrar()
    {
        var novaRequisicao = ObterDadosCadastrais();
        repositorioRequisicao.Cadastrar(novaRequisicao);
        Console.WriteLine("Requisição cadastrada com sucesso!");
        Console.ReadLine();
    }

    public void Editar()
    {
        VisualizarTodos();
        Console.Write("Digite o ID da requisição que deseja editar: ");
        Guid id = Guid.Parse(Console.ReadLine()!);

        var novaRequisicao = ObterDadosCadastrais();
        repositorioRequisicao.Editar(id, novaRequisicao);
        Console.WriteLine("Requisição editada com sucesso!");
        Console.ReadLine();
    }

    public void Excluir()
    {
        VisualizarTodos();
        Console.Write("Digite o ID da requisição que deseja excluir: ");
        Guid id = Guid.Parse(Console.ReadLine()!);

        repositorioRequisicao.Excluir(id);
        Console.WriteLine("Requisição excluída com sucesso!");
        Console.ReadLine();
    }

    public void VisualizarTodos(bool deveExibirCabecalho = true)
    {
        var registros = repositorioRequisicao.SelecionarTodos();
        foreach (var r in registros)
        {
            Console.WriteLine($"{r.Id} | {r.Medicamento.Nome} | {r.Quantidade} | {r.Funcionario.Nome} | {r.Data.ToShortDateString()}");
        }
    }

    private RequisicaoEntrada ObterDadosCadastrais()
    {
        Console.Write("Digite a quantidade: ");
        int quantidade = Convert.ToInt32(Console.ReadLine());

        var medicamento = repositorioMedicamento.SelecionarTodos().First();
        var funcionario = repositorioFuncionario.SelecionarTodos().First();

        return new RequisicaoEntrada(medicamento, quantidade, funcionario);
    }
}
