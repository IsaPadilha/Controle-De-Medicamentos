using ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura.Orm;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionario.Dominio;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionario.Infraestrutura
{
    public sealed class RepositorioFuncionarioEmOrm : IRepositorioFuncionario
    {
        private readonly ControleDeMedicamentosDbContext dbContext;

        public RepositorioFuncionarioEmOrm(ControleDeMedicamentosDbContext dbContext)
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
            Funcionario? funcionarioSelecionado = SelecionarPorId(idSelecionado);

            if (funcionarioSelecionado == null)
                return false;

            funcionarioSelecionado.Atualizar(entidadeAtualizada);

            dbContext.SaveChanges();

            return true;
        }

        public bool Excluir(Guid idSelecionado)
        {
            Funcionario? funcionarioSelecionado = SelecionarPorId(idSelecionado);

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
