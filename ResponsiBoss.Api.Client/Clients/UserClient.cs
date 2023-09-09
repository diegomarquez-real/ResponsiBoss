using Flurl.Http;
using Microsoft.Extensions.Logging;
using ResponsiBoss.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Api.Client
{
    public class UserClient : ClientBase, Abstractions.IUserClient
    {
        public UserClient(Abstractions.IApiClientSettings apiClientSettings, 
            ILogger<UserClient> logger)
         : base(apiClientSettings, logger)
        {
        }

        protected override string BaseSegment => "Users";

        public Task<AuthTokenModel> AuthenticateAsync(UserLoginModel userLoginModel)
        {
            return UrlBuilderForAnonymous()
               .AppendPathSegment("Authenticate")
               .AllowHttpStatus(System.Net.HttpStatusCode.Unauthorized)
               .PostJsonAsync(userLoginModel)
               .ReceiveJson<AuthTokenModel>();
        }
    }
}