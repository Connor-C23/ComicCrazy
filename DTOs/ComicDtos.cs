namespace ComicCrazy.DTOs
{
    public record CreateComicRequest
    (
        string Title,
        string Series,
        string Publisher,
        int IssueNumber,
        DateOnly ReleaseDate,
        string? Description);
}
