using System.ComponentModel.DataAnnotations;

namespace App_Estacionamento.Models
{
    public class Empresa
    {
        public Empresa()
        {
            //    //Controleveiculos = new HashSet<Controleveiculo>();
            //    //Id = Guid.NewGuid();
        }

        [Display(Name = "ID")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string? RazaoSocial { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Fantasia { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Logradouro { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Numero { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Cnpj { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Bairro { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Cidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Email { get; set; }

    }
}
