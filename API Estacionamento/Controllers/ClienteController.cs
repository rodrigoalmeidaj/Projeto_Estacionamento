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
    public class ClienteController : ControllerBase
    {
        private readonly PostgresContext _context;
        private readonly IClienteService _service;

        public ClienteController(PostgresContext context, IClienteService service)
        {
            _context = context;
            _service = service;
        }

        /// <summary>
        /// Obter todos os clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterTodos")]
        public async Task<List<Cliente>> GetCliente()
        {
            return await _service.ListarTodos();
        }

        /// <summary>
        /// Obter um cliente específico por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterId/{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return BadRequest(new Mensagem("Erro", "Erro ao obter todos os clientes."));
            }

            return cliente;
        }

        /// <summary>
        /// Alterar um cliente específico por ID             
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cliente"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("Alterar/{id}")]
        public async Task<IActionResult> PutCliente(int id, [FromBody]Cliente cliente)
        {
            if (id != cliente.Id)
            {   
                return BadRequest(new Mensagem("Erro", "Erro ao alterar cliente."));
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return BadRequest(new Mensagem("Erro", "Erro ao alterar cliente."));
                }
                else
                {
                    throw;
                }
            }
            return Ok(new Mensagem("Ok", "Cliente alterado com sucesso."));
        }

        /// <summary>
        /// Incluir um cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Incluir")]
        public async Task<ActionResult<Cliente>> PostCliente([FromBody]Cliente cliente)
        {
            try
            {
                if (cliente == null)
                {
                    return BadRequest(new Mensagem("Bad Request", "Erro ao incluido cliente."));
                }
                else
                {
                    _service.Incluir(cliente);
                    return Ok(new Mensagem("Ok", "Cliente incluido com sucesso."));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Exlcuir um cliente específico por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Excluir/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return BadRequest(new Mensagem("Erro", "Erro ao exluir cliente."));
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok(new Mensagem("Ok", "Cliente excluido com sucesso."));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
    }
}
