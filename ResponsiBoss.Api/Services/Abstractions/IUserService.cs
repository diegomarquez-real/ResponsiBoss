using ResponsiBoss.Api.Models;
using ResponsiBoss.Api.Models.Create;
using ResponsiBoss.Api.Models.Update;
using ResponsiBoss.Data.Models;

namespace ResponsiBoss.Api.Services.Abstractions
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllAsync();
        Task<UserModel> GetByIdAsync(Guid userId);
        Task<bool> DeleteAsync(Guid userId);
        Task<bool> AddAsync(UserProfile user, CreateUserModel model);
        Task<bool> UpdateAsync(UserProfile user, UpdateUserModel model);
    }
}