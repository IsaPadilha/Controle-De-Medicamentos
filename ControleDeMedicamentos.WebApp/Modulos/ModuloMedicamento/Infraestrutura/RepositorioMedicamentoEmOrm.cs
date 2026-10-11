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
                .Include(m => m.Requisicoes)
                .Include(m => m.Saidas)
                .SingleOrDefault(m => m.Id == idSelecionado);
        }

        public List<Medicamento> SelecionarTodos()
        {
            var medicamentos = dbContext.Medicamentos
                .Include(m => m.Fornecedor)
                .Include(m => m.Requisicoes)
                .ToList();

            var saidas = dbContext.RequisicoesSaida
                .Include(r => r.MedicamentoPrescritos)
                    .ThenInclude(mp => mp.Medicamento)
                .ToList();

            foreach (var medicamento in medicamentos)
            {
                medicamento.Saidas = saidas
                    .Where(s => s.MedicamentoPrescritos
                        .Any(mp => mp.Medicamento.Id == medicamento.Id))
                    .ToList();
            }

            return medicamentos;
        }
    }
}
