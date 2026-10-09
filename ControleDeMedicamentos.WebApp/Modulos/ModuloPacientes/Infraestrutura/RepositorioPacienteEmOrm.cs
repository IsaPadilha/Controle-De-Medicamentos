using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Modulos.ModuloPaciente.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura.Orm;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloPaciente.Infraestrutura
{
    public sealed class RepositorioPacienteEmOrm : IRepositorioPaciente
    {
        private readonly ControleDeMedicamentosDbContext dbContext;

        public RepositorioPacienteEmOrm(ControleDeMedicamentosDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Inserir(Paciente entidade)
        {
            dbContext.Pacientes.Add(entidade);
            dbContext.SaveChanges();
        }

        public void Editar(Guid idSelecionado, Paciente entidadeAtualizada)
        {
            var pacienteSelecionado = SelecionarPorId(idSelecionado);

            if (pacienteSelecionado == null)
                return;

            pacienteSelecionado.Atualizar(entidadeAtualizada);
            dbContext.SaveChanges();
        }

        public void Excluir(Guid idSelecionado)
        {
            var pacienteSelecionado = SelecionarPorId(idSelecionado);

            if (pacienteSelecionado == null)
                return;

            dbContext.Pacientes.Remove(pacienteSelecionado);
            dbContext.SaveChanges();
        }

        public Paciente? SelecionarPorId(Guid idSelecionado)
        {
            return dbContext.Pacientes.SingleOrDefault(p => p.Id == idSelecionado);
        }

        public List<Paciente> SelecionarTodos()
        {
            return dbContext.Pacientes.ToList();
        }
    }
}
