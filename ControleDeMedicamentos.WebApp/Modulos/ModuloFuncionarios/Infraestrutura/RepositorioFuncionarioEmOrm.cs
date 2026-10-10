using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura.Orm;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Infraestrutura
{
    public sealed class RepositorioFuncionarioEmOrm : RepositorioBaseEmOrm<Funcionario>, IRepositorioFuncionario
    {
        private readonly ControleDeMedicamentosDbContext dbContext;

        public RepositorioFuncionarioEmOrm(
    ControleDeMedicamentosDbContext dbContext)
    : base(dbContext)
        {
            this.dbContext = dbContext;
        }


        public void Cadastrar(Funcionario entidade)
        {
            dbContext.Funcionarios.Add(entidade);
            dbContext.SaveChanges();
        }

        public bool Editar(Guid idSelecionado, Funcionario entidadeAtualizada)
        {
            var funcionarioSelecionado = SelecionarPorId(idSelecionado);

            if (funcionarioSelecionado == null)
                return false;

            funcionarioSelecionado.Atualizar(entidadeAtualizada);
            dbContext.SaveChanges();
            return true;
        }

        public bool Excluir(Guid idSelecionado)
        {
            var funcionarioSelecionado = SelecionarPorId(idSelecionado);

            if (funcionarioSelecionado == null)
                return false;

            dbContext.Funcionarios.Remove(funcionarioSelecionado);
            dbContext.SaveChanges();
            return true;
        }

        public Funcionario? SelecionarPorId(Guid idSelecionado)
        {
            return dbContext.Funcionarios.SingleOrDefault(f => f.Id == idSelecionado);
        }

        public List<Funcionario> SelecionarTodos()
        {
            return dbContext.Funcionarios.ToList();
        }
    }
}
