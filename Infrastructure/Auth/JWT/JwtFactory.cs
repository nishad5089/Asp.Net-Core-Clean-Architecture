using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Application.Viewmodels;
using Infrastructure.Identity;
using Infrastructure.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth.JWT {
    public class JwtFactory : IJwtFactory {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public JwtFactory (UserManager<ApplicationUser> userManager, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters, ApplicationDbContext context, RoleManager<IdentityRole> roleManager) {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync (ApplicationUser user) {
            var tokenHandler = new JwtSecurityTokenHandler ();
            var key = Encoding.ASCII.GetBytes (_jwtSettings.Secret);
            var claims = new List<Claim> {
                new Claim (JwtRegisteredClaimNames.Sub, user.Email),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ()),
                new Claim (JwtRegisteredClaimNames.Iat, ToUnixEpochDate (DateTime.UtcNow).ToString (), ClaimValueTypes.Integer64),
                new Claim (JwtRegisteredClaimNames.Email, user.Email),
                new Claim ("id", user.Id)
            };
            var userClaims = await _userManager.GetClaimsAsync (user);
            claims.AddRange (userClaims);
            var userRoles = await _userManager.GetRolesAsync (user);
            foreach (var userRole in userRoles) {
                claims.Add (new Claim (ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync (userRole);
                if (role == null) continue;
                var roleClaims = await _roleManager.GetClaimsAsync (role);

                foreach (var roleClaim in roleClaims) {
                    if (claims.Contains (roleClaim))
                        continue;

                    claims.Add (roleClaim);
                }
            }
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.UtcNow.Add (_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken (tokenDescriptor);
            var refreshToken = new RefreshToken {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths (6)
            };

            await _context.RefreshTokens.AddAsync (refreshToken);
            await _context.SaveChangesAsync ();

            return new AuthenticationResult {
                Success = true,
                    Token = tokenHandler.WriteToken (token),
                    ExpiresIn = tokenDescriptor.Expires.Value.Second,
                    RefreshToken = refreshToken.Token
            };
        }
        // private ClaimsPrincipal GetPrincipalFromToken (string token) {
        //     var tokenHandler = new JwtSecurityTokenHandler ();

        //     try {
        //         var principal = tokenHandler.ValidateToken (token, _tokenValidationParameters, out var validatedToken);
        //         if (!IsJwtWithValidSecurityAlgorithm (validatedToken)) {
        //             return null;
        //         }

        //         return principal;
        //     } catch {
        //         return null;
        //     }
        // }
        private bool IsJwtWithValidSecurityAlgorithm (SecurityToken validatedToken) {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals (SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);
        }

        private static long ToUnixEpochDate (DateTime date) => (long) Math.Round ((date.ToUniversalTime () -
                new DateTimeOffset (1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);

        public ClaimsPrincipal GetPrincipalFromToken (string token) {
            var tokenHandler = new JwtSecurityTokenHandler ();

            try {
                var principal = tokenHandler.ValidateToken (token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm (validatedToken)) {
                    return null;
                }

                return principal;
            } catch {
                return null;
            }
        }
    }
}