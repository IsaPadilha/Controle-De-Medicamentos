public abstract class EntidadeBase<TEntidade> where TEntidade : EntidadeBase<TEntidade>
{
    public Guid Id { get; set; }
    public abstract List<string> Validar();
    public abstract void Atualizar(TEntidade entidadeAtualizada);
}
