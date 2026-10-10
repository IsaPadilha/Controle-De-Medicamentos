using ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloFuncionarios.Infraestrutura
{
    public sealed class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.ToTable("TBFuncionario");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedNever();

            builder
                .Property(f => f.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(f => f.Cpf)
                .HasMaxLength(11)
                .IsRequired();

            builder
                .Property(f => f.Telefone)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(f => f.Cpf)
                .IsUnique();
        }
    }
}
