using api.custom.system.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api__custom_system.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "USER";
        public string ProfileStyle { get; set; } = "DEFAULT";
        public virtual UserProfile UserProfile { get; set; }
        public int UserProfileId { get; set; }
    }
}
