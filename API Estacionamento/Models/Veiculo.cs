using System.ComponentModel.DataAnnotations.Schema;

namespace API_Estacionamento.Models
{

    public class Veiculo
    {
        public Veiculo()
        {
            //Controleveiculos = new HashSet<Controleveiculo>();
        }

        public int Id { get; set; }
        public string? Placa { get; set; } = null!;
        public string? Descricao { get; set; } = null!;
        public DateTime? DataCadastro { get; set; }
        public string? TipoVeiculo { get; set; } = null!;

        //public virtual ICollection<Controleveiculo> Controleveiculos { get; set; }
    }
}
