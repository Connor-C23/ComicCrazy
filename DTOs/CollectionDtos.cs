namespace ComicCrazy.DTOs
{

    public record AddToCollectionRequest(string status);
    
    public record CollectionItemResponse(
        Guid ComicId,
        string Title,
        string Series,
        string Publisher,
        int IssueNumber,
        string Status,
        DateTime AddedAt);
}
