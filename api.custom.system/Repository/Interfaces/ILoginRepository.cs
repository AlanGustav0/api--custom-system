using api__custom_system.Models;

namespace api.custom.system.Repository.Interfaces
{
    public interface ILoginRepository
    {
        public Task<User?> AuthUser(string userName, string password);

    }
}
