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
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBook([FromQuery] int? authorId)
        {
            if (authorId.HasValue)
            {
                return await _context.Book.Include(b => b.Author).Where(b => b.AuthorId == authorId).Select(b => new BookDTO(b)).ToListAsync();
            }
            return await _context.Book
                .Include(b => b.Author)
                .Select(b => new BookDTO(b))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(long id)
        {
            if (id > int.MaxValue || id < int.MinValue)
                return NotFound();

            int bookId = (int)id;
            var book = await _context.Book
                .Include(b => b.Author)
                .Where(b => b.Id == bookId)
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
            try
            {
                var book = new Book
                {
                    Title = input.Title,
                    Year = input.Year,
                    AuthorId = input.AuthorId
                };

                _context.Book.Add(book);
                await _context.SaveChangesAsync();
                Author author = await _context.Author.Where(a => a.Id == input.AuthorId).FirstOrDefaultAsync();
                AuthorDTO dto = new AuthorDTO(author);
                var bookdto = new BookDTO(book);
                bookdto.Author = dto;
                return CreatedAtAction("GetBook", new { id = book.Id }, new
                {
                    Id = book.Id,
                    Title = input.Title,
                    Year = input.Year,
                    AuthorId = input.AuthorId,
                    Author = dto
                });

            } catch (Exception error)
            {
                Console.WriteLine(error);
                return BadRequest();
            }


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(long id, CreateBook input)
        {
            if (id > int.MaxValue || id < int.MinValue)
                return NotFound();

            int bookId = (int)id;

            var book = new Book
            {
                Id = bookId,
                Title = input.Title,
                Year = input.Year,
                AuthorId = input.AuthorId
            };

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(bookId))
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
        public async Task<IActionResult> DeleteBook(long id)
        {
            if (id > int.MaxValue || id < int.MinValue)
                return NotFound();

            int bookId = (int)id;
            var book = await _context.Book.FindAsync(bookId);
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
