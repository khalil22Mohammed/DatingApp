namespace FirstApp.DTOs
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; } 
        public required string token { get; set; }
        public string? ImageUrl { get; set; }
    }
}
