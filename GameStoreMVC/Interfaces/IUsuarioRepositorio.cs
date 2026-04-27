using GameStoreMVC.Models;
 
namespace GameStoreMVC.Interfaces
{
    public interface IUsuarioRepositorio
    {
        void Adicionar(Usuario usuario);
        Usuario? ObterPorEmail(string email);
    }
}
 