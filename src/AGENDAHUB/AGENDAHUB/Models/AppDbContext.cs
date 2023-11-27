using Microsoft.EntityFrameworkCore;


namespace AGENDAHUB.Models
{
    public class AppDbContext : DbContext
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
        public DbSet<ServicoProfissional> ServicoProfissional { get; set; }



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

            modelBuilder.Entity<Agendamentos>()
                .HasOne(a => a.Caixa)
                .WithOne(c => c.Agendamento)
                .HasForeignKey<Caixa>(c => c.ID_Agendamento);

            modelBuilder.Entity<Configuracao>()
                .Property(c => c.DiasDaSemanaJson)
                .HasColumnName("DiasDaSemanaJson")
                .IsRequired();

            modelBuilder.Entity<Configuracao>()
                .Property(c => c.HoraInicio)
                .IsRequired();

            modelBuilder.Entity<ServicoProfissional>()
                .HasKey(sp => new { sp.ID_Servico, sp.ID_Profissional });

            modelBuilder.Entity<ServicoProfissional>()
                .HasOne(sp => sp.Servico)
                .WithMany(s => s.ServicosProfissionais)
                .HasForeignKey(sp => sp.ID_Servico);

            modelBuilder.Entity<ServicoProfissional>()
                .HasOne(sp => sp.Profissional)
                .WithMany(p => p.ServicosProfissionais)
                .HasForeignKey(sp => sp.ID_Profissional);

            base.OnModelCreating(modelBuilder);
        }
    }
}