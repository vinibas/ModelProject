using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using Vini.ModelProject.Application.Interfaces;
using System.Threading.Tasks;

namespace Vini.ModelProject.Presentation.ViewComponents
{
    public class ContaViewComponent : ViewComponent
    {
        public readonly IContaAppService _contaAppService;

        public ContaViewComponent(IContaAppService contaAppService)
            => _contaAppService = contaAppService;

        private string NomeUsuárioLogado
        {
            get
            {
                string nomeUsuárioLogado = null;

                if (_contaAppService.UsuárioEstáLogado(UserClaimsPrincipal) &&
                    (nomeUsuárioLogado = HttpContext.Session.GetString("NomeUsuárioLogado")) == null)
                {
                    nomeUsuárioLogado = _contaAppService.ObterNomeDoUsuárioLogadoAsync(UserClaimsPrincipal).Result;
                    HttpContext.Session.SetString("NomeUsuárioLogado", nomeUsuárioLogado);
                }

                return nomeUsuárioLogado;
            }
        }

        public IViewComponentResult Invoke()
            => View(model:NomeUsuárioLogado);
    }
}
