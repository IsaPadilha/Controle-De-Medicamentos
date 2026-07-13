using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

public class RepositorioFuncionarioEmArquivos : RepositorioBaseEmArquivo<Funcionario>
{
    public RepositorioFuncionarioEmArquivos(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Funcionario> ObterRegistros()
    {
        return contexto.Funcionarios;
    }
}
