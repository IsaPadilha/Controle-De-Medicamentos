using ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloFornecedores.Infraestrutura;

public sealed class FornecedorConfiguration : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.ToTable("TBFornecedor");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).ValueGeneratedNever();

        builder
            .Property(f => f.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(f => f.Cnpj)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(f => f.Telefone)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(f => f.Cnpj)
            .IsUnique();
    }
}