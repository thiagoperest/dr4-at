using Microsoft.EntityFrameworkCore;
using dr4_at.Models;

namespace dr4_at.Data
{
    public class Dr4AtContext : DbContext
    {
        public Dr4AtContext(DbContextOptions<Dr4AtContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Destino> Destinos { get; set; }
        public DbSet<PacoteTuristico> PacotesTuristicos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<PacoteDestino> PacoteDestinos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            });

            modelBuilder.Entity<Destino>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Cidade).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Pais).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DeletedAt);
            });

            modelBuilder.Entity<PacoteTuristico>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DataInicio).IsRequired();
                entity.Property(e => e.CapacidadeMaxima).IsRequired();
                entity.Property(e => e.Preco).IsRequired().HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DataReserva).IsRequired();

                entity.HasOne(e => e.Cliente)
                    .WithMany(c => c.Reservas)
                    .HasForeignKey(e => e.ClienteId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.PacoteTuristico)
                    .WithMany(p => p.Reservas)
                    .HasForeignKey(e => e.PacoteTuristicoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PacoteDestino>(entity =>
            {
                entity.HasKey(e => new { e.PacoteTuristicoId, e.DestinoId });
                
                entity.HasOne(e => e.PacoteTuristico)
                    .WithMany(p => p.PacoteDestinos)
                    .HasForeignKey(e => e.PacoteTuristicoId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(e => e.Destino)
                    .WithMany(d => d.PacoteDestinos)
                    .HasForeignKey(e => e.DestinoId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.Property(e => e.DataInclusao).IsRequired();
                entity.Property(e => e.OrdemVisita).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}