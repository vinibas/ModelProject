using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Vini.ModelProject.Infra.CrossCutting.Identity.Models;

namespace Vini.ModelProject.Infra.CrossCutting.Identity.Services
{
    public interface IContaIdentityService
    {
        Task<IList<string>> CreateAsync(UsuárioIdentity usuário, string senha);
        Task SignInAsync(UsuárioIdentity usuário);
        Task<IList<string>> PasswordSignInAsync(string nomeUsuário, string senha);
        Task SignOutAsync();
        Task<UsuárioIdentity> FindByNameAsync(string nomeUsuário);
        bool IsUserAuthenticated(ClaimsPrincipal userClaimsPrincipal);
        string GetUserId(ClaimsPrincipal userClaimsPrincipal);
    }
}