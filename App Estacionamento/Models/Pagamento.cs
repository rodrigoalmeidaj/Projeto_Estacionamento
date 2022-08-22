using System.ComponentModel.DataAnnotations;

namespace App_Estacionamento.Models
{
    public class Pagamento
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        public int IdTicket { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "FORMA DE PAGAMENTO:")]
        public string FormaDePagamento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "VALOR:")]
        public decimal? ValorTotal { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "DESCONTO:")]
        public decimal? Desconto { get; set; }

        public string? ChavePix { get; set; }

        public string? QrCode { get; set; }

        public Pagamento(int idTicket, string formaPagamento, decimal desconto, decimal valorTotal, string chavePix, string? qrCode)
        {
            this.IdTicket = idTicket;
            this.FormaDePagamento = formaPagamento;
            this.Desconto = desconto;
            this.ValorTotal = valorTotal;
            this.ChavePix = chavePix;
            this.QrCode = qrCode;
        }

        public Pagamento(int idTicket, string formaPagamento, decimal desconto, decimal valorTotal, string chavePix)
        {
            this.IdTicket = idTicket;
            this.FormaDePagamento = formaPagamento;
            this.Desconto = desconto;
            this.ValorTotal = valorTotal;
            this.ChavePix = chavePix;
        }

        public Pagamento()
        {

        }
    }
}
