namespace ControleDeMedicamentos.WebApp.Compartilhado;

public abstract class EntidadeBase<T>
{
    public Guid Id { get; set; }

    public abstract List<string> Validar();
    public abstract void Atualizar(T entidadeAtualizada);
}