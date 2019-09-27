using System.Security.Claims;
using System.Threading.Tasks;
using Application.Viewmodels;
using Infrastructure.Identity.Entity;

namespace Infrastructure.Auth.JWT {
    public interface IJwtFactory {
        Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync (ApplicationUser user);
        ClaimsPrincipal GetPrincipalFromToken (string token);
    }
}