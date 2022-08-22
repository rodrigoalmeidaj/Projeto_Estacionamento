using API_Estacionamento.Models;

namespace API_Estacionamento.Interfaces
{
    public interface IPagamentoService
    {
        Task<List<Pagamento>> ListarTodos();
        bool Incluir(Pagamento pagamento);
    }
}
