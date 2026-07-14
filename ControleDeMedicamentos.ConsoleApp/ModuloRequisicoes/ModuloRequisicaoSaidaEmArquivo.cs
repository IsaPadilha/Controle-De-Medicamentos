using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes.RequisicaoSaida;

public class ModuloRequisicaoSaidaEmArquivo : RepositorioBaseEmArquivo<RequisicaoSaida>
{
    public ModuloRequisicaoSaidaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<RequisicaoSaida> ObterRegistros()
    {
        return contexto.RequisicoesSaida;
    }
}
