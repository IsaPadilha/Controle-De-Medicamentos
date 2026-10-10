using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Modulos.ModuloPacientes.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura.Orm;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloPacientes.Infraestrutura;

public sealed class RepositorioPacienteEmOrm : IRepositorioPaciente
{
    private readonly ControleDeMedicamentosDbContext dbContext;

    public RepositorioPacienteEmOrm(ControleDeMedicamentosDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Cadastrar(Paciente entidade)
    {
        dbContext.Pacientes.Add(entidade);
        dbContext.SaveChanges();
    }

    public bool Editar(Guid idSelecionado, Paciente entidadeAtualizada)
    {
        Paciente? pacienteSelecionado = SelecionarPorId(idSelecionado);

        if (pacienteSelecionado == null)
            return false;

        pacienteSelecionado.Atualizar(entidadeAtualizada);

        dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Guid idSelecionado)
    {
        Paciente? pacienteSelecionado = SelecionarPorId(idSelecionado);

        if (pacienteSelecionado == null)
            return false;

        dbContext.Pacientes.Remove(pacienteSelecionado);

        dbContext.SaveChanges();

        return true;
    }

    public Paciente? SelecionarPorId(Guid idSelecionado)
    {
        return dbContext.Pacientes
            .SingleOrDefault(p => p.Id == idSelecionado);
    }

    public List<Paciente> SelecionarTodos()
    {
        return dbContext.Pacientes.ToList();
    }
}