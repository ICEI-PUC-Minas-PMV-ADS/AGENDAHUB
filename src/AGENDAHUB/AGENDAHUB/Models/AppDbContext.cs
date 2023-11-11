using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AGENDAHUB.Models
{
    public class AppDbContext : IdentityDbContext<Usuario, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Servicos> Servicos { get; set; }
        public DbSet<Agendamentos> Agendamentos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Profissionais> Profissionais { get; set; }
        public DbSet<Configuracao> Configuracao { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Caixa> Caixa { get; set; }


        //Para deixar unico o nome de usuario
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.NomeUsuario)
                .IsUnique();

            // Configuração do relacionamento um-para-um entre Usuario e Configuracao
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Configuracao)
                .WithOne(c => c.Usuario)
                .HasForeignKey<Configuracao>(c => c.UsuarioID);

            // Configuração do relacionamento um-para-um entre Profissionais e Usuario
            modelBuilder.Entity<Profissionais>()
                .HasOne(p => p.Usuario)
                .WithOne(u => u.Profissionais)
                .HasForeignKey<Profissionais>(p => p.UsuarioID);

            modelBuilder.Entity<Agendamentos>()
                .HasOne(a => a.Caixa)
                .WithOne(c => c.Agendamento)
                .HasForeignKey<Caixa>(c => c.ID_Agendamento);

            modelBuilder.Entity<Usuario>().ToTable("Usuarios");

            base.OnModelCreating(modelBuilder);
        }

    }
}