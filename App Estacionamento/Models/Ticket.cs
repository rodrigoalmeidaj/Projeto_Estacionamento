using System.ComponentModel.DataAnnotations;

namespace App_Estacionamento.Models
{
    public class Ticket
    {
        public Ticket()
        {
            //Id = Guid.NewGuid();
        }

        [Display(Name = "ID")]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "CLIENTE")]
        public string? Cliente { get; set; }

        //[Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "PLACA")]
        public string? Placa { get; set; }

        //[Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "VEÍCULO")]
        public string? Veiculo { get; set; }

        //[Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "VALOR PRÉVIO")]
        public decimal? Valor { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? Desconto { get; set; }

        //[Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.DateTime)]
        [Display(Name = "ENTRADA")]
        public DateTime? Entrada { get; set; }
        public DateTime? Saida { get; set; }

        //[Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "TEMPO")]
        public DateTime Tempo { get; set; }

        [Display(Name = "FORMA PAGAMENTO")]
        public int IdFormaPagamento { get; set; }
        public string FormaPagamento { get; set; }
        public string? ObservacaoTicket { get; set; }
        public bool? Pago { get; set; }
        public string? ChavePix { get; set; }

    }
}
