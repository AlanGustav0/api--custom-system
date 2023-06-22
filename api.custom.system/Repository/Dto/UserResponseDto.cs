namespace api__custom_system.Repository.Dto
{
    public class UserResponseDto
    {
        public int id { get; set; }
        public string UserName { get; set; }

        public string Role { get; set; }
        public string ProfileStyle { get; set; }

        public string ImageProfile { get; set; }

        public int UserProfileId { get; set; }
    }
}
