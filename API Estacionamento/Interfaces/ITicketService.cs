using API_Estacionamento.Models;

namespace API_Estacionamento.Interfaces
{
    public interface ITicketService
    {
        Task<List<Ticket>> ListarTodos();
        Task<List<Ticket>> ListarTodosNPagos();
        bool Incluir(Ticket ticket);
    }
}
