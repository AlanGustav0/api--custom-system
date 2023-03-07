using api__auth.Repository.Dto;

namespace api__auth.Models
{
    public class TokenResponse
    {
        public UserResponseDto User { get; set; }

        public string Token { get; set; }
    }
}
