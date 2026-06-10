using ComicCrazy.API.Data;
using ComicCrazy.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> Create(Comic comic)
    {
        _context.Comics.Add(comic);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = comic.Id }, comic);
    }
}