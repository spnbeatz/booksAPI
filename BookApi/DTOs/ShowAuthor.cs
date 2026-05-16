using BookApi.Models;

namespace BookApi.DTOs
{
    public class ShowAuthor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Book> Books { get; set; }

        public ShowAuthor() { }
        public ShowAuthor(Author author)
        {
            Id = author.Id;
            FirstName = author.FirstName;
            LastName = author.LastName;
            Books = author.Books;
        }
    }
}
