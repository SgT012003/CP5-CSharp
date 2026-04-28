namespace GameStoreMVC.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string UrlImagem { get; set; } = string.Empty;
    }
}