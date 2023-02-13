using System.ComponentModel.DataAnnotations;

namespace api__auth.Models
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
