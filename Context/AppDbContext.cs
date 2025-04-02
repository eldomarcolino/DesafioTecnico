using SistemaDeRecarga.Model;
using Microsoft.EntityFrameworkCore;

namespace SistemaDeRecarga.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Definir as tabelas como DbSet
        public DbSet<User> User { get; set; }
        public DbSet<Balance> Balance { get; set; }
        public DbSet<Transacao> Transaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuração de índices únicos
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(x => x.RegistrationNumber).IsUnique();




            //Configuração de relacionamentos



            modelBuilder.Entity<Balance>() //Configuraçao para Salso
                .HasOne(u => u.User)
                .WithOne(b => b.Balance)
                .HasForeignKey<Balance>(b => b.IdUser);

            modelBuilder.Entity<Transacao>() // Configuraçao para Transiçao
                .HasOne(t => t.User)
                .WithMany(u => u.Transaction)
                .HasForeignKey(t => t.IdUser);



            base.OnModelCreating(modelBuilder);
        }
    }
}