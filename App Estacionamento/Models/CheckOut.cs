using System.ComponentModel.DataAnnotations;

namespace App_Estacionamento.Models
{
    public class CheckOut
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "CLIENTE:")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "PLACA:")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "VEÍCULO:")]
        public string Veiculo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "DATA/HORA:")]
        [DataType(DataType.DateTime)]
        public DateTime Entrada { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "TIMER:")]
        public DateTime Timer { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "VALOR PRÉVIO:")]
        public decimal ValorPrevio { get; set; }
    }
}
