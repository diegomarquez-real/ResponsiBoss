using ResponsiBoss.Data.Models;

namespace ResponsiBoss.Api.Services.Results
{
    public class LoginResult
    {
        public LoginResult()
        {

        }

        public LoginResult(LoginResultCode code)
        {
            ResultCode = code;
        }

        public LoginResult(LoginResultCode code, UserProfile user)
        {
            ResultCode = code;
            User = user;
        }

        public LoginResultCode ResultCode { get; set; }
        public Data.Models.UserProfile User { get; set; }
    }

    public enum LoginResultCode
    {
        EmailNotFound = 1,
        InvalidPassword = 2,
        Success = 3
    }
}