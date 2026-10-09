namespace ControleDeMedicamentos.WebApp.Compartilhado
{
    public interface IRepositorio<TEntidade>
    {
        void Cadastrar(TEntidade entidade);
        bool Editar(Guid id, TEntidade entidadeAtualizada);
        bool Excluir(Guid id);
        List<TEntidade> SelecionarTodos();
        TEntidade? SelecionarPorId(Guid id);
    }
}
