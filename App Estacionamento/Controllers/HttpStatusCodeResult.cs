using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App_Estacionamento.Controllers
{
    internal class HttpStatusCodeResult : ActionResult
    {
        private HttpStatusCode badRequest;

        public HttpStatusCodeResult(HttpStatusCode badRequest)
        {
            this.badRequest = badRequest;
        }
    }
}