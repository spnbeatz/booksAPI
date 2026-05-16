using Microsoft.AspNetCore.Mvc;
using BookApi.Models;
using BookApi.Data;
using Microsoft.EntityFrameworkCore;
using BookApi.DTOs;



namespace BookApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBook()
        {
            return await _context.Book
                .Include(b => b.Author)
                .Select(b => new BookDTO(b))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await _context.Book
                .Include(b => b.Author)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return new BookDTO(book);

        }
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(CreateBook input)
        {
            var book = new Book
            {
                Title = input.Title,
                Year = input.Year,
                AuthorId = input.AuthorId,
            };

            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntry", new { id = book.Id }, book);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, CreateBook input)
        {
            if (id != input.Id)
            {
                return BadRequest();
            }

            var book = new Book()
            {
                Id = id,
                Title = input.Title,
                Year = input.Year,
                AuthorId = input.AuthorId,
            };

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
