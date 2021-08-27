using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FinalMonth.Api.Services
{
    public interface IIdentityService
    {
        public string UserId { get; }

        public string FullName { get; }

        public string Email { get; }
    }

    public class IdentityService : IIdentityService
    {
        private readonly HttpContextAccessor _httpContextAccessor;

        public IdentityService(HttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string UserId => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
        public string FullName => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        public string Email => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
    }
}
