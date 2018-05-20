using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetoSimples.Presentation.Data;
using ProjetoSimples.Presentation.Models;
using System;
using Microsoft.AspNetCore.Http;

namespace ProjetoSimples.Presentation.ViewComponents
{
    public class ContaViewComponent : ViewComponent
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UsuárioRepository _usuárioRepository;

        public ContaViewComponent(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            UsuárioRepository usuárioRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _usuárioRepository = usuárioRepository;
        }

        private string NomeUsuárioLogado
        {
            get
            {
                string nomeUsuárioLogado = null;

                if (_signInManager.IsSignedIn(UserClaimsPrincipal) && 
                    (nomeUsuárioLogado = HttpContext.Session.GetString("NomeUsuárioLogado")) == null)
                {
                    var userId = _userManager.GetUserId(UserClaimsPrincipal);
                    nomeUsuárioLogado = _usuárioRepository.ObterPorIdAsync(new Guid(userId)).Result.Nome;

                    HttpContext.Session.SetString("NomeUsuárioLogado", nomeUsuárioLogado);
                }

                return nomeUsuárioLogado;
            }
        }

        public IViewComponentResult Invoke()
         => View(model:NomeUsuárioLogado);
    }
}
