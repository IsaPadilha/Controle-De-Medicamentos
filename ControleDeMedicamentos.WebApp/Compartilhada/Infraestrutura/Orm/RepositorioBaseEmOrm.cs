using Microsoft.EntityFrameworkCore;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura.Orm;

public abstract class RepositorioBaseEmOrm<T> : IRepositorio<T> where T : EntidadeBase<T>
{
    protected readonly DbContext contexto;
    protected readonly DbSet<T> registros;

    protected RepositorioBaseEmOrm(DbContext contexto)
    {
        this.contexto = contexto;
        registros = contexto.Set<T>();
    }

    public virtual void Cadastrar(T entidade)
    {
        registros.Add(entidade);
        contexto.SaveChanges();
    }

    public virtual bool Editar(Guid id, T entidadeAtualizada)
    {
        T? entidade = SelecionarPorId(id);

        if (entidade is null)
            return false;

        contexto.Entry(entidade).CurrentValues.SetValues(entidadeAtualizada);

        contexto.SaveChanges();

        return true;
    }

    public virtual bool Excluir(Guid id)
    {
        T? entidade = SelecionarPorId(id);

        if (entidade is null)
            return false;

        registros.Remove(entidade);

        contexto.SaveChanges();

        return true;
    }

    public virtual T? SelecionarPorId(Guid id)
    {
        return registros.FirstOrDefault(x => x.Id == id);
    }

    public virtual List<T> SelecionarTodos()
    {
        return registros.ToList();
    }
}