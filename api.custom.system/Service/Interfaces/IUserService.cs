using api.custom.system.Models;
using api.custom.system.Repository.Dto;
using api__custom_system.Models;
using api__custom_system.Repository.Dto;
using Microsoft.AspNetCore.Mvc;

namespace api.custom.system.Service.Interfaces
{
    public interface IUserService
    {
        public void SaveImageProfile(ICollection<IFormFile> file, int id);

        public User? GetUserById(int id);

        public User CreateUser(UserRequestDto user);

        public UserProfileResponseDto UpdateUserProfile(UserProfileRequestDto userProfileDto);

        public UserProfile GetUserProfile(int userAdressId);

    }
}
