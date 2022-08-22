using App_Estacionamento.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using App_Estacionamento.Services;
using Microsoft.AspNetCore.Authorization;
using Gerencianet.NETCore.SDK;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System;
using System.IO;
#nullable disable

namespace App_Estacionamento.Controllers
{
    //[Authorize]
    public class TicketController : Controller
    {
        // URL da api Rodrigo
        const string urlApi = "https://localhost:7210/api/Ticket/";
        const string urlApiPagamento = "https://localhost:7210/api/Pagamento/";
        // URL da api Carlos
        //const string urlApi = "https://localhost:5007/api/Ticket/";
        // ObterTodos
        // ObterTodosNaoPagos
        // ObterId/{id}
        // Incluir
        // Alterar/{id}
        // Excluir/{id}

        /// <summary>
        /// Página de Controle de Tickets
        /// O usuário terá o controle dos veículos que ocupam as vagas do estacionamento nesta página.
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        public async Task<ActionResult> Abertos()
        {
            IEnumerable<Ticket> ticket = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(urlApi + "ObterTodosNaoPagos");

                var resposta = await httpClient.GetAsync("");
                if (resposta.IsSuccessStatusCode)
                {
                    var lerTask = resposta.Content.ReadAsAsync<IList<Ticket>>();
                    lerTask.Wait();
                    ticket = lerTask.Result;
                }
                else
                {
                    ticket = Enumerable.Empty<Ticket>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor.");
                }
                return View(ticket);
            }
        }
        /// <summary>
        /// Pagina de Entrada
        /// O usuário fará a entrada dos veículos nesta página.
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        public ActionResult Entrada()
        {
            Ticket ticket = new Ticket();
            var data = DateTime.Now;
            ticket.Entrada = data;

            return View(ticket);
        }

