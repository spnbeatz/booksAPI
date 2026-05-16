using BookApi.Models;

namespace BookApi.DTOs
{
    public class BookAuthor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public BookAuthor() { }

        public BookAuthor(Author author)
        {
            Id = author.Id;
            FirstName = author.FirstName;
            LastName = author.LastName;

        }
    }
}
