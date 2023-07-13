using api.custom.system.Models;
using api__custom_system.Models;
using AutoMapper;

namespace api.custom.system.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task SaveImageProfile();

        public Task<User?> GetUserById(int id);

        public Task CreateUser(User user);

        public Task UpdateUserProfile();

        public Task<UserProfile> GetUserProfile(int userProfileId);

        public Task<UserProfile> CreateUserProfile(UserProfile userProfile);
    }
}
