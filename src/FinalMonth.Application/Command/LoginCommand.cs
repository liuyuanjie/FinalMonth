using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinalMonth.Application.Command
{
    public class LoginCommand : IRequest<IList<Claim>>
    {
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, IList<Claim>>
    {
        private readonly UserManager<ShinetechUser> _userManager;

        public LoginCommandHandler(UserManager<ShinetechUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IList<Claim>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                var valid = await _userManager.CheckPasswordAsync(user, request.Password);
                if (valid)
                {
                    var newRefreshToken = await _userManager.GenerateUserTokenAsync(user, "MyApp", "RefreshToken");
                    await _userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", newRefreshToken);
                    await _userManager.UpdateSecurityStampAsync(user);

                    var roleResult = await _userManager.GetRolesAsync(user);
                    var claims = new List<Claim>()
                    {
                        new(ClaimTypes.Name,user.UserName),
                        new(ClaimTypes.Email,user.Email),
                        new(ClaimTypes.Sid,user.Id),
                    };
                    foreach (var role in roleResult)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    return claims;
                }
            }

            return new List<Claim>();
        }
    }
}
