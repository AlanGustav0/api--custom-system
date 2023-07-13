using api__custom_system.Repository.Dto;

namespace api__custom_system.Service.Interfaces
{
    public interface ILoginService
    {
        public Task<UserResponseDto>? AuthUser(string userName, string password);
    }
}
