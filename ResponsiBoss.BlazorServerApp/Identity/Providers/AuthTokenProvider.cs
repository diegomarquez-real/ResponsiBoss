using ResponsiBoss.Api.Client.Abstractions;
using ResponsiBoss.Api.Models;
using ResponsiBoss.BlazorServerApp.Identity.Abstractions;

namespace ResponsiBoss.BlazorServerApp.Identity.Providers
{
    public class AuthTokenProvider : IAuthTokenProvider
    {
        private readonly Identity.Abstractions.IUserClaimService _userClaimService;
        private readonly IApplicationSignOutManager _applicationSignOutManager;
        private AuthTokenModel _authToken;

        public AuthTokenProvider(IUserClaimService userClaimService, 
            IApplicationSignOutManager applicationSignOutManager)
        {
            _userClaimService = userClaimService;
            _applicationSignOutManager = applicationSignOutManager;
        }
        public AuthTokenModel GetAuthToken()
        {
            // If Auth Token Has Been Explicitly Set, We'll Use That, Other Wise Get It From The UserClaim Service.
            if (_authToken == null)
            {
                return new AuthTokenModel()
                {
                    Token = _userClaimService.GetCurrentBearerTokenThrowIfMissing(),
                    UserId = _userClaimService.GetCurrentUserIdThrowIfMissing(),
                };
            }
            else
            {
                return _authToken;
            }
        }

        public void AssignAuthToken(AuthTokenModel authToken)
        {
            _authToken = authToken;
        }

        public void OnTokenExpired()
        {
            _applicationSignOutManager.SignOutAsync();
        }
    }
}