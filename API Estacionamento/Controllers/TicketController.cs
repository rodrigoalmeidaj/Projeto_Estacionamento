using API_Estacionamento.Interfaces;
using API_Estacionamento.Models;
using API_Estacionamento.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Estacionamento.AppDbContext;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Estacionamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly ITicketService _service;
        public TicketController(ITicketService service, PostgresContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// Obter todos ticket
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterTodos")]
        public async Task<List<Ticket>> GetTicket()
        {
            return await _service.ListarTodos();
        }

        /// <summary>
        /// Obter todos ticket não pagos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterTodosNaoPagos")]
        public async Task<List<Ticket>> GetTicketnPagos()
        {
            return await _service.ListarTodosNPagos();
        }

        /// <summary>
        /// Obter um ticket específico por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterId/{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);

            if (ticket == null)
            {
                return BadRequest(new Mensagem("Erro", "Ticket não encontrado"));
            }

            return ticket;
        }

        /// <summary>
        /// Incluir um ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Incluir")]
        public IActionResult PostTicket([FromBody] Ticket ticket)
        {
            try
            {
                if (ticket == null)
                {
                    return BadRequest(new Mensagem("Bad Request", "Erro ao incluido ticket."));
                }
                else
                {
                    _service.Incluir(ticket);
                    return Ok(new Mensagem("Ok", "Ticket incluido com sucesso."));
                }
            }
            catch (Exception)
            {

                throw;
            }            
        }

        /// <summary>
        /// Alterar um ticket específico por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Alterar/{id}")]
        public async Task<IActionResult> PutTicket(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest(new Mensagem("Erro", "Erro ao alterar ticket."));
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return BadRequest(new Mensagem("Erro", "Erro ao alterar ticket."));
                }
                else
                {
                    throw;
                }
            }

            return Ok(new Mensagem("Ok", "Ticket alterado com sucesso."));
        }

        /// <summary>
        /// Exlcuir um ticket específico por ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Excluir/{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return BadRequest(new Mensagem("Bad Request", $"Não foi possível excluir esse Ticket ID {id}, pois o mesmo não foi encontrado"));
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return Ok(new Mensagem("Ok", "Ticket excluido com sucesso."));

        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}
