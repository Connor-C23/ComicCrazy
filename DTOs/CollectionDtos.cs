using System.ComponentModel.DataAnnotations;

namespace ComicCrazy.DTOs
{

    public record AddToCollectionRequest([Required]string status);
    
    public record CollectionItemResponse(
        Guid ComicId,
        string Title,
        string Series,
        string Publisher,
        int IssueNumber,
        string Status,
        DateTime AddedAt);
}
