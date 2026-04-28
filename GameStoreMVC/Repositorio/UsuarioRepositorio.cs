using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
 
namespace GameStoreMVC.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly string _connectionString;
 
        public UsuarioRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }
 
        public void Adicionar(Usuario usuario)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
 
            var query = "INSERT INTO Usuarios (Nome, Email, SenhaHash, IsAdmin) VALUES (@Nome, @Email, @SenhaHash, @IsAdmin)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Nome", usuario.Nome);
            command.Parameters.AddWithValue("@Email", usuario.Email);
            command.Parameters.AddWithValue("@SenhaHash", usuario.SenhaHash);
            command.Parameters.AddWithValue("@IsAdmin", usuario.IsAdmin);
 
            command.ExecuteNonQuery();
        }
 
        public Usuario? ObterPorEmail(string email)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
 
            var query = "SELECT Id, Nome, Email, SenhaHash, IsAdmin FROM Usuarios WHERE Email = @Email";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);
 
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Usuario
                {
                    Id = reader.GetInt32("Id"),
                    Nome = reader.GetString("Nome"),
                    Email = reader.GetString("Email"),
                    SenhaHash = reader.GetString("SenhaHash"),
                    IsAdmin = !reader.IsDBNull(reader.GetOrdinal("IsAdmin")) && reader.GetBoolean("IsAdmin")
                };
            }
 
            return null;
        }
    }
}
