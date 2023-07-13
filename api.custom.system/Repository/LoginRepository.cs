using api.custom.system.Repository.Interfaces;
using api__custom_system.Models;
using api__custom_system.Repository;
using Microsoft.EntityFrameworkCore;

namespace api.custom.system.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DatabaseContext _context;

        public LoginRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthUser(string userName, string password)
        {
           return await _context.UserInfos.FirstOrDefaultAsync(value => value.UserName == userName && value.Password == password);
     
        }
    }
}
