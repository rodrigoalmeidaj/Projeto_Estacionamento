using API_Estacionamento.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Estacionamento.AppDbContext
{
    public class PostgresContext : DbContext
    {
        public PostgresContext(DbContextOptions<PostgresContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                
            }
        }

        public virtual DbSet<Cliente> Cliente { get; set; } = null!;
        public virtual DbSet<Ticket> Ticket { get; set; } = null!;
        public virtual DbSet<Empresa> Empresa { get; set; } = null!;
        public virtual DbSet<Pagamento> Pagamento { get; set; } = null!;
        public virtual DbSet<Veiculo> Veiculo { get; set; } = null!;
    }
}
