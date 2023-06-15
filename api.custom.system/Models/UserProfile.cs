using api__custom_system.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.custom.system.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string? Email { get; set; }
        public string? Endereco { get; set; }
        public string? Numero { get; set; }
        public string? Cep { get; set; }

        public string? Cidade { get; set; }

        public string? Estado { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
