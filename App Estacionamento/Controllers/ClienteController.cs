using App_Estacionamento.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace App_Estacionamento.Controllers
{
    //[Authorize]
    public class ClienteController : Controller
    {
        // URL da api Rodrigo
        const string urlApi = "https://localhost:7210/api/Cliente/";
        // URL da api Carlos
        //const string urlApi = "https://localhost:5007/api/Cliente/";
        // ObterTodos
        // ObterTodosNaoPagos
        // ObterId/{id}
        // Incluir
        // Alterar/{id}
        // Excluir/{id}

        /// <summary>
        /// Página listar clientes 
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        public async Task<ActionResult> ListarClientes()
        {
            IEnumerable<Cliente> clientes = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(urlApi + "ObterTodos");

                var resposta = await httpClient.GetAsync("");
                if (resposta.IsSuccessStatusCode)
                {
                    var lerTask = resposta.Content.ReadAsAsync<IList<Cliente>>();
                    lerTask.Wait();
                    clientes = lerTask.Result;
                }
                else
                {
                    clientes = Enumerable.Empty<Cliente>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor.");
                }
                return View(clientes);
            }
        }

        /// <summary>
        /// Página incluir cliente
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        public ActionResult IncluirCliente()
        {

            Cliente cliente = new Cliente();
            cliente.DataCadastro = DateTime.Now;
            return View(cliente);
        }

        /// <summary>
        /// Metodo para incluir um novo cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        //[Authorize]
        public async Task<ActionResult> BtnIncluirCliente(Cliente cliente)
        {
            try
            {
                if (cliente == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                using (var httpClient = new HttpClient())
                {

                    httpClient.BaseAddress = new Uri(urlApi + "Incluir");
                    var resposta = await httpClient.PostAsJsonAsync<Cliente>("", cliente);
                    if (resposta.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListarClientes");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Erro no Servidor.");
                    }
                }
                return View(cliente);
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Página editar clientes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize]
        public async Task<ActionResult> EditarCliente(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cliente cliente = null;
            //ViewData["EditId"] = id;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(urlApi);
                var resposta = await httpClient.GetAsync("ObterId/" + id);

                if (resposta.IsSuccessStatusCode)
                {
                    var response = await resposta.Content.ReadAsStringAsync();
                    cliente = JsonConvert.DeserializeObject<Cliente>(response);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro no Servidor.");
                }
            }
            return View(cliente);
        }

        /// <summary>
        /// Metodo para editar cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize]
        public async Task<ActionResult> BtnEditarCliente(Cliente cliente, int id)
        {
            try
            {
                if (cliente == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                using (var httpClient = new HttpClient())
                {

                    httpClient.BaseAddress = new Uri(urlApi + "Alterar/" + cliente.Id);

                    var resposta = await httpClient.PutAsJsonAsync("", cliente);
                    if (resposta.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListarClientes");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Erro no Servidor.");
                    }
                }
                return View(cliente);
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Página de detalhes do cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize]
        public async Task<ActionResult> DetalhesCliente(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cliente cliente = null;
            //ViewData["EditId"] = id;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(urlApi + "ObterId/" + id);
                var resposta = await httpClient.GetAsync("");

                if (resposta.IsSuccessStatusCode)
                {
                    var response = await resposta.Content.ReadAsStringAsync();
                    cliente = JsonConvert.DeserializeObject<Cliente>(response);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro no Servidor.");
                }
            }
            return View(cliente);
        }

        /// <summary>
        /// Página para excluir cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize]
        public async Task<ActionResult> ExcluirCliente(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cliente cliente = null;

            using (var httpClient = new HttpClient())
            {

                httpClient.BaseAddress = new Uri(urlApi + "ObterId/" + id);

                //HTTP GET
                var resposta = await httpClient.GetAsync("");
                if (resposta.IsSuccessStatusCode)
                {
                    string response = await resposta.Content.ReadAsStringAsync();
                    cliente = JsonConvert.DeserializeObject<Cliente>(response);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro no Servidor.");
                }
            }
            return View(cliente);
        }

        /// <summary>
        /// Metodo para excluir cliente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BtnExcluirCliente(int id, IFormCollection collection)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cliente cliente = null;

                using (var httpClient = new HttpClient())
                {

                    httpClient.BaseAddress = new Uri(urlApi + "Excluir/" + id);
                    //HTTP DELETE
                    var resposta = await httpClient.DeleteAsync("");

                    if (resposta.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListaClientes");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Erro no Servidor.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
