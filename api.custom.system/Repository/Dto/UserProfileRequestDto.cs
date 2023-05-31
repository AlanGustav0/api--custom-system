namespace api.custom.system.Repository.Dto
{
    public class UserProfileRequestDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? ImageProfile { get; set; }

        public string? Address { get; set; }
    }
}
