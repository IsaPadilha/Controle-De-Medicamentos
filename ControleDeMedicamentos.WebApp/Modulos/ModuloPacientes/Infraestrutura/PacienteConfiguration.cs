using ControleDeMedicamentos.WebApp.Modulos.ModuloPaciente.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloPaciente.Infraestrutura
{
    public sealed class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("TBPaciente");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();

            builder
                .Property(p => p.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(p => p.Telefone)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(p => p.CartaoSus)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(p => p.Cpf)
                .HasMaxLength(11)
                .IsRequired();

            builder.HasIndex(p => p.Cpf)
                .IsUnique();

            builder.HasIndex(p => p.CartaoSus)
                .IsUnique();
        }
    }
}
