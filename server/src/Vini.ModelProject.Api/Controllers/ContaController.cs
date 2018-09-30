using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vini.ModelProject.Application.AplicationServices;
using Vini.ModelProject.Application.Interfaces;
using Vini.ModelProject.Application.ViewModels;

namespace Vini.ModelProject.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/conta")]
    public class ContaController : Controller
    {
        private readonly IContaAppService _contaAppService;

        //private readonly IStringLocalizer<ContaApiController> _localizer;

        public ContaController(
            IContaAppService contaAppService
            //UserManager<ApplicationUser> userManager,
            //SignInManager<ApplicationUser> signInManager,
            //UsuárioRepository usuárioRepository,
            //IStringLocalizer<ContaApiController> localizer
            )
        {
            this._contaAppService = contaAppService;
            //_userManager = userManager;
            //_signInManager = signInManager;
            //_usuárioRepository = usuárioRepository;
            //_localizer = localizer;
        }

        [HttpPost("cadastrar-usuario")]
        public async Task<IActionResult> CadastrarUsuário([FromBody]CadastrarUsuárioViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequestModelStateInválida();

            var erros = await _contaAppService.CadastrarUsuário(vm);

            if (erros.Count == 0)
            {
                var nomeUsuário = await _contaAppService.ObterNomeDoUsuárioPorUserNameAsync(vm.Nome);
                return OkUsuárioAutenticado(nomeUsuário);
            }
            else
            {
                foreach (var erro in erros)
                    ModelState.AddModelError(string.Empty, erro);

                return BadRequestModelStateInválida();
            }
        }

        [HttpGet("listar")]
        [Authorize]
        public async Task<IActionResult> Listar()
            => Ok(new
                {
                    success = true,
                    data = await _contaAppService.ListarUsuáriosAsync(),
                });

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequestModelStateInválida();

            var erros = await _contaAppService.LoginAsync(vm);

            if (erros.Count == 0)
            {
                var nomeUsuário = await _contaAppService.ObterNomeDoUsuárioPorUserNameAsync(vm.Nome);
                return OkUsuárioAutenticado(nomeUsuário);
            }
            else
            {
                foreach (var erro in erros)
                    ModelState.AddModelError(string.Empty, erro);

                return BadRequestModelStateInválida();
            }
        }

        private IActionResult OkUsuárioAutenticado(string nomeUsuário)
        {
            return Ok(new
            {
                success = true,
                data = new
                {
                    tokenUsuarioLogado = GerarToken(),
                    nomeUsuarioLogado = nomeUsuário,
                }
            });
        }

        private IActionResult BadRequestModelStateInválida()
        {
            return BadRequest(new
            {
                success = false,
                errors = ModelState.Values.SelectMany(p => p.Errors).Select(p => p.ErrorMessage).ToArray()
            });
        }

        private string GerarToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecuritySecretKey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "teste")
            };

            var token = new JwtSecurityToken(
                issuer: "https://localhost:44367/",
                audience: "http://localhost:4200/",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _contaAppService.LogoutAsync();
            return Ok(new
            {
                success = true
            });
        }
    }
}
