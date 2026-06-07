using ComicCrazy.API.Models;

namespace ComicCrazy.API.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<CollectionItem> Collection { get; set; } = new List<CollectionItem>();
    }
}
