using ResponsiBoss.Api.Models;
using ResponsiBoss.Api.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Api.Client.Abstractions
{
    public interface IUserClient
    {
        Task<AuthTokenModel> AuthenticateAsync(UserLoginModel userLoginModel);
        Task<Guid> CreateUserAsync(CreateUserModel createUserModel);
    }
}