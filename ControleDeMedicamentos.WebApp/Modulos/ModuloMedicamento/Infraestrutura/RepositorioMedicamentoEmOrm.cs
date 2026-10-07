using ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura.Orm;
using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Dominio;
using Microsoft.EntityFrameworkCore;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Infraestrutura
{
    public sealed class RepositorioMedicamentoEmOrm : IRepositorioMedicamento
    {
        private readonly ControleDeMedicamentosDbContext dbContext;

        public RepositorioMedicamentoEmOrm(ControleDeMedicamentosDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Cadastrar(Medicamento entidade)
        {
            dbContext.Medicamentos.Add(entidade);
            dbContext.SaveChanges();
        }

        public bool Editar(Guid idSelecionado, Medicamento entidadeAtualizada)
        {
            Medicamento? medicamentoSelecionado = SelecionarPorId(idSelecionado);

            if (medicamentoSelecionado == null)
                return false;

            medicamentoSelecionado.Atualizar(entidadeAtualizada);

            dbContext.SaveChanges();

            return true;
        }

        public bool Excluir(Guid idSelecionado)
        {
            Medicamento? medicamentoSelecionado = SelecionarPorId(idSelecionado);

            if (medicamentoSelecionado == null)
                return false;

            dbContext.Medicamentos.Remove(medicamentoSelecionado);

            dbContext.SaveChanges();

            return true;
        }

        public Medicamento? SelecionarPorId(Guid idSelecionado)
        {
            return dbContext.Medicamentos
                .Include(m => m.Fornecedor) // já traz o fornecedor junto
                .SingleOrDefault(m => m.Id == idSelecionado);
        }

        public List<Medicamento> SelecionarTodos()
        {
            return dbContext.Medicamentos
                .Include(m => m.Fornecedor) // lista com fornecedor carregado
                .ToList();
        }
    }
}
