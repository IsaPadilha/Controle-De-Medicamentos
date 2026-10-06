using ControleDeMedicamentos.WebApp.Compartilhado.Infraestrutura.Orm;
using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Dominio;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Infraestrutura;

public sealed class RepositorioFornecedorEmOrm : IRepositorioFornecedor
{
    private readonly ControleDeMedicamentosDbContext dbContext;

    public RepositorioFornecedorEmOrm(ControleDeMedicamentosDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Cadastrar(Fornecedor entidade)
    {
        dbContext.Fornecedores.Add(entidade);
        dbContext.SaveChanges();
    }

    public bool Editar(Guid idSelecionado, Fornecedor entidadeAtualizada)
    {
        Fornecedor? fornecedorSelecionado = SelecionarPorId(idSelecionado);

        if (fornecedorSelecionado == null)
            return false;

        fornecedorSelecionado.Atualizar(entidadeAtualizada);

        dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Guid idSelecionado)
    {
        Fornecedor? fornecedorSelecionado = SelecionarPorId(idSelecionado);

        if (fornecedorSelecionado == null)
            return false;

        dbContext.Fornecedores.Remove(fornecedorSelecionado);

        dbContext.SaveChanges();

        return true;
    }

    public Fornecedor? SelecionarPorId(Guid idSelecionado)
    {
        return dbContext.Fornecedores.SingleOrDefault(f => f.Id == idSelecionado);
    }

    public List<Fornecedor> SelecionarTodos()
    {
        return dbContext.Fornecedores.ToList();
    }
}