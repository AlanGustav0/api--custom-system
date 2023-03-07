using System.ComponentModel.DataAnnotations;

namespace api__auth.Repository.Dto
{
    public class UserRequestDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
