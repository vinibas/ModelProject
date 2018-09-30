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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequestModelStateInválida();

            var resultado = await _contaAppService.Login(vm);

            if (resultado.Count == 0)
            {
                var user = await _userManager.FindByNameAsync(vm.Nome);
                var usuário = await _usuárioRepository.ObterPorIdAsync(Guid.Parse(user.Id));

                return OkUsuárioAutenticado(usuário);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Falha: Usuário ou senha incorretos!");

                return BadRequestModelStateInválida();
            }
        }

        private IActionResult OkUsuárioAutenticado(Usuário usuário)
        {
            return Ok(new
            {
                success = true,
                data = new
                {
                    tokenUsuarioLogado = GerarToken(),
                    nomeUsuarioLogado = usuário.Nome
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
            await _signInManager.SignOutAsync();
            return Ok(new
            {
                success = true
            });
        }

        [HttpGet("listar")]
        [Authorize]
        public async Task<IActionResult> Listar()
        {
            var usuários = await _usuárioRepository.ListarTodosAsync();
            var listarVM = usuários.Select(u => new ListarViewModel { Id = u.Id, Nome = u.Nome, CriadoEm = u.CriadoEm });

            return Ok(new
            {
                success = true,
                data = usuários
            });
        }

        [HttpPost("cadastrar-usuario")]
        public async Task<IActionResult> CadastrarUsuário([FromBody]CadastrarUsuárioViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequestModelStateInválida();

            var appUser = new ApplicationUser() { UserName = vm.Nome };

            var resultado = await _userManager.CreateAsync(appUser, vm.Senha);

            if (resultado.Succeeded)
            {
                var usuário = new Usuário { Id = new Guid(appUser.Id), Nome = vm.Nome, CriadoEm = DateTime.Now };

                await _usuárioRepository.AdicionarAsync(usuário);
                await _signInManager.SignInAsync(appUser, false);

                return OkUsuárioAutenticado(usuário);
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

                return BadRequestModelStateInválida();
            }
        }

    }
}
