using BookApi.Models;
using Microsoft.VisualBasic;

namespace BookApi.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public BookAuthor? Author { get; set; }

        public BookDTO() { }
        public BookDTO(Book book)
        {
            Id = book.Id;
            Title = book.Title;
            Year = book.Year;
            Author = book.Author != null ? new BookAuthor(book.Author) : null;

        }
    }
}
