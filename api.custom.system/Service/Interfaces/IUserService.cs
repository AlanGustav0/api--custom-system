using api.custom.system.Models;
using api.custom.system.Repository.Dto;
using api__custom_system.Models;
using api__custom_system.Repository.Dto;

namespace api.custom.system.Service.Interfaces
{
    public interface IUserService
    {
        public void SaveImageProfile(ProfileData profileData);

        public User? GetUserById(int id);

        public User CreateUser(UserRequestDto user);

        public UserProfileResponseDto UpdateUserProfile(UserProfileRequestDto userProfileDto);

        public UserProfile GetUserProfile(int userAdressId);

    }
}
