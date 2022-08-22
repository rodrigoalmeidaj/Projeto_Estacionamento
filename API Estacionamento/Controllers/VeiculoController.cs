#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Estacionamento.AppDbContext;
using API_Estacionamento.Models;
using API_Estacionamento.Classes;

namespace API_Estacionamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly PostgresContext _context;

        public VeiculoController(PostgresContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter todos veículos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterTodos")]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculo()
        {
            return await _context.Veiculo.ToListAsync();
        }

        /// <summary>
        /// Obter um veículo específico por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterId/{id}")]
        public async Task<ActionResult<Veiculo>> GetVeiculo(int id)
        {
            var veiculo = await _context.Veiculo.FindAsync(id);

            if (veiculo == null)
            {
                return BadRequest(new Mensagem("Erro", "Veículo não encontrado"));
            }

            return veiculo;
        }

        /// <summary>
        /// Alterar um veículo específico por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="veiculo"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("Alterar/{id}")]
        public async Task<IActionResult> PutVeiculo(int id, Veiculo veiculo)
        {
            if (id != veiculo.Id)
            {
                return BadRequest(new Mensagem("Erro", "Erro ao alterar veículo."));
            }

            _context.Entry(veiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoExists(id))
                {
                    return BadRequest(new Mensagem("Erro", "Erro ao alterar veículo."));
                }
                else
                {
                    throw;
                }
            }

            return Ok(new Mensagem("Ok", "Veículo alterado com sucesso."));
        }

        /// <summary>
        /// Incluir um veículo
        /// </summary>
        /// <param name="veiculo"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Incluir")]
        public async Task<ActionResult<Veiculo>> PostVeiculo(Veiculo veiculo)
        {
            _context.Veiculo.Add(veiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVeiculo", new { id = veiculo.Id }, veiculo);
        }

        /// <summary>
        /// Excluir um veículo específico por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Excluir/{id}")]
        public async Task<IActionResult> DeleteVeiculo(int id)
        {
            var veiculo = await _context.Veiculo.FindAsync(id);
            if (veiculo == null)
            {
                return BadRequest(new Mensagem("Bad Request", "Erro ao excluir veículo."));
            }

            _context.Veiculo.Remove(veiculo);
            await _context.SaveChangesAsync();

            return Ok(new Mensagem("Ok", "Veículo excluido com sucesso."));
        }

        private bool VeiculoExists(int id)
        {
            return _context.Veiculo.Any(e => e.Id == id);
        }
    }
}
