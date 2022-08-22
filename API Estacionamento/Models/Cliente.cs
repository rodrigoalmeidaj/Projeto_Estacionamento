using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Estacionamento.Models
{
    [Produces("application/json")]
    public class Cliente
    {
        
        public Cliente()
        {
            //Controleveiculos = new HashSet<Controleveiculo>();
            //Id = Guid.NewGuid();
        }

        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public DateTime? Nascimento { get; set; }
        public string? Telefone { get; set; } 
        public string? Logradouro { get; set; }
        public int? Numero { get; set; }
        public string? Veiculo { get; set; }
        public string? Email { get; set; }
        public DateTime? DataCadastro { get; set; }

        //public virtual Pagamento Pagamento { get; set; } = null!;
        //public virtual ICollection<Controleveiculo> Controleveiculos { get; set; }
    }
}
