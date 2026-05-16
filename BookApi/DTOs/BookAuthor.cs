using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BookApi.Models;

namespace BookApi.DTOs
{
    public class BookAuthor
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        [JsonPropertyName("last_name")]
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
