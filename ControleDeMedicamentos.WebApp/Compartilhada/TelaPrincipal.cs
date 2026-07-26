using System.Net.Http.Headers;
using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFornecedores;
using ControleDeMedicamentos.WebApp.ModuloFuncionario;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using ControleDeMedicamentos.WebApp.ModuloPacientes;
using ControleDeMedicamentos.WebApp.ModuloRequisicoes;
using ControleDeMedicamentos.WebApp.ModuloRequisicoes.RequisicaoSaida;

namespace ControleDeMedicamentos.WebApp.Compartilhado;

public class TelaPrincipal
{
    private readonly TelaFornecedor telaFornecedor;
    private readonly TelaMedicamento telaMedicamento;
    private readonly TelaRequisicaoEntrada telaRequisicaoEntrada;
    private readonly TelaRequisicaoSaida telaRequisicaoSaida;
    private readonly TelaPaciente telaPaciente;
    private readonly TelaFuncionario telaFuncionario;

    public TelaPrincipal(ContextoJson contexto)
    {
        RepositorioFornecedorEmArquivo repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        RepositorioMedicamentoEmArquivo repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
        RepositorioRequisicaoEntradaEmArquivo repositorioRequisicaoEntrada = new RepositorioRequisicaoEntradaEmArquivo(contexto);
        RepositorioRequisicaoSaidaEmArquivo repositorioRequisicaoSaida = new RepositorioRequisicaoSaidaEmArquivo(contexto);
        RepositorioPacienteEmArquivo repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);
        RepositorioFuncionarioEmArquivos repositorioFuncionario = new RepositorioFuncionarioEmArquivos(contexto);

        telaFornecedor = new TelaFornecedor(repositorioFornecedor);
        telaMedicamento = new TelaMedicamento(repositorioMedicamento, repositorioFornecedor);
        telaRequisicaoEntrada = new TelaRequisicaoEntrada(repositorioRequisicaoEntrada, repositorioMedicamento, repositorioFuncionario);
        telaRequisicaoSaida = new TelaRequisicaoSaida(repositorioRequisicaoSaida, repositorioMedicamento, repositorioPaciente);
        telaPaciente = new TelaPaciente(repositorioPaciente);
        telaFuncionario = new TelaFuncionario(repositorioFuncionario);
    }

    public ITelaOpcoes? ObterOpcaoMenuPrincipal()
    {
        //Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Controle de Medicamentos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Gestão de Fornecedores");
        Console.WriteLine("2 - Gestão de Medicamentos");
        Console.WriteLine("3 - Gestão de Requisições de Entrada");
        Console.WriteLine("4 - Gestão de Requisições de Saída");
        Console.WriteLine("5 - Gestão de Pacientes");
        Console.WriteLine("6 - Gestão de Funcionários");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");

        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

        if (opcaoMenuPrincipal == "1")
            return telaFornecedor;

        if (opcaoMenuPrincipal == "2")
            return telaMedicamento;

        if (opcaoMenuPrincipal == "3")
            return telaRequisicaoEntrada;

        if (opcaoMenuPrincipal == "4")
            return telaRequisicaoSaida;

        if (opcaoMenuPrincipal == "5")
            return telaPaciente;

        if (opcaoMenuPrincipal == "6")
            return telaFuncionario;

        return null;
    }
}