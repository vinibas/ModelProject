using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetoSimples.Presentation.Data;
using ProjetoSimples.Presentation.Models;
using System;
using System.Threading.Tasks;

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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_signInManager.IsSignedIn(UserClaimsPrincipal))
            {
                var userId = _userManager.GetUserId(UserClaimsPrincipal);
                var usuário = await _usuárioRepository.ObterPorIdAsync(new Guid(userId));

                return View(usuário);
            }
            else
                return View(new Usuário());
        }
    }
}
