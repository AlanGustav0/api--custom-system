using System.ComponentModel.DataAnnotations;

namespace api__custom_system.Repository.Dto
{
    public class UserRequestDto
    {
        public string UserName { get; set; }

        public string? NickName { get; set; }

        public string Password { get; set; }

    }
}
