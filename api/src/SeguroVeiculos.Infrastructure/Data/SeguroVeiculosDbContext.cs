using Microsoft.EntityFrameworkCore;
using SeguroVeiculos.Domain.Entities;
using SeguroVeiculos.Domain.ValueObjects;

namespace SeguroVeiculos.Infrastructure.Data;

public class SeguroVeiculosDbContext : DbContext
{
    public SeguroVeiculosDbContext(DbContextOptions<SeguroVeiculosDbContext> options) : base(options)
    {
    }

    public DbSet<Seguro> Seguros { get; set; }
    public DbSet<Segurado> Segurados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da entidade Segurado
        modelBuilder.Entity<Segurado>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CPF).IsRequired().HasMaxLength(11);
            entity.Property(e => e.Idade).IsRequired();
            entity.HasIndex(e => e.CPF).IsUnique();
        });

        // Configuração da entidade Seguro
        modelBuilder.Entity<Seguro>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SeguradorId).IsRequired();
            entity.Property(e => e.TaxaRisco).HasPrecision(18, 4);
            entity.Property(e => e.PremioRisco).HasPrecision(18, 2);
            entity.Property(e => e.PremioPuro).HasPrecision(18, 2);
            entity.Property(e => e.PremioComercial).HasPrecision(18, 2);
            entity.Property(e => e.ValorFinal).HasPrecision(18, 2);
            entity.Property(e => e.DataCriacao).IsRequired();

            // Configuração do Value Object Veiculo
            entity.OwnsOne(e => e.Veiculo, veiculo =>
            {
                veiculo.Property(v => v.Valor)
                    .HasColumnName("VeiculoValor")
                    .HasPrecision(18, 2)
                    .IsRequired();
                
                veiculo.Property(v => v.MarcaModelo)
                    .HasColumnName("VeiculoMarcaModelo")
                    .HasMaxLength(200)
                    .IsRequired();
            });

            // Relacionamento com Segurado
            entity.HasOne(e => e.Segurado)
                .WithMany(s => s.Seguros)
                .HasForeignKey(e => e.SeguradorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

