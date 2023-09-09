namespace ResponsiBoss.BlazorServerApp.Identity.Abstractions
{
    public interface IUserClaimService
    {

        System.Security.Claims.Claim BuildUserIdClaim(Guid userId);
        Guid? GetCurrentUserId();
        Guid GetCurrentUserIdThrowIfMissing();

        System.Security.Claims.Claim BuildEmailClaim(string email);
        string GetEmail();

        System.Security.Claims.Claim BuildBearerTokenClaim(string bearerToken);
        string GetCurrentBearerToken();
        string GetCurrentBearerTokenThrowIfMissing();
    }
}