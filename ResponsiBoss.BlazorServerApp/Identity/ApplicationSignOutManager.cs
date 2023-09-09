using Microsoft.AspNetCore.Authentication;
using ResponsiBoss.BlazorServerApp.Identity.Abstractions;

namespace ResponsiBoss.BlazorServerApp.Identity
{
    public class ApplicationSignOutManager : IApplicationSignOutManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationSignOutManager(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Task SignOutAsync()
        {
            return _httpContextAccessor.HttpContext.SignOutAsync();
        }
    }
}