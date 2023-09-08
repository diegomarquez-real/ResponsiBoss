using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ResponsiBoss.Api.Models;
using ResponsiBoss.Api.Models.Create;
using ResponsiBoss.Api.Models.Update;
using ResponsiBoss.Api.Services.Results;
using ResponsiBoss.Data.Abstractions;
using ResponsiBoss.Data.Models;

namespace ResponsiBoss.Api.Services
{
    public class UserService : Abstractions.IUserService
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<UserProfile> _passwordHasher;
        private readonly IUserProfileRepository _userProfileRepository;

        public UserService(IMapper mapper,
            Microsoft.AspNetCore.Identity.IPasswordHasher<UserProfile> passwordHasher,
            IUserProfileRepository userProfileRepository)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _userProfileRepository = userProfileRepository;
        }

        public async Task<LoginResult> LoginAsync(UserLoginModel model)
        {
            var user = await _userProfileRepository.FindByEmailAsync(model.Email);

            // Return NULL If User Not Found.
            if (user == null)
                return new LoginResult(LoginResultCode.EmailNotFound);

            // Checks Whether The Existing Password Matches With The Password Used To Login.
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (result != Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
                return new LoginResult(LoginResultCode.InvalidPassword);

            return new LoginResult(LoginResultCode.Success, user);
        }

        public async Task<UserModel> FindByIdAsync(Guid userId)
        {
            var userProfile = await _userProfileRepository.FindByIdAsync(userId);

            var userModel = _mapper.Map<UserProfile, UserModel>(userProfile);

            return userModel;
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            var userProfiles = await _userProfileRepository.GetAllAsync();

            var userModels = _mapper.Map<IEnumerable<UserProfile>, List<UserModel>>(userProfiles);

            return userModels;
        }

        public async Task<Guid> CreateUserAsync(CreateUserModel createUserModel)
        {
            var userProfile = _mapper.Map<CreateUserModel, UserProfile>(createUserModel);
            userProfile.UserId = Guid.NewGuid();
            userProfile.PasswordHash = HashPassword(userProfile, createUserModel.Password);

            var result = await _userProfileRepository.CreateAsync(userProfile);

            return result.UserId;
        }

        public async Task UpdateUserAsync(UserModel user, UpdateUserModel updateUserModel)
        {
            var userProfile = _mapper.Map<UpdateUserModel, UserProfile>(updateUserModel);
            userProfile.UserId = user.UserId;
            userProfile.PasswordHash = HashPassword(userProfile, updateUserModel.Password);

            await _userProfileRepository.UpdateAsync(userProfile);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            await _userProfileRepository.DeleteAsync(userId);
        }

        private string HashPassword(UserProfile userProfile, string password)
        {
            if (userProfile == null)
                throw new ArgumentNullException(paramName: nameof(userProfile));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(paramName: nameof(password));

            return _passwordHasher.HashPassword(userProfile, password);
        }
    }
}