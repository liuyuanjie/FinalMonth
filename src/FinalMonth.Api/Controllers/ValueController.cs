using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinalMonth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValueController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ValueController> _logger;

        public ValueController(ILogger<ValueController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var isIn = User.IsInRole("admin");
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "kaka")
                };
            var claimsIdentity = new ClaimsIdentity(claims, "Login");
            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("Auth")]
        [Authorize]
        public string Authorize()
        {
            _logger.LogInformation("Name:{username},login: {login}", HttpContext.User.Identity.Name, HttpContext.Items["loginName"]);
            return $"Name:{HttpContext.User.Identity.Name},login: {HttpContext.Items["loginName"]}";
        }

        [HttpGet]
        [Route("admin")]
        [Authorize("admin")]
        public string Admin()
        {
            return $"Name:{HttpContext.User.Identity.Name},claim: {User.Claims.First().Value}";
        }


        [HttpGet]
        [Route("develop")]
        [Authorize(Policy = "develop")]
        public string Develop()
        {
            return $"Name:{HttpContext.User.Identity.Name},claim: {User.Claims.First().Value}";
        }

        [HttpGet]
        [Route("adminrole")]
        [Authorize(Roles = "admin")]
        public string AdminRole()
        {
            return $"Name:{HttpContext.User.Identity.Name},claim: {User.Claims.First().Issuer}-{User.Claims.First().Value}";
        }

        [HttpGet]
        [Route("developrole")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "admin,develop")]
        public string DevelopRole()
        {
            return $"Name:{HttpContext.User.Identity.Name},claim: {User.Claims.First().Issuer}-{User.Claims.First().Value}";
        }

        [HttpGet]
        [Route("testrole")]
        [Authorize(Roles = "admin,develop,test")]
        public string TestRole()
        {
            return $"Name:{HttpContext.User.Identity.Name},claim: {User.Claims.First().Issuer}-{User.Claims.First().Value}";
        }
    }
}
