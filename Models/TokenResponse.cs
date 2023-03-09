using api__custom_system.Repository.Dto;

namespace api__custom_system.Models
{
    public class TokenResponse
    {
        public UserResponseDto User { get; set; }

        public string Token { get; set; }
    }
}
