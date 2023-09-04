using ResponsiBoss.Api.Models;
using ResponsiBoss.Api.Models.Create;
using ResponsiBoss.Api.Models.Update;
using ResponsiBoss.Data.Abstractions;
using ResponsiBoss.Data.Models;

namespace ResponsiBoss.Api.Services
{
    public class UserService : Abstractions.IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> AddAsync(UserProfile user, CreateUserModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(UserProfile user, UpdateUserModel model)
        {
            throw new NotImplementedException();
        }
    }
}