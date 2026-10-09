using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Infraestrutura;

public class RepositorioRequisicaoSaidaEmArquivo : RepositorioBaseEmArquivo<RequisicaoSaida>
{
    public RepositorioRequisicaoSaidaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<RequisicaoSaida> ObterRegistros()
    {
        return contexto.RequisicoesSaida;
    }
}
