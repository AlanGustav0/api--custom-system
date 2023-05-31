using api__custom_system.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.custom.system.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? ImageProfile { get; set; }

        public string? Address { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
