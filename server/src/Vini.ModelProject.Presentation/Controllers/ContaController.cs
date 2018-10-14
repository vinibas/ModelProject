using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vini.ModelProject.Application.Interfaces;
using Vini.ModelProject.Application.ViewModels;

namespace Vini.ModelProject.Presentation.Controllers
{
    public class ContaController: Controller
    {
        private readonly IContaAppService _contaAppService;

        //private readonly IStringLocalizer<ContaController> _localizer;

        public ContaController(
            IContaAppService contaAppService)
            //IStringLocalizer<ContaController> localizer)
        {
            this._contaAppService = contaAppService;

            //_localizer = localizer;
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

            var erros = await _contaAppService.CadastrarUsuário(vm);

            if (erros.Count == 0)
                return RedirecionarParaHomeIndex();
            else
            {
                foreach (var erro in erros)
                    ModelState.AddModelError(string.Empty, erro);

                return View(vm);
            }
        }

        [Authorize]
        public async Task<IActionResult> Listar()
            => View(await _contaAppService.ListarUsuáriosAsync());

        public ActionResult Login()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View();

            var erros = await _contaAppService.LoginAsync(vm);

            if (erros.Count == 0)
                return RedirecionarParaHomeIndex();
            else
            {
                foreach (var erro in erros)
                    ModelState.AddModelError(string.Empty, erro);

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _contaAppService.LogoutAsync();
            return RedirecionarParaHomeIndex();
        }

        private IActionResult RedirecionarParaHomeIndex()
            => RedirectToAction(nameof(HomeController.Index), "Home");

    }
}
