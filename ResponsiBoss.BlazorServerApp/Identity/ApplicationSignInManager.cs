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
        private readonly CustomStorage _customStorage;
        private readonly IUserClient _userClient;
        private readonly ILogger<ApplicationSignInManager> _logger;

        public ApplicationSignInManager(IUserClient userClient,
            ILogger<ApplicationSignInManager> logger,
            IUserClaimService userClaimService,
            IHttpContextAccessor httpContextAccessor,
            CustomStorage customStorage)
        {
            _userClient = userClient;
            _logger = logger;
            _userClaimService = userClaimService;
            _httpContextAccessor = httpContextAccessor;
            _customStorage = customStorage;
        }

        public async Task SignInAsync(AuthTokenModel authTokenModel)
        {
            var identity = CreateIdentity(authTokenModel.UserId, authTokenModel.Email, authTokenModel.Token);

            await _httpContextAccessor.HttpContext.SignOutAsync();

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties() { IsPersistent = false });
        }

        public async Task<AuthenticationResult> SignInResultAsync(string email, string password)
        {
            bool isAuthenticated = false;

            AuthTokenModel authTokenModel;

            try
            {
                authTokenModel = await _userClient.AuthenticateAsync(new UserLoginModel() { Email = email, Password = password });

                isAuthenticated = !string.IsNullOrEmpty(authTokenModel?.Token);
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

            var authenticationResult = new AuthenticationResult() { AuthTokenTempId = Guid.NewGuid() };

            _customStorage.UserSession.SaveItemEncrypted(authenticationResult.AuthTokenTempId.Value, authTokenModel);

            return authenticationResult;
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