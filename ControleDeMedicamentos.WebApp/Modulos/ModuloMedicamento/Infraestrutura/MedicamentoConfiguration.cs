using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Infraestrutura
{
    public sealed class MedicamentoConfiguration : IEntityTypeConfiguration<Medicamento>
    {
        public void Configure(EntityTypeBuilder<Medicamento> builder)
        {
            builder.ToTable("TBMedicamento");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedNever();

            builder
                .Property(m => m.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(m => m.Descricao)
                .HasMaxLength(255)
                .IsRequired();

            // Quantidade em estoque não é armazenada diretamente,
            // mas se você quiser persistir, pode mapear como coluna.
            builder
                .Ignore(m => m.QuantidadeEmEstoque);

            // Relacionamento com Fornecedor
            builder
                .HasOne(m => m.Fornecedor)
                .WithMany()
                .IsRequired();

            // Opcional: índices
            builder.HasIndex(m => m.Nome)
                .IsUnique();
        }
    }
}
