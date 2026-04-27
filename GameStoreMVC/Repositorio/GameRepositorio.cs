using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
 
namespace GameStoreMVC.Repositorio
{
    public class GameRepositorio : IGameRepositorio
    {
        private readonly string _connectionString;
 
        public GameRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }
 
        public IEnumerable<Game> ObterTodos()
        {
            var games = new List<Game>();
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
 
            var query = "SELECT Id, Titulo, Descricao, Preco, UrlImagem FROM Games";
            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();
 
            while (reader.Read())
            {
                games.Add(new Game
                {
                    Id = reader.GetInt32("Id"),
                    Titulo = reader.GetString("Titulo"),
                    Descricao = reader.IsDBNull(reader.GetOrdinal("Descricao")) ? string.Empty : reader.GetString("Descricao"),
                    Preco = reader.GetDecimal("Preco"),
                    UrlImagem = reader.IsDBNull(reader.GetOrdinal("UrlImagem")) ? string.Empty : reader.GetString("UrlImagem")
                });
            }
 
            return games;
        }
 
        public Game? ObterPorId(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
 
            var query = "SELECT Id, Titulo, Descricao, Preco, UrlImagem FROM Games WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
 
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Game
                {
                    Id = reader.GetInt32("Id"),
                    Titulo = reader.GetString("Titulo"),
                    Descricao = reader.IsDBNull(reader.GetOrdinal("Descricao")) ? string.Empty : reader.GetString("Descricao"),
                    Preco = reader.GetDecimal("Preco"),
                    UrlImagem = reader.IsDBNull(reader.GetOrdinal("UrlImagem")) ? string.Empty : reader.GetString("UrlImagem")
                };
            }
 
            return null;
        }
 
        public void Adicionar(Game game)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
 
            var query = "INSERT INTO Games (Titulo, Descricao, Preco, UrlImagem) VALUES (@Titulo, @Descricao, @Preco, @UrlImagem)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Titulo", game.Titulo);
            command.Parameters.AddWithValue("@Descricao", string.IsNullOrEmpty(game.Descricao) ? (object)DBNull.Value : game.Descricao);
            command.Parameters.AddWithValue("@Preco", game.Preco);
            command.Parameters.AddWithValue("@UrlImagem", string.IsNullOrEmpty(game.UrlImagem) ? (object)DBNull.Value : game.UrlImagem);
 
            command.ExecuteNonQuery();
        }
 
        public void Atualizar(Game game)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
 
            var query = "UPDATE Games SET Titulo = @Titulo, Descricao = @Descricao, Preco = @Preco, UrlImagem = @UrlImagem WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", game.Id);
            command.Parameters.AddWithValue("@Titulo", game.Titulo);
            command.Parameters.AddWithValue("@Descricao", string.IsNullOrEmpty(game.Descricao) ? (object)DBNull.Value : game.Descricao);
            command.Parameters.AddWithValue("@Preco", game.Preco);
            command.Parameters.AddWithValue("@UrlImagem", string.IsNullOrEmpty(game.UrlImagem) ? (object)DBNull.Value : game.UrlImagem);
 
            command.ExecuteNonQuery();
        }
 
        public void Remover(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
 
            var query = "DELETE FROM Games WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
 
            command.ExecuteNonQuery();
        }
    }
}