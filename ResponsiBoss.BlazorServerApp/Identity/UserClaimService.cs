using ResponsiBoss.BlazorServerApp.Identity.Abstractions;
using System.Security.Claims;

namespace ResponsiBoss.BlazorServerApp.Identity
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

        #region EmailAddress

        public Claim BuildEmailClaim(string email)
        {
            return new Claim(ClaimTypes.Email, email);
        }

        public string GetEmail()
        {
            return this.GetClaimByType(ClaimTypes.Email)?.Value;
        }

        #endregion

        #region BearerToken

        public Claim BuildBearerTokenClaim(string bearerToken)
        {
            return new Claim("BearerToken", bearerToken);
        }

        public string GetCurrentBearerTokenThrowIfMissing()
        {
            var bearerToken = this.GetCurrentBearerToken();

            if (String.IsNullOrEmpty(bearerToken))
                throw new ApplicationException("User Claims Failed To Provide API Access Code.");
            else
                return bearerToken;
        }

        public string GetCurrentBearerToken()
        {
            return this.GetClaimByType("BearerToken")?.Value;
        }

        #endregion

        private System.Security.Claims.Claim GetClaimByType(string type)
        {
            // This Is Where We Actually Read The Claims From The MVC.NET Identity User.
            var identity = (System.Security.Claims.ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;

            return identity.FindFirst(x => x.Type == type);
        }
    }
}