using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionario.Dominio;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionario.Infraestrutura
{
    public class RepositorioFuncionarioEmArquivo
    {
        private readonly List<Funcionario> registros = new();

        public void Inserir(Funcionario entidade)
        {
            registros.Add(entidade);
        }

        public void Editar(Guid id, Funcionario entidadeAtualizada)
        {
            var existente = registros.FirstOrDefault(r => r.Id == id);
            if (existente != null)
                existente.Atualizar(entidadeAtualizada);
        }

        public void Excluir(Guid id)
        {
            var existente = registros.FirstOrDefault(r => r.Id == id);
            if (existente != null)
                registros.Remove(existente);
        }

        public List<Funcionario> SelecionarTodos()
        {
            return registros;
        }

        public Funcionario? SelecionarPorId(Guid id)
        {
            return registros.FirstOrDefault(r => r.Id == id);
        }
    }
}
