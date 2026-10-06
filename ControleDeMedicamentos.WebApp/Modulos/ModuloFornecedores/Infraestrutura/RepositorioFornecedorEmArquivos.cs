using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores
{
    public abstract class RepositorioBaseEmArquivo<TEntidade>
    where TEntidade : EntidadeBase<TEntidade>
    {
        protected ContextoJson contexto;

        protected RepositorioBaseEmArquivo(ContextoJson contexto)
        {
            this.contexto = contexto;
        }

        protected abstract List<TEntidade> ObterRegistros();
    }

}
