using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Infraestrutura
{
    public sealed class RequisicaoEntradaConfiguration : IEntityTypeConfiguration<RequisicaoEntrada>
    {
        public void Configure(EntityTypeBuilder<RequisicaoEntrada> builder)
        {
            builder.ToTable("TBRequisicaoEntrada");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedNever();

            builder
                .Property(r => r.Data)
                .IsRequired();

            builder
                .Property(r => r.Quantidade)
                .IsRequired();

            builder
                .HasOne(r => r.Medicamento)
                .WithMany() // se quiser, pode criar uma coleção em Medicamento
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(r => r.Funcionario)
                .WithMany() // se quiser, pode criar uma coleção em Funcionario
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
