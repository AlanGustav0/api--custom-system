using System.ComponentModel.DataAnnotations;

namespace api__auth.Repository.Dto
{
    public class UserInfoCreatedDto
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
