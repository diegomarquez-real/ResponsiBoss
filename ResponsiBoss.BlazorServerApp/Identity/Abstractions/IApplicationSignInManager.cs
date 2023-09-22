using ResponsiBoss.BlazorServerApp.Identity.Results;

namespace ResponsiBoss.BlazorServerApp.Identity.Abstractions
{
    public interface IApplicationSignInManager
    {
        Task SignInAsync(string email, string password);
        Task<AuthenticationResult> SignInResultAsync(string email, string password);
    }
}