using ComicCrazy.API.Models;

namespace ComicCrazy.API.Models;

public class CollectionItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid ComicId { get; set; }
    public ReadStatus Status { get; set; } = ReadStatus.Owned;
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public Comic Comic { get; set; } = null!;
}

public enum ReadStatus { Owned, Wishlist, Reading }