        /// <summary>
        /// Botão de Check-In
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        //[Authorize]
        public async Task<ActionResult> BtnCheckIn(Ticket ticket)
        {
            try
            {
                if (ticket == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                using (var httpClient = new HttpClient())
                {
                    ticket.Pago = false;
                    httpClient.BaseAddress = new Uri(urlApi + "Incluir");
                    var resposta = await httpClient.PostAsJsonAsync<Ticket>("", ticket);
                    if (resposta.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Abertos");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Erro no Servidor.");
                    }
                }
                return RedirectToAction("Abertos");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Pagina de Saída
        /// O usuário fará a saída dos veículos nesta página.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize]
        public async Task<ActionResult> Saida(int? id)
        {
            if (id == null)
            {
                var service = new TicketService();
                // Busca a lista de produtos e tickets na api
                List<Ticket> tickets = await service.ListarTickets(urlApi + "ObterTodosNaoPagos");

                // Passa para a View um SelectListItem com a lista de tickets
                ViewBag.Tickets = service.SelectListTicket(tickets);

                Ticket ticketS = new Ticket();
                ticketS.Saida = DateTime.Now;
                return View(ticketS);
            }
            
            Ticket ticket = new Ticket();
            ticket.Pago = false;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(urlApi + "ObterId/" + id);

                var resposta = await httpClient.GetAsync("");
                if (resposta.IsSuccessStatusCode)
                {
                    var response = await resposta.Content.ReadAsStringAsync();
                    ticket = JsonConvert.DeserializeObject<Ticket>(response);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro no servidor.");
                }

                // FATORAR
                // kkkkkkkkkkkk diferencia dos tempos. ticket.Tempo = ticket.Saida - ticket.Entrada
                ticket.Saida = DateTime.Now;
                var diferencaTempo = ticket.Saida - ticket.Entrada;
                var tempo = diferencaTempo.ToString();
                ticket.Tempo = DateTime.Parse(tempo);

                // FATORAR
                ticket.Valor = 2.50m;
                var horas = ticket.Tempo.Hour + 1;
                var minutos = ticket.Tempo.Minute;
                ticket.ValorTotal = ticket.Valor * horas;

                return View(ticket);
            }
        }

        /// <summary>
        /// Botão Check-Out
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        //[Authorize]
        public async Task<ActionResult> BtnCheckOut(Ticket ticket)
        {
            try
            {
                if (ticket == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                using (var httpClient = new HttpClient())
                {
                    ticket.Pago = false;
                    
                    httpClient.BaseAddress = new Uri(urlApi + "Alterar/" + ticket.Id);

                    var resposta = await httpClient.PutAsJsonAsync("", ticket);
                    if (resposta.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Pagamento", ticket);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Erro no Servidor.");
                    }
                }
                return RedirectToAction("Abertos");
            }
            catch
            {
                return View();
            }
        }


        /// <summary>
        /// Página de Pagamento, onde o usuário concluirá o pagamento das vagas
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize]
        public async Task<ActionResult> Pagamento(int? id)
        {
            if(id == null)
            {
                var service = new TicketService();
                // Busca a lista de produtos e clientes na api
                List<Ticket> tickets = await service.ListarTickets(urlApi);
                
                // Passa para a View um SelectListItem com a lista de produtos e clientes
                ViewBag.Tickets = service.SelectListTicket(tickets);

                return View();
            }

            Ticket ticket = new Ticket();
            ticket.Pago = false;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(urlApi + "ObterId/" + id);

                var resposta = await httpClient.GetAsync("");
                if (resposta.IsSuccessStatusCode)
                {
                    var response = await resposta.Content.ReadAsStringAsync();
                    ticket = JsonConvert.DeserializeObject<Ticket>(response);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro no servidor.");
                }
                return View(ticket);
            }
        }


        /// <summary>
        /// Botão Salvar Pagamento
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        //[Authorize]
        public async Task<ActionResult> SalvarPagamento(Ticket ticket)
        {
            try
            {
                if (ticket == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                // Altera o campo pago da tabela ticket para true
                using (var httpClient = new HttpClient())
                {
                    ticket.Pago = true;
                    httpClient.BaseAddress = new Uri(urlApi + "Alterar/" + ticket.Id);
                    var resposta = await httpClient.PutAsJsonAsync("", ticket);
                    if (!resposta.IsSuccessStatusCode)
                        ModelState.AddModelError(string.Empty, "Erro no Servidor.");
                }

                if(ticket.FormaPagamento.Equals("PIX"))
                    CriarCobranca(ticket.ValorTotal, ticket.Cliente, ticket.Id.ToString());

                // Insere na tabela pagamento
                using (var httpClient = new HttpClient())
                {
                    Pagamento pagamento = new();
                    pagamento.IdTicket = ticket.Id;
                    pagamento.FormaDePagamento = ticket.FormaPagamento;
                    pagamento.ValorTotal = ticket.ValorTotal;
                    pagamento.Desconto = ticket.Desconto;
                    pagamento.ChavePix = ticket.ChavePix;                    
                    
                    httpClient.BaseAddress = new Uri(urlApiPagamento + "Incluir");
                    var resposta = await httpClient.PostAsJsonAsync<Pagamento>("", pagamento);
                    if (!resposta.IsSuccessStatusCode)
                        ModelState.AddModelError(string.Empty, "Erro no Servidor.");
                    

                }

                return RedirectToAction("Abertos");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Detalhes(int id)
        {
            Ticket ticket = new Ticket();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(urlApi + "ObterId/" + id);

                var resposta = await httpClient.GetAsync("");
                if (resposta.IsSuccessStatusCode)
                {
                    var response = await resposta.Content.ReadAsStringAsync();
                    ticket = JsonConvert.DeserializeObject<Ticket>(response);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro no servidor.");
                }
                return View(ticket);
            }
        }

        /// <summary>
        /// Metodo para criar a cobrança PIX
        /// </summary>
        /// <param name="valor">Valor cobrado</param>
        /// <param name="nomeCliente">Nome cliente para identificação</param>
        /// <param name="idTicket">ID ticket</param>
        // ALTERAR PARA RETORNAR O ID DA COBRANÇA E TXTID
        public void CriarCobranca(decimal? valor, string nomeCliente, string idTicket)
        {
            // Rota homologação https://api-pix-h.gerencianet.com.br
            string clientId = "Client_Id_333384806dda9b2a2e9cadc16dad085ba75814bf";
            string clientSecret = "Client_Secret_44504776cf34be67926efc87089f6d055554b76e";
            string certificado = "homologacao-397788-certificado-hom.p12";
            var diretorioAppEstacionamento = Directory.GetCurrentDirectory();
            var caminhoCertificado = Path.Combine(diretorioAppEstacionamento, "Certificado", certificado);

            //dynamic endpoints = new Endpoints(JObject.Parse(File.ReadAllText("credentials.json")));
            dynamic endpoints = new Endpoints(clientId, clientSecret, true, caminhoCertificado);

            var body = new
            {
                calendario = new
                {
                    expiracao = 3600
                },
                devedor = new
                {
                    nome = nomeCliente
                },
                valor = new
                {
                    original = valor
                },
                chave = "34992112115",
                solicitacaoPagador = String.Concat("Referente ao ticket: ", idTicket)
            };

            try
            {
                var response = endpoints.PixCreateImmediateCharge(null, body);
                Console.WriteLine(response);
            }
            catch (GnException e)
            {
                Console.WriteLine(e.ErrorType);
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Método para gerar o QrCode
        /// </summary>
        /// <param name="idCobranca">ID cobrança</param>
        /// <returns>Imagem QrCode.jpg</returns>
        public async Task<ActionResult> GerarQrCode(int idCobranca)
        {
            // Rota homologação https://api-pix-h.gerencianet.com.br
            string clientId = "Client_Id_333384806dda9b2a2e9cadc16dad085ba75814bf";
            string clientSecret = "Client_Secret_44504776cf34be67926efc87089f6d055554b76e";
            string certificado = "homologacao-397788-certificado-hom.p12";
            var diretorioAppEstacionamento = Directory.GetCurrentDirectory();
            var caminhoCertificado = Path.Combine(diretorioAppEstacionamento, "Certificado", certificado);

            dynamic endpoints = new Endpoints(clientId, clientSecret, true, caminhoCertificado);

            var param = new
            {
                id = idCobranca
            }; 
            
            try
            {
                var response = endpoints.PixGenerateQRCode(param);
                Console.WriteLine(response);

                // Generate QRCode Image to JPEG Format
                JObject jsonResponse = JObject.Parse(response);
                string img = (string)jsonResponse["imagemQrcode"];
                img = img.Replace("data:image/png;base64,", "");

                var fromBytes = Convert.FromBase64String(img);
                var qrCodeImage = Image.FromStream(new MemoryStream(fromBytes));

                var caminhoQrCode = Path.GetDirectoryName("./QRCode/");
                qrCodeImage.Save(Path.Combine(Directory.GetCurrentDirectory(), "QRCode", "QRCodeImage.jpg"));
                Console.WriteLine(response);

                return File(fromBytes, "QRCodeImage/jpg");
            }
            catch (GnException e)
            {
                Console.WriteLine(e.ErrorType);
                Console.WriteLine(e.Message);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txId"></param>
        /// <returns></returns>
        public async Task<ActionResult> ConferirCobranca(string txId)
        {
            // Rota homologação https://api-pix-h.gerencianet.com.br
            string clientId = "Client_Id_333384806dda9b2a2e9cadc16dad085ba75814bf";
            string clientSecret = "Client_Secret_44504776cf34be67926efc87089f6d055554b76e";
            string certificado = "homologacao-397788-certificado-hom.p12";

            var diretorioAppEstacionamento = Directory.GetCurrentDirectory();
            var caminhoCertificado = Path.Combine(diretorioAppEstacionamento, "Certificado", certificado);
            dynamic endpoints = new Endpoints(clientId, clientSecret, true, caminhoCertificado);

            var param = new
            {
                txid = txId
            };

            try
            {
                var response = endpoints.PixDetailCharge(param);
                Console.WriteLine(response);
            }
            catch (GnException e)
            {
                Console.WriteLine(e.ErrorType);
                Console.WriteLine(e.Message);
            }
            return View();
        }


        [HttpGet]
        public IActionResult AccessDenied(int? id)
        {
            return View();
        }
    }
}
