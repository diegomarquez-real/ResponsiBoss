using ResponsiBoss.Api.Services.Abstractions;
using ResponsiBoss.Data.Abstractions;

namespace ResponsiBoss.Api
{
    public class UserContext : IUserContext
    {
        private readonly IUserClaimService _userClaimService;

        public UserContext(IUserClaimService userClaimService)
        {
            _userClaimService = userClaimService;
        }

        private Guid? _currentUserIdentifier;
        public Guid CurrentUserIdentifier
        {
            get
            {
                if (!_currentUserIdentifier.HasValue)
                {
                    _currentUserIdentifier = _userClaimService.GetCurrentUserIdThrowIfMissing();
                }

                return _currentUserIdentifier.Value;
            }
        }
    }
}