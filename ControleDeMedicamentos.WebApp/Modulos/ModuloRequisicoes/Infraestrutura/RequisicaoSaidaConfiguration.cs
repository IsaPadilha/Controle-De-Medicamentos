using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Infraestrutura
{
    public sealed class RequisicaoSaidaConfiguration : IEntityTypeConfiguration<RequisicaoSaida>
    {
        public void Configure(EntityTypeBuilder<RequisicaoSaida> builder)
        {
            builder.ToTable("TBRequisicaoSaida");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedNever();

            builder
                .Property(r => r.Data)
                .IsRequired();

            builder
                .HasOne(r => r.Paciente)
                .WithMany() // se quiser, pode criar uma coleção em Paciente
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com MedicamentoPrescrito
            builder
                .HasMany(r => r.MedicamentoPrescritos)
                .WithOne(mp => mp.RequisicaoSaida) // precisa existir a propriedade de navegação em MedicamentoPrescrito
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
