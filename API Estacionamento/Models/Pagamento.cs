using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Estacionamento.Models
{
    [Produces("application/json")]
    public class Pagamento
    {

        public int Id { get; set; }
        public int IdTicket { get; set; }
        public string? FormaDePagamento { get; set; }
        public decimal Desconto { get; set; }
        public decimal? ValorTotal { get; set; }              
        public string? ChavePix { get; set; }
        public string? QrCode { get; set; }        

        //public virtual Cliente IdclienteNavigation { get; set; } = null!;
        //public virtual Empresa IdempresaNavigation { get; set; } = null!;
        //public virtual Controleveiculo IdticketNavigation { get; set; } = null!;
    }
}
