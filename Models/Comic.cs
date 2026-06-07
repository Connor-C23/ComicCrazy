using ComicCrazy.API.Models;

namespace ComicCrazy.API.Models
{
    public class Comic
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Series {  get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int IssueNumber { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string? Description { get; set; } // ? means description is optional
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }
}
