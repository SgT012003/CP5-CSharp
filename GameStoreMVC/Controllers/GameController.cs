using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 
namespace GameStoreMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GameController : Controller
    {
        private readonly IGameRepositorio _gameRepositorio;
 
        public GameController(IGameRepositorio gameRepositorio)
        {
            _gameRepositorio = gameRepositorio;
        }
 
        public IActionResult Index()
        {
            var games = _gameRepositorio.ObterTodos();
            return View(games);
        }
 
        public IActionResult Create()
        {
            return View();
        }
 
        [HttpPost]
        public IActionResult Create(Game game)
        {
            if (ModelState.IsValid)
            {
                _gameRepositorio.Adicionar(game);
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }
 
        public IActionResult Edit(int id)
        {
            var game = _gameRepositorio.ObterPorId(id);
            if (game == null) return NotFound();
           
            return View(game);
        }
 
        [HttpPost]
        public IActionResult Edit(Game game)
        {
            if (ModelState.IsValid)
            {
                _gameRepositorio.Atualizar(game);
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }
 
        public IActionResult Delete(int id)
        {
            var game = _gameRepositorio.ObterPorId(id);
            if (game == null) return NotFound();
 
            return View(game);
        }
 
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _gameRepositorio.Remover(id);
            return RedirectToAction(nameof(Index));
        }
    }
}