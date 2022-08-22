using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Estacionamento.Models
{
    [Produces("application/json")]
    public class Empresa
    {
        public Empresa()
        {
        //    //Controleveiculos = new HashSet<Controleveiculo>();
        //    //Id = Guid.NewGuid();
        }

        public int Id { get; set; }
        public string? RazaoSocial { get; set; } 
        public string? Fantasia { get; set; } 
        public string? Logradouro { get; set; }
        public string? Numero { get; set; } 
        public string? Cnpj { get; set; } 
        public string? Bairro { get; set; }
        public string? Cidade { get; set; } 
        public string? Estado { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }

        //public virtual Pagamento Pagamento { get; set; } = null!;
        //public virtual ICollection<Controleveiculo> Controleveiculos { get; set; }
    }
}
