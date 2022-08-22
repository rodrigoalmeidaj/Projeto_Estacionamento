#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Estacionamento.AppDbContext;
using API_Estacionamento.Models;
using API_Estacionamento.Classes;
using API_Estacionamento.Interfaces;

namespace API_Estacionamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly IPagamentoService _service;
        public PagamentoController(IPagamentoService service, PostgresContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// Obter todos pagamentos
        /// </summary>
        /// <returns>Lista de pagamento</returns>
        [HttpGet]
        [Route("ObterTodos")]
        public async Task<List<Pagamento>> GetPagamento()
        {
            return await _service.ListarTodos();
        }

        /// <summary>
        /// Obter um pagamento específico por ID
        /// </summary>
        /// <param name="id">ID pagamento</param>
        /// <returns>Pagamento</returns>
        [HttpGet]
        [Route("ObterId/{id}")]
        public async Task<ActionResult<Pagamento>> GetPagamento(int id)
        {
            var pagamento = await _context.Pagamento.FindAsync(id);

            if (pagamento == null)
            {
                return BadRequest(new Mensagem("Erro", "Erro ao obter pagamento"));
            }

            return pagamento;
        }

        /// <summary>
        /// Alterar um pagamento específico por ID
        /// </summary>
        /// <param name="id">ID Pagamento</param>
        /// <param name="pagamento">Pagamento</param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("Alterar/{id}")]
        public async Task<IActionResult> PutPagamento(int id, Pagamento pagamento)
        {
            if (id != pagamento.Id)
            {
                return BadRequest(new Mensagem("Erro", "Erro ao alterar pagamento."));
            }

            _context.Entry(pagamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagamentoExists(id))
                {
                    return BadRequest(new Mensagem("Erro", "Erro ao alterar pagamento."));
                }
                else
                {
                    throw;
                }
            }
            return Ok(new Mensagem("Ok", "Pagamento alterado com sucesso."));
        }

        /// <summary>
        /// Incluir um pagamento
        /// </summary>
        /// <param name="pagamento">Pagamento</param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Incluir")]
        public async Task<ActionResult<Pagamento>> PostPagamento(Pagamento pagamento)
        {
            try
            {
                if (pagamento == null)
                {
                    return BadRequest(new Mensagem("Bad Request", "Erro ao incluir pagamento."));
                }
                else
                {
                    _service.Incluir(pagamento);
                    return Ok(new Mensagem("Ok", "Pagamento incluido com sucesso."));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Exlcuir um pagamento específico por ID
        /// </summary>
        /// <param name="id">ID Pagamento</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Excluir/{id}")]
        public async Task<IActionResult> DeletePagamento(int id)
        {
            var pagamento = await _context.Pagamento.FindAsync(id);
            if (pagamento == null)
            {
                return BadRequest(new Mensagem("Erro", "Erro ao excluir pagamento."));
            }

            _context.Pagamento.Remove(pagamento);
            await _context.SaveChangesAsync();

            return Ok(new Mensagem("Ok", "Pagamento excluido com sucesso."));
        }

        private bool PagamentoExists(int id)
        {
            return _context.Pagamento.Any(e => e.Id == id);
        }
    }
}
