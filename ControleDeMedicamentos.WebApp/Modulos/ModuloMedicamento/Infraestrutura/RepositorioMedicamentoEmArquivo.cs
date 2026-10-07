using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Dominio;

using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos;

public class RepositorioMedicamentoEmArquivo : RepositorioBaseEmArquivo<Medicamento>
{
    public RepositorioMedicamentoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Medicamento> ObterRegistros()
    {
        return contexto.Medicamentos;
    }
}