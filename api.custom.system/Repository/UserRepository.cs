using api.custom.system.Models;
using api.custom.system.Repository.Dto;
using api.custom.system.Repository.Interfaces;
using api__custom_system.Models;
using api__custom_system.Repository;
using api__custom_system.Repository.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api.custom.system.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task CreateUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public async Task<UserProfile> CreateUserProfile(string nome)
        {
            _context.Add(nome);
            _context.SaveChanges();

            UserProfile userProfile = await _context.UserProfile?.OrderBy(orderId => orderId.Id).LastOrDefaultAsync();

            return userProfile;
        }


        public async Task<User?> GetUserById(int id)
        {
            User? user = await _context.UserInfos.FirstOrDefaultAsync(value => value.Id == id);

            return user;
        }

        public async Task<UserProfile> GetUserProfile(int userProfileId)
        {
            UserProfile? userProfile = await _context.UserProfile.FirstOrDefaultAsync(profileId => profileId.Id == userProfileId);



            return userProfile;
        }

        public async Task SaveImageProfile()
        {
            var response = await _context.SaveChangesAsync();

        }

        public async Task UpdateUserProfile()
        {
            await _context.SaveChangesAsync();
        }

    }
}
