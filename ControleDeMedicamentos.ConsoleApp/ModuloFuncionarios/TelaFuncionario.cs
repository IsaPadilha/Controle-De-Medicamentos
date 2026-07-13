using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

public class TelaFuncionario : TelaBase<Funcionario>, ITelaOpcoes, ITelaCrud
{
    public TelaFuncionario(RepositorioBaseEmArquivo<Funcionario> repositorio) : base("Funcionario", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            //Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Visualização de Funcionarios");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -17}",
            "Id", "Nome", "Telefone", "CPF"
        );

        List<Funcionario> registros = repositorio.SelecionarTodos();

        foreach (Funcionario f in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -17}",
                f.Id, f.Nome, f.Telefone, f.Cpf
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Funcionario ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do funcionário: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o telefone do funcionário: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o CPF do funcionário: ");
        string cpf = Console.ReadLine() ?? string.Empty;

        return new Funcionario(nome, telefone, cpf);
    }

    protected override bool ExisteRegistroComInformacoesExclusivas(Funcionario entidade, int? idIgnorado = null)
    {
        List<Funcionario> registros = repositorio.SelecionarTodos();

        foreach (Funcionario f in registros)
        {
            if (f.Id != idIgnorado && f.Cpf == entidade.Cpf)
            {
                Console.WriteLine("---------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Já existe um funcionário cadastrado com o CPF informado.");
                Console.ResetColor();
                Console.WriteLine("---------------------------------");
                return true;
            }
        }

        return false;
    }

    public void CadastrarFuncionarior()
    {
        Funcionario novoFuncionario = ObterDadosCadastrais();

        var erros = novoFuncionario.Validar();
        if (erros.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var erro in erros)
                Console.WriteLine(erro);
            Console.ResetColor();
            return;
        }

        // está verificando se tem cpf duplicado
        if (ExisteRegistroComInformacoesExclusivas(novoFuncionario))
            return;

        // se passou em todas as validaões, irá salvar
        repositorio.Cadastrar(novoFuncionario);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Funcionário cadastrado com sucesso!");
        Console.ResetColor();
    }
}