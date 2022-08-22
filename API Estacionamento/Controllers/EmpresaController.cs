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
    public class EmpresaController : ControllerBase
    {
        private readonly PostgresContext _context;

        public EmpresaController(PostgresContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter todas empresa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterTodos")]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresa()
        {
            List<Empresa> listaEmpresa =  await _context.Empresa.ToListAsync();
            return listaEmpresa;
        }

        /// <summary>
        /// Obter uma empresa específica por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterId/{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _context.Empresa.FindAsync(id);

            if (empresa == null)
            {
                return BadRequest(new Mensagem("Erro", "Erro ao obter empresa"));
            }

            return empresa;
        }

        /// <summary>
        /// Alterar uma empresa específica por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empresa"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("Alterar/{id}")]
        public async Task<IActionResult> PutEmpresa(int id, Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return BadRequest(new Mensagem("Erro", "Erro ao alterar empresa")); 
            }

            _context.Entry(empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
                {
                    return BadRequest(new Mensagem("Erro", "Erro ao alterar empresa."));
                }
                else
                {
                    throw;
                }
            }
            return Ok(new Mensagem("Ok", "Empresa alterada com sucesso."));
        }

        /// <summary>
        /// Incluir uma empresa
        /// </summary>
        /// <param name="empresa"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Incluir")]
        public async Task<ActionResult<Empresa>> PostEmpresa(Empresa empresa)
        {
            _context.Empresa.Add(empresa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpresa", new { id = empresa.Id }, empresa);
        }

        /// <summary>
        /// Excluir uma empresa específica por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Excluir/{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _context.Empresa.FindAsync(id);
            if (empresa == null)
            {
                return BadRequest(new Mensagem("Erro", "Erro ao excluir empresa."));
            }

            _context.Empresa.Remove(empresa);
            await _context.SaveChangesAsync();

            return Ok(new Mensagem("Ok", "Empresa excluido com sucesso."));
        }

        private bool EmpresaExists(int id)
        {
            return _context.Empresa.Any(e => e.Id == id);
        }
    }
}
