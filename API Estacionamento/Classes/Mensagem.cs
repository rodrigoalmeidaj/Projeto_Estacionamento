
namespace API_Estacionamento.Classes
{
    public class Mensagem
    {
        public string Descricao { get; set; }
        public string Status { get; set; }

        public Mensagem(string status, string descricao)
        {
            Status = status;
            Descricao = descricao;
        }
        
    }
}
