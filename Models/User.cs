using System.ComponentModel.DataAnnotations;

namespace api__auth.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "USER";
        public string ProfileStyle { get; set; } = "DEFAULT";
    }
}
