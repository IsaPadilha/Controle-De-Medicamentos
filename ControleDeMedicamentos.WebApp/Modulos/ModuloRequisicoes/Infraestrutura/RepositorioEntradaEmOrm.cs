using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura.Orm;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Infraestrutura
{
    public sealed class RepositorioRequisicaoEntradaEmOrm : IRepositorioRequisicaoEntrada
    {
        private readonly ControleDeMedicamentosDbContext dbContext;

        public RepositorioRequisicaoEntradaEmOrm(ControleDeMedicamentosDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Cadastrar(RequisicaoEntrada entidade)
        {
            dbContext.RequisicoesEntrada.Add(entidade);
            dbContext.SaveChanges();
        }

        public bool Editar(Guid idSelecionado, RequisicaoEntrada entidadeAtualizada)
        {
            var requisicaoSelecionada = SelecionarPorId(idSelecionado);

            if (requisicaoSelecionada == null)
                return false;

            requisicaoSelecionada.Atualizar(entidadeAtualizada);
            dbContext.SaveChanges();
            return true;
        }

        public bool Excluir(Guid idSelecionado)
        {
            var requisicaoSelecionada = SelecionarPorId(idSelecionado);

            if (requisicaoSelecionada == null)
                return false;

            dbContext.RequisicoesEntrada.Remove(requisicaoSelecionada);
            dbContext.SaveChanges();
            return true;
        }

        public RequisicaoEntrada? SelecionarPorId(Guid idSelecionado)
        {
            return dbContext.RequisicoesEntrada.SingleOrDefault(r => r.Id == idSelecionado);
        }

        public List<RequisicaoEntrada> SelecionarTodos()
        {
            return dbContext.RequisicoesEntrada.ToList();
        }
    }
}
