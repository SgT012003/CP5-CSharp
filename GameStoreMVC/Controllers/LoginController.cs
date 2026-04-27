using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
 
namespace GameStoreMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
 
        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
 
        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
 
            return View();
        }
 
        [HttpPost]
        public async Task<IActionResult> Entrar(Usuario loginParams)
        {
            if (string.IsNullOrEmpty(loginParams.Email) || string.IsNullOrEmpty(loginParams.Senha))
            {
                ViewBag.Error = "Email e Senha são obrigatórios.";
                return View("Index");
            }
 
            var usuario = _usuarioRepositorio.ObterPorEmail(loginParams.Email);
 
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginParams.Senha, usuario.SenhaHash))
            {
                ViewBag.Error = "Email ou senha inválidos.";
                return View("Index");
            }
 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("UsuarioId", usuario.Id.ToString())
            };
 
            if (usuario.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
 
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
 
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
 
            return RedirectToAction("Index", "Home");
        }
 
        public IActionResult Cadastrar()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
 
            return View();
        }
 
        [HttpPost]
        public IActionResult Salvar(CadastroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Cadastrar", model);
            }
 
            var existente = _usuarioRepositorio.ObterPorEmail(model.Email);
            if (existente != null)
            {
                ModelState.AddModelError("Email", "Este email já está em uso.");
                return View("Cadastrar", model);
            }
 
            var usuario = new Usuario
            {
                Nome = model.Nome,
                Email = model.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(model.Senha)
            };
 
            _usuarioRepositorio.Adicionar(usuario);
 
            TempData["MensagemSucesso"] = "Cadastro realizado com sucesso! Faça login.";
            return RedirectToAction("Index");
        }
 
        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}