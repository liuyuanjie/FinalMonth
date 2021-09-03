using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FinalMonth.Api.Identity.AuthenticationSchemes
{
    public static class ShinetechAuthenticationDefaults
    {
        public const string AuthenticationScheme = "Shinetech";
    }

    public class ShinetechAuthOptions : AuthenticationSchemeOptions
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
    }

    public class ShinetechAuthenticationHandler : AuthenticationHandler<ShinetechAuthOptions>
    {
        public ShinetechAuthenticationHandler(
            IOptionsMonitor<ShinetechAuthOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
            )
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Unauthorized");

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.NoResult();
            }

            if (!authorizationHeader.StartsWith(ShinetechAuthenticationDefaults.AuthenticationScheme, StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string token = authorizationHeader.Substring(ShinetechAuthenticationDefaults.AuthenticationScheme.Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            try
            {
                return await ValidateToken(token);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }

        private async Task<AuthenticateResult> ValidateToken(string token)
        {
            var issuer = Options.JwtIssuer;
            var key = Options.JwtKey;

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(jwtSecurityToken.Claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
