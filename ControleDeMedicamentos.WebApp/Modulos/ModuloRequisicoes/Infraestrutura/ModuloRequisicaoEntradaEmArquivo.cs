using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Infraestrutura
{
    public class RepositorioRequisicaoEntradaEmArquivo
    {
        private readonly List<RequisicaoEntrada> registros = new();

        public void Inserir(RequisicaoEntrada entidade)
        {
            registros.Add(entidade);
        }

        public void Editar(Guid id, RequisicaoEntrada entidadeAtualizada)
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

        public List<RequisicaoEntrada> SelecionarTodos()
        {
            return registros;
        }

        public RequisicaoEntrada? SelecionarPorId(Guid id)
        {
            return registros.FirstOrDefault(r => r.Id == id);
        }
    }
}
