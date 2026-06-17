using ComicCrazy.API.Data;
using ComicCrazy.API.Models;
using ComicCrazy.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ComicCrazy.Controllers;

[ApiController]
[Route("api/collection")]
[Authorize] // authorize every endpoint, all require token
public class CollectionController : ControllerBase
{
    private readonly AppDbContext _context;

    public CollectionController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCollection()
    {
        var userId = GetUserId();

        var items = await _context.CollectionItems
            .Include(c => c.Comic) // include all comics 
            .Where(c => c.UserId == userId) // from logged in user (because endpoint needs token)
            .ToListAsync(); 

        var response = items.Select(c => new CollectionItemResponse(
            c.ComicId,
            c.Comic.Title,
            c.Comic.Series,
            c.Comic.Publisher,
            c.Comic.IssueNumber,
            c.Status.ToString(),
            c.AddedAt));

        return Ok(response);
    }

    [HttpPost("{comicId}")]
    public async Task<IActionResult> AddToCollection(Guid comicId, AddToCollectionRequest req)
    {
        var userId = GetUserId();

        var comic = await _context.Comics.FindAsync(comicId);
        if (comic is null)
            return NotFound("Comic not found.");

        var exists = await _context.CollectionItems
            .AnyAsync(c => c.UserId == userId && c.ComicId == comicId);
        if (exists)
            return Conflict("Comic already in your collection.");

        if (!Enum.TryParse<ReadStatus>(req.status, out var status))
            return BadRequest("Invalid status. Use Owned, Wishlist, or Reading.");

        var item = new CollectionItem
        {
            UserId = userId,
            ComicId = comicId,
            Status = status
        };

        _context.CollectionItems.Add(item);
        await _context.SaveChangesAsync();
        return Ok("Comic added to collection.");
    }

    [HttpPut("{comicId}")]
    public async Task<IActionResult> UpdateStatus(Guid comicId, AddToCollectionRequest req)
    {
        var userId = GetUserId();

        var item = await _context.CollectionItems
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ComicId == comicId);
        if (item is null)
            return NotFound("Comic not in your collection.");

        if (!Enum.TryParse<ReadStatus>(req.status, out var status))
            return BadRequest("Invalid status. Use Owned, Wishlist, or Reading.");

        item.Status = status;
        await _context.SaveChangesAsync();
        return Ok("Status updated.");
    }

    [HttpDelete("{comicId}")]
    public async Task<IActionResult> RemoveFromCollection(Guid comicId)
    {
        var userId = GetUserId();

        var item = await _context.CollectionItems
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ComicId == comicId);
        if (item is null)
            return NotFound("Comic not in your collection.");

        _context.CollectionItems.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private Guid GetUserId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return Guid.Parse(id);
    }
}