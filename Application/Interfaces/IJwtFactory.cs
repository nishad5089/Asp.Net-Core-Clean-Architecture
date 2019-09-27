using System.Security.Claims;
using System.Threading.Tasks;
using Application.Viewmodels;
using Domain.Entities.Auth;

namespace Application.Interfaces {
    public interface IJwtFactory {
        Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync (ApplicationUser user);
        ClaimsPrincipal GetPrincipalFromToken (string token);
    }
}