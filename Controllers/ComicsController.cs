using ComicCrazy.API.Data;
using ComicCrazy.API.Models;
using ComicCrazy.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ComicCrazy.Controllers;

[ApiController]
[Route("api/comics")]
public class ComicsController : ControllerBase
{
    private readonly AppDbContext _context; // dependancy injection again, controller needs database

    public ComicsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet] // responds to GET requests
    public async Task<IActionResult> GetAll() // asynchronous function, can handle other requests while waiting on database

    {
        // gets every row from "Comics" table
        var comics = await _context.Comics.ToListAsync();
        return Ok(comics); // returns HTTP 200 status 
    }

    [HttpPost]
    [Authorize (Roles = "Admin")] // checks if user is logged in and if they have admin role using ASP.NET
    public async Task<IActionResult> Create(CreateComicRequest req) // limits what user can change details of to specifically whats in the
                                                                    // Comic class as seen below, ID and Created At is set automatically because DTO wont accept user input of those fields
    {
        var comic = new Comic
        {
            Title = req.Title,
            Series = req.Series,
            Publisher = req.Publisher,
            IssueNumber = req.IssueNumber,
            ReleaseDate = req.ReleaseDate,
            Description = req.Description
        };

            
        _context.Comics.Add(comic);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = comic.Id }, comic);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var comic = await _context.Comics.FindAsync(id);
        if (comic is null)
            return NotFound();

        return Ok(comic);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, Comic updated)
    {
        var comic = await _context.Comics.FindAsync(id);
        if (comic is null)
            return NotFound();

        comic.Title = updated.Title;
        comic.Series = updated.Series;
        comic.Publisher = updated.Publisher;
        comic.IssueNumber = updated.IssueNumber;
        comic.ReleaseDate = updated.ReleaseDate;
        comic.Description = updated.Description;

        await _context.SaveChangesAsync();
        return Ok(comic);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var comic = await _context.Comics.FindAsync(id);
        if (comic is null)
            return NotFound();

        _context.Comics.Remove(comic);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}