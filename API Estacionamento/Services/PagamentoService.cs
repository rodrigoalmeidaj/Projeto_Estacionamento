using API_Estacionamento.AppDbContext;
using API_Estacionamento.Interfaces;
using API_Estacionamento.Models;
using Dapper;
using Npgsql;

namespace API_Estacionamento.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly PostgresContext _context;

        public PagamentoService(PostgresContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Pagamento>> ListarTodos()
        {
            var conexaoBanco = new Conexao();
            using (var conexao = new NpgsqlConnection(conexaoBanco.StringDeConexao()))
            {
                var pagamentos = conexao.Query<Pagamento>(@"SELECT * FROM public.""Pagamento"" ORDER BY ""Id"";");
                return pagamentos.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagamento"></param>
        /// <returns></returns>
        public bool Incluir(Pagamento pagamento)
        {
            if (pagamento == null)
            {
                return false;
            }
            var conexaoBanco = new Conexao();
            using (var conexao = new NpgsqlConnection(conexaoBanco.StringDeConexao()))
            {
                int idIncrementado = conexaoBanco.IdIncrementado("Pagamento");
                pagamento.Id = idIncrementado;
            }
            _context.Pagamento.AddAsync(pagamento);
            _context.SaveChangesAsync();
            return true;
        }
    }
}
