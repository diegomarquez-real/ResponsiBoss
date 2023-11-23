using ResponsiBoss.Api.Services.Abstractions;
using System.Security.Claims;

namespace ResponsiBoss.Api.Services
{
    public class UserClaimService : IUserClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region UserId

        public Claim BuildUserIdClaim(Guid userId)
        {
            return new Claim(ClaimTypes.Sid, userId.ToString());
        }

        public Guid GetCurrentUserIdThrowIfMissing()
        {
            var userId = this.GetCurrentUserId();

            if (userId.HasValue)
                return userId.Value;
            else
                throw new ApplicationException("User Claims Failed To Provide User Id");
        }

        public Guid? GetCurrentUserId()
        {
            var idClaim = this.GetClaimByType(System.Security.Claims.ClaimTypes.Sid);

            if (idClaim == null)
                return null;

            return Guid.TryParse(idClaim.Value, out Guid id) ? id : null;
        }

        #endregion

        #region Email

        public Claim BuildEmailClaim(string email)
        {
            return new Claim(ClaimTypes.Email, email);
        }

        public string GetEmail()
        {
            return this.GetClaimByType(ClaimTypes.Email)?.Value;
        }

        #endregion

        #region SessionKey

        public Claim BuildSessionKeyClaim(string sessionKey)
        {
            return new Claim("session_key", sessionKey);
        }

        public string GetSessionKey()
        {
            return this.GetClaimByType("session_key")?.Value;
        }

        #endregion

        private Claim GetClaimByType(string type)
        {
            //this is where we actually read the claims from the MVC.NET identity user
            var identity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;

            return identity.FindFirst(x => x.Type == type);
        }
    }
}