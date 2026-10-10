using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Infraestrutura;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Apresentacao;

public class TelaFornecedor : TelaBase<Fornecedor>, ITelaOpcoes, ITelaCrud
{
    public TelaFornecedor(IRepositorioFornecedor repositorio)
        : base("Fornecedor", repositorio)
    {
    }

    protected override Fornecedor ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do fornecedor: ");
        string nome = Console.ReadLine()!;

        Console.Write("Digite o telefone: ");
        string telefone = Console.ReadLine()!;

        Console.Write("Digite o CNPJ: ");
        string cnpj = Console.ReadLine()!;

        return new Fornecedor(nome, telefone, cnpj);
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Visualização de Fornecedores");
            Console.WriteLine("-----------------------------------------");
        }

        var registros = repositorio.SelecionarTodos();

        Console.WriteLine("{0, -7} | {1, -30} | {2, -15} | {3, -17}",
            "Id", "Nome", "Telefone", "CNPJ");

        foreach (var f in registros)
            Console.WriteLine("{0, -7} | {1, -30} | {2, -15} | {3, -17}",
                f.Id, f.Nome, f.Telefone, f.Cnpj);
    }

    protected override bool ExisteRegistroComInformacoesExclusivas(Fornecedor entidade, Guid? idIgnorado = null)
    {
        var registros = repositorio.SelecionarTodos();

        foreach (var f in registros)
        {
            if (f.Id != idIgnorado && f.Cnpj == entidade.Cnpj)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Já existe um fornecedor com este CNPJ.");
                Console.ResetColor();
                return true;
            }
        }
        return false;
    }
}