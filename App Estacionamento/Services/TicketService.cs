using App_Estacionamento.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App_Estacionamento.Services
{
    public class TicketService
    {
        // Recebe a lista de produtos e incluir no SelectListItem
        public List<SelectListItem> SelectListTicket(List<Ticket> tickets)
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            foreach (var item in tickets)
            {
                lista.Add(new SelectListItem(item.Cliente, item.Id.ToString()));
            }
            return lista;
        }

        // Busca na api um lista de Tickes para usar na controler
        public async Task<List<Ticket>> ListarTickets(string url)
        {
            List<Ticket> tickets = new List<Ticket>();
            using (var httpCliente = new HttpClient())
            {               
                httpCliente.BaseAddress = new Uri(url);
                var respostaCliente = await httpCliente.GetAsync("");
                if (respostaCliente.IsSuccessStatusCode)
                {
                    var lerTask = respostaCliente.Content.ReadAsAsync<List<Ticket>>();
                    lerTask.Wait();
                    tickets = lerTask.Result;
                }
                return tickets;
            }
        }
    }
}
