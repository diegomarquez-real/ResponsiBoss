using ResponsiBoss.Api.Models;
using ResponsiBoss.Api.Models.Create;
using ResponsiBoss.Api.Models.Update;
using ResponsiBoss.Api.Services.Results;

namespace ResponsiBoss.Api.Services.Abstractions
{
    public interface IUserService
    {
        Task<LoginResult> LoginAsync(UserLoginModel model);
        Task<UserModel> FindByIdAsync(Guid userId);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<Guid> CreateUserAsync(CreateUserModel createUserModel);
        Task UpdateUserAsync(UserModel user, UpdateUserModel updateUserModel);
        Task DeleteUserAsync(Guid userId);
    }
}