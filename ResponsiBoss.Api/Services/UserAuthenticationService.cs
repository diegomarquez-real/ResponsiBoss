using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ResponsiBoss.Api.Models;
using ResponsiBoss.Api.Options;
using ResponsiBoss.Api.Services.Abstractions;
using ResponsiBoss.Api.Services.Results;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ResponsiBoss.Api.Services
{
    public class UserAuthenticationService : Abstractions.IUserAuthenticationService
    {
        private readonly IOptions<JwtBearerOptions> _jwtBearerOptions;
        private readonly IOptions<TokenOptions> _tokenOptions;
        private readonly IUserService _userService;

        public UserAuthenticationService(IOptions<JwtBearerOptions> jwtBearerOptions,
            IOptions<TokenOptions> tokenOptions,
            IUserService userService)
        {
            _jwtBearerOptions = jwtBearerOptions;
            _tokenOptions = tokenOptions;
            _userService = userService;
        }

        public async Task<AuthTokenModel> AuthenticateAsync(UserLoginModel userLoginModel)
        {
            var loginResult = await _userService.LoginAsync(userLoginModel);

            if (loginResult.ResultCode != LoginResultCode.Success)
                return null;

            var user = loginResult.User;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.EmailAddress),
            };

            var jwtToken = new JwtSecurityToken(
                _jwtBearerOptions.Value.Issuer,
                _jwtBearerOptions.Value.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_jwtBearerOptions.Value.AccessExpirationMinutes),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Value.Key)),
                                                            SecurityAlgorithms.HmacSha256)
            );

            return new Api.Models.AuthTokenModel()
            {
                UserId = user.UserId,
                Email = user.EmailAddress,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            };
        } 
    }
}