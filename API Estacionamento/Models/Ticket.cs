using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Estacionamento.Models
{
    [Produces("application/json")]
    public class Ticket
    {
        public Ticket()
        {
            //Id = Guid.NewGuid();
        }

        public int? Id { get; set; }
        public string? Cliente { get; set; }
        public string? Placa { get; set; }
        public string? Veiculo { get; set; }
        public decimal? Valor { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? Desconto { get; set; }
        public DateTime? Entrada { get; set; }
        public DateTime? Saida { get; set; }
        public DateTime? Tempo { get; set; }
        public int? FormaPagamento { get; set; }
        public string? ObservacaoTicket { get; set; }
        public bool? Pago { get; set; }
    }
}
