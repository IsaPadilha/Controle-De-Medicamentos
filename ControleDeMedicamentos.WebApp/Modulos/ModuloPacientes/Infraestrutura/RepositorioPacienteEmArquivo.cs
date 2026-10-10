using System;
using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.Modulos.ModuloPacientes.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public class RepositorioPacienteEmArquivo : RepositorioBaseEmArquivo<Paciente>
{
    public RepositorioPacienteEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Paciente> ObterRegistros()
    {
        return contexto.Pacientes;
    }
}
