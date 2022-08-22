using API_Estacionamento.AppDbContext;
using API_Estacionamento.Interfaces;
using API_Estacionamento.Models;
using Dapper;
using Npgsql;

namespace API_Estacionamento.Services
{
    public class TicketService : ITicketService
    {
        private readonly PostgresContext _context;

        public TicketService(PostgresContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Ticket>> ListarTodos()
        {
            var conexaoBanco = new Conexao();
            using (var conexao = new NpgsqlConnection(conexaoBanco.StringDeConexao()))
            {
                var tickets = conexao.Query<Ticket>(@"SELECT * FROM public.""Ticket"" ORDER BY ""Id"";");
                return tickets.ToList();
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Ticket>> ListarTodosNPagos()
        {
            var conexaoBanco = new Conexao();
            using (var conexao = new NpgsqlConnection(conexaoBanco.StringDeConexao()))
            {
                var tickets = conexao.Query<Ticket>(@"SELECT * FROM public.""Ticket"" WHERE ""Pago"" = false ORDER BY ""Id"";");
                return tickets.ToList();
            }
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public bool Incluir(Ticket ticket)
        {
            if (ticket == null)
            {
                return false;
            }
            var conexaoBanco = new Conexao();
            using (var conexao = new NpgsqlConnection(conexaoBanco.StringDeConexao()))
            {
                int idIncrementado = conexaoBanco.IdIncrementado("Ticket");
                //var sql = new StringBuilder();
                //sql.Append(@"INSERT INTO public.""Ticket"" (");
                //sql.Append(@"""Id"", ""Entrada"", ""Saida"", ""Cliente"", ""Placa"", ""Veiculo"", ""Valor"", ");
                //sql.Append(@"""ValorTotal"", ""Desconto"", ""FormaPagamento"", ""Tempo"", ""ObservacaoTicket"")");
                //sql.Append(@" Values (");
                //sql.Append($"'{idIncrementado}', '{ticket.Entrada}', '{ticket.Saida}', '{ticket.Cliente}', '{ticket.Placa}', '{ticket.Veiculo}', '{ticket.Valor}', ");
                //sql.Append($"'{ticket.ValorTotal}', '{ticket.Desconto}', '{ticket.FormaPagamento}', '{ticket.Tempo}', '{ticket.ObservacaoTicket}');");
                //var regAfetados = conexao.Execute(sql.ToString());
                //return (regAfetados > 0);
                ticket.Id = idIncrementado;
            }
            _context.Ticket.AddAsync(ticket);
            _context.SaveChangesAsync();
            return true;
        }
               
    }
}
