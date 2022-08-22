using System.ComponentModel.DataAnnotations;

namespace App_Estacionamento.Models
{
    public class Cliente
    {
        public Cliente()
        {
            //Controleveiculos = new HashSet<Controleveiculo>();
            //Id = Guid.NewGuid();
        }

        [Display(Name = "ID")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Id { get; set; }

        [Display(Name = "NOME")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Nome { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime? Nascimento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Logradouro { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int? Numero { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Veiculo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime? DataCadastro { get; set; }
    }
}
