using API_Estacionamento.Models;

namespace API_Estacionamento.Interfaces
{
    public interface IClienteService
    {
        Task<List<Cliente>> ListarTodos();
        bool Incluir(Cliente cliente);
    }
}
