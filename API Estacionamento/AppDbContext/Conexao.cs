using Dapper;
using Npgsql;
using System.Diagnostics;

namespace API_Estacionamento.AppDbContext
{
    public class Conexao
    {
        public int Id { get; set; }

        public string StringDeConexao()
        {
            try
            {
                NpgsqlConnectionStringBuilder stringBuilder = new NpgsqlConnectionStringBuilder
                {
                    Host = "ec2-3-225-213-67.compute-1.amazonaws.com",
                    Port = 5432,
                    Username = "pkymfjydbwtnkm",
                    Password = "d1faa6a2939abf7912f5a841be15fa6bc1e4d4ea59f2b4ede5fd1a6f642afa07",
                    Database = "dem2alvbh48icp"
                };
                return stringBuilder.ToString();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return "erro";
            }
        }

        public int IdIncrementado(string tabela)
        {
            using (var conexao = new NpgsqlConnection(StringDeConexao()))
            {
                int idBanco = conexao.ExecuteScalar<int>(@$"SELECT Max(""Id"") AS Maior From public.""{tabela}""");
                return idBanco + 1;
            }                
        }
    }
}
