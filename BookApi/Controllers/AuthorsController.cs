using Microsoft.AspNetCore.Mvc;
using BookApi.Models;
using BookApi.Data;
using Microsoft.EntityFrameworkCore;
using BookApi.DTOs;



namespace BookApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowAuthor>>> GetAuthor()
        {
            return await _context.Author
                .Include(b => b.Books)
                .Select(b => new ShowAuthor(b))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShowAuthor>> GetAuthor(int id)
        {
            var author = await _context.Author
                .Include(b => b.Books)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if (author == null)
            {
                return NotFound();
            }

            return new ShowAuthor(author);

        }
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(CreateAuthor input)
        {
            var author = new Author
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
            };

            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntry", new { id = author.Id }, author);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, CreateAuthor input)
        {
            if (id != input.Id)
            {
                return BadRequest();
            }

            var author = new Author()
            {
                Id = id,
                FirstName = input.FirstName,
                LastName = input.LastName,
            };

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Author.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.Id == id);
        }
    }
}
