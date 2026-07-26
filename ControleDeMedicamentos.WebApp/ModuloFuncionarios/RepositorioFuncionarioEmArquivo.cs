using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario;

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
