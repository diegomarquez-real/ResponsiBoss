using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Api.Client.Abstractions
{
    public interface IAuthTokenProvider
    {
        void OnTokenExpired();
        Models.AuthTokenModel GetAuthToken();
        void AssignAuthToken(Models.AuthTokenModel authToken);
    }
}