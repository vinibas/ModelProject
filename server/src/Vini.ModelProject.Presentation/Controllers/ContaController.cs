using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vini.ModelProject.Presentation.Controllers
{
    public class ContaController: Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UsuárioRepository _usuárioRepository;
        private readonly IStringLocalizer<ContaController> _localizer;

        public ContaController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            UsuárioRepository usuárioRepository,
            IStringLocalizer<ContaController> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _usuárioRepository = usuárioRepository;
            _localizer = localizer;
        }

        [Route("CadastrarUsuario")]
        public ActionResult CadastrarUsuário()
            => View();

        [Route("CadastrarUsuario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastrarUsuário(CadastrarUsuárioViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var appUser = new ApplicationUser() { UserName = vm.Nome };

            var resultado = await _userManager.CreateAsync(appUser, vm.Senha);

            if (resultado.Succeeded)
            {
                var usuário = new Usuário { Id = new Guid(appUser.Id), Nome = vm.Nome, CriadoEm = DateTime.Now };

                await _usuárioRepository.AdicionarAsync(usuário);
                await _signInManager.SignInAsync(appUser, false);

                return RedirecionarParaHomeIndex();
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    var descrição = error.Description;

                    if (error.Code == "DuplicateUserName")
                        descrição = _localizer["O nome de usuário escolhido não está disponível. Escolha outro."];

                    ModelState.AddModelError(string.Empty, descrição);

                }

                return View(vm);
            }
        }

        [Authorize]
        public async Task<IActionResult> Listar()
        {
            var usuários = await _usuárioRepository.ListarTodosAsync();
            var listarVM = usuários.Select(u => new ListarViewModel { Id = u.Id, Nome = u.Nome, CriadoEm = u.CriadoEm });

            return View(listarVM);
        }

        public ActionResult Login()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View();

            var resultado = await _signInManager.PasswordSignInAsync(vm.Nome, vm.Senha, false, false);

            if (resultado.Succeeded)
                return RedirecionarParaHomeIndex();
            else
            {
                ModelState.AddModelError(string.Empty, "Falha: Usuário ou senha incorretos!");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirecionarParaHomeIndex();
        }

        private IActionResult RedirecionarParaHomeIndex()
            => RedirectToAction(nameof(HomeController.Index), "Home");

    }
}
