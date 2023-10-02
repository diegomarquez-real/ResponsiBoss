using ResponsiBoss.Api.Models;
using ResponsiBoss.BlazorServerApp.Identity.Results;

namespace ResponsiBoss.BlazorServerApp.Identity.Abstractions
{
    public interface IApplicationSignInManager
    {
        Task SignInAsync(AuthTokenModel authTokenModel);
        Task<AuthenticationResult> SignInResultAsync(string email, string password);
    }
}