using api.custom.system.Models;
using api__custom_system.Models;
using api__custom_system.Repository.Dto;
using Microsoft.AspNetCore.Mvc;

namespace api.custom.system.Service.Interfaces
{
    public interface IUserService
    {
        public Task SaveImageProfile(ICollection<IFormFile> file, int id);

        public Task<UserProfile?> GetProfileById(int id);

        public Task<User> GetUserById(int id);

        public Task CreateUser(User user);
        public Task<UserProfile> CreateUserProfile(User user);

        public Task UpdateUserProfile(UserProfile userProfile);

    }
}
