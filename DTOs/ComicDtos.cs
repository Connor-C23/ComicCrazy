using System.ComponentModel.DataAnnotations;

namespace ComicCrazy.DTOs
{
    public record CreateComicRequest
    (
        [Required][MaxLength(200)] string Title,
        [Required][MaxLength(200)] string Series,
        [Required][MaxLength(200)] string Publisher,
        [Range(1,1000)]int IssueNumber,
        DateOnly ReleaseDate,
        [MaxLength(1000)]string? Description);
}
