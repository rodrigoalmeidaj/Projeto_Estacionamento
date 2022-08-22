using System.ComponentModel.DataAnnotations;

namespace App_Estacionamento.Models
{
    public class CheckIn
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
        [DataType(DataType.DateTime)]
        [Display(Name = "DATA/HORA:")]
        public DateTime Entrada { get; set; }
    }
}
