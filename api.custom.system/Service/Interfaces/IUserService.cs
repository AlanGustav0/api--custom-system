using api.custom.system.Models;
using api.custom.system.Repository.Dto;
using api__custom_system.Models;
using api__custom_system.Repository.Dto;

namespace api.custom.system.Service.Interfaces
{
    public interface IUserService
    {
        public Task SaveImageProfile(ProfileData profileData);

        public Task<User?> GetUserById(int id);

        public Task<User> CreateUser(UserRequestDto user);

        public Task<UserProfileResponseDto> UpdateUserProfile(UserProfileRequestDto userProfileDto);

        public Task<UserProfile> GetUserProfile(int userAdressId);

    }
}
