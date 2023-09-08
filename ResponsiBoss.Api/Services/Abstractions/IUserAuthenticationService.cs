using ResponsiBoss.Api.Models;

namespace ResponsiBoss.Api.Services.Abstractions
{
    public interface IUserAuthenticationService
    {
        Task<AuthTokenModel> AuthenticateAsync(UserLoginModel userLoginModel);
    }
}