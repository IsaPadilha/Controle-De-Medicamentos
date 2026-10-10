using System.Net.Http.Headers;
using ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura.Orm;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Apresentacao;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Apresentacao;
using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Apresentacao;
using ControleDeMedicamentos.WebApp.Modulos.ModuloPacientes.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloPacientes.Apresentacao;
using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Infraestrutura;
using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Apresentacao;

namespace ControleDeMedicamentos.WebApp.Compartilhado;

public class TelaPrincipal
{
    private readonly TelaFornecedor telaFornecedor;
    private readonly TelaMedicamento telaMedicamento;
    private readonly TelaRequisicaoEntrada telaRequisicaoEntrada;
    private readonly TelaRequisicaoSaida telaRequisicaoSaida;
    private readonly TelaPaciente telaPaciente;
    private readonly TelaFuncionario telaFuncionario;

    public TelaPrincipal(ControleDeMedicamentosDbContext contexto)
    {
        RepositorioFornecedorEmOrm repositorioFornecedor = new RepositorioFornecedorEmOrm(contexto);
        RepositorioMedicamentoEmOrm repositorioMedicamento = new RepositorioMedicamentoEmOrm(contexto);
        RepositorioRequisicaoEntradaEmOrm repositorioRequisicaoEntrada = new RepositorioRequisicaoEntradaEmOrm(contexto);
        RepositorioRequisicaoSaidaEmOrm repositorioRequisicaoSaida = new RepositorioRequisicaoSaidaEmOrm(contexto);
        RepositorioPacienteEmOrm repositorioPaciente = new RepositorioPacienteEmOrm(contexto);
        RepositorioFuncionarioEmOrm repositorioFuncionario = new RepositorioFuncionarioEmOrm(contexto);

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