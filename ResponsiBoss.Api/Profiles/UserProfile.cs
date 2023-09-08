using AutoMapper;

namespace ResponsiBoss.Api.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<Data.Models.UserProfile, Models.UserModel>();
            CreateMap<Models.Create.CreateUserModel, Data.Models.UserProfile>();
            CreateMap<Models.Update.UpdateUserModel, Data.Models.UserProfile>();
        }
    }
}