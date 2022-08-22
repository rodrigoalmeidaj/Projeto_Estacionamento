using API_Estacionamento.AppDbContext;
using API_Estacionamento.Interfaces;
using API_Estacionamento.Models;
using Dapper;
using Npgsql;

namespace API_Estacionamento.Services
{
    public class ClienteServico : IClienteService
    {
        private readonly PostgresContext _context;

        public ClienteServico(PostgresContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> ListarTodos()
        {
            var conexaoBanco = new Conexao();
            using (var conexao = new NpgsqlConnection(conexaoBanco.StringDeConexao()))
            {
                var clientes = conexao.Query<Cliente>(@"SELECT * FROM public.""Cliente"" ORDER BY ""Id"";");
                return clientes.ToList();
            }
        }

        public bool Incluir(Cliente cliente)
        {
            if (cliente == null)
            {
                return false;
            }
            var conexaoBanco = new Conexao();
            using (var conexao = new NpgsqlConnection(conexaoBanco.StringDeConexao()))
            {
                int idIncrementado = conexaoBanco.IdIncrementado("Cliente");
                cliente.Id = idIncrementado;
            }
            _context.Cliente.AddAsync(cliente);
            _context.SaveChangesAsync();
            return true;
        }
    }
}
