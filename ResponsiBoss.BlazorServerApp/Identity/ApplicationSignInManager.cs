using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ResponsiBoss.Api.Client.Abstractions;
using ResponsiBoss.Api.Models;
using ResponsiBoss.BlazorServerApp.Identity.Abstractions;
using ResponsiBoss.BlazorServerApp.Identity.Results;
using System.Security.Claims;

namespace ResponsiBoss.BlazorServerApp.Identity
{
    public class ApplicationSignInManager : IApplicationSignInManager
    {

        private readonly IUserClaimService _userClaimService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserClient _userClient;
        private readonly ILogger<ApplicationSignInManager> _logger;

        public ApplicationSignInManager(IUserClient userClient,
            ILogger<ApplicationSignInManager> logger,
            IUserClaimService userClaimService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userClient = userClient;
            _logger = logger;
            _userClaimService = userClaimService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SignInAsync(string email, string password)
        {
            var authToken = await _userClient.AuthenticateAsync(new UserLoginModel() { Email = email, Password = password });

            var identity = CreateIdentity(authToken.UserId, email, authToken.Token);

            await _httpContextAccessor.HttpContext.SignOutAsync();

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties() { IsPersistent = false });
        }

        public async Task<AuthenticationResult> SignInResultAsync(string email, string password)
        {
            bool isAuthenticated = false;

            Api.Models.AuthTokenModel authToken;

            try
            {
                authToken = await _userClient.AuthenticateAsync(new UserLoginModel() { Email = email, Password = password });

                isAuthenticated = !string.IsNullOrEmpty(authToken?.Token);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: "Error Calling Login.");

                return new AuthenticationResult("Internal Error.");
            }

            if (!isAuthenticated)
            {
                return new AuthenticationResult("Username Or Password Is Not Correct.");
            }

            return new AuthenticationResult();
        }

        private ClaimsIdentity CreateIdentity(Guid userId, string email, string bearerToken)
        {
            var identity = new ClaimsIdentity(authenticationType: CookieAuthenticationDefaults.AuthenticationScheme,
                nameType: ClaimsIdentity.DefaultNameClaimType,
                roleType: ClaimsIdentity.DefaultRoleClaimType);

            var userDdClaim = _userClaimService.BuildUserIdClaim(userId);
            identity.AddClaim(userDdClaim);

            var userNameClaim = _userClaimService.BuildEmailClaim(email);
            identity.AddClaim(userNameClaim);
   
            var bearerTokenClaim = _userClaimService.BuildBearerTokenClaim(bearerToken);
            identity.AddClaim(bearerTokenClaim);

            return identity;
        }
    }
}