using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura.Orm;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Infraestrutura
{
    public sealed class RepositorioRequisicaoSaidaEmOrm : IRepositorioRequisicaoSaida
    {
        private readonly ControleDeMedicamentosDbContext dbContext;

        public RepositorioRequisicaoSaidaEmOrm(ControleDeMedicamentosDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Cadastrar(RequisicaoSaida entidade)
        {
            dbContext.RequisicoesSaida.Add(entidade);
            dbContext.SaveChanges();
        }

        public bool Editar(Guid idSelecionado, RequisicaoSaida entidadeAtualizada)
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

            dbContext.RequisicoesSaida.Remove(requisicaoSelecionada);
            dbContext.SaveChanges();
            return true;
        }

        public RequisicaoSaida? SelecionarPorId(Guid idSelecionado)
        {
            return dbContext.RequisicoesSaida.SingleOrDefault(r => r.Id == idSelecionado);
        }

        public List<RequisicaoSaida> SelecionarTodos()
        {
            return dbContext.RequisicoesSaida.ToList();
        }
    }
}
