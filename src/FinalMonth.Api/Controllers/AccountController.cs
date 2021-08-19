using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FinalMonth.Infrastructure.Data;

namespace FinalMonth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ShinetechUser> _userManager;
        private readonly SignInManager<ShinetechUser> _signInManager;

        public AccountController(UserManager<ShinetechUser> userManager, SignInManager<ShinetechUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] string userName, [FromForm] string email, [FromForm] string password)
        {
            var shinetechUser = new ShinetechUser
            {
                UserName = userName,
                Email = email,
            };
            var result = await _userManager.CreateAsync(shinetechUser, password);
            if (result.Succeeded)
            {
                var claims = new List<Claim>
                {
                    //new Claim(ClaimTypes.Role, "admin"),
                    new Claim(ClaimTypes.Role, "technology"),
                    new Claim(ClaimTypes.Role, "tester"),
                };

                var claimResult = await _userManager.AddClaimsAsync(shinetechUser, claims);
                if (claimResult.Succeeded)
                {
                    return Ok("successed");
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm] string userName, [FromForm] string password)
        {
            var result = await _userManager.FindByNameAsync(userName);
            if (result != null)
            {
                var signInRestult = await _signInManager.PasswordSignInAsync(result, password, false, false);
                if (signInRestult.Succeeded)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }
    }
}
