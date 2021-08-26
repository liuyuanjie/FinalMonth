using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinalMonth.Api.Command;
using FinalMonth.Api.Common;
using Microsoft.AspNetCore.Identity;
using FinalMonth.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FinalMonth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ShinetechUser> _userManager;
        private readonly SignInManager<ShinetechUser> _signInManager;

        public AccountController(IMediator mediator, UserManager<ShinetechUser> userManager, SignInManager<ShinetechUser> signInManager)
        {
            _mediator = mediator;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterCommand registerCommand)
        {
            var shinetechUser = new ShinetechUser
            {
                UserName = registerCommand.UserName,
                Email = registerCommand.Email,
            };
            var result = await _userManager.CreateAsync(shinetechUser, registerCommand.Password);
            if (result.Succeeded)
            {
                var claims = new List<Claim>() { new Claim(ClaimTypes.System, "test") };
                var roles = new List<string> { "test" };
                if (registerCommand.IsAdmin)
                {
                    roles.Add("admin");
                    claims.Add(new Claim(ClaimTypes.System, "admin"));
                }

                var claimResult = await _userManager.AddClaimsAsync(shinetechUser, claims);
                var roleResult = await _userManager.AddToRolesAsync(shinetechUser, roles);
                if (claimResult.Succeeded && roleResult.Succeeded)
                {
                    return Ok("successed");
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var user = await _userManager.FindByNameAsync(loginCommand.UserName);
            if (user != null)
            {
                var valid = await _userManager.CheckPasswordAsync(user, loginCommand.Password);
                if (valid)
                {
                    var roleResult = await _userManager.GetRolesAsync(user);
                    var claims = new List<Claim>()
                    {
                        new(ClaimTypes.Name,user.UserName),
                        new(ClaimTypes.Email,user.Email),
                    };
                    foreach (var role in roleResult)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("Loginnoclaim")]
        public async Task<IActionResult> LoginNoClaim(LoginCommand loginCommand)
        {
            var user = await _userManager.FindByNameAsync(loginCommand.UserName);
            if (user != null)
            {
                var valid = await _userManager.CheckPasswordAsync(user, loginCommand.Password);
                if (valid)
                {
                    var claims = await _userManager.GetClaimsAsync(user);
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("IdentityLogin")]
        public async Task<IActionResult> IdentityLogin(LoginCommand loginCommand)
        {
            var result = await _userManager.FindByNameAsync(loginCommand.UserName);
            if (result != null)
            {
                var signIn = await _signInManager.PasswordSignInAsync(result, loginCommand.Password, false, false);
                if (signIn.Succeeded)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("JwtLogin")]
        public async Task<IActionResult> JwtLogin(LoginCommand loginCommand)
        {
            var command = await _mediator.Send(loginCommand);
            if (command.Any())
            {
                return Ok(JwtTokenGenerator.Generator(command));
            }
            return BadRequest();
        }
    }
}
