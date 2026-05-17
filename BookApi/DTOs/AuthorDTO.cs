using System.Text.Json.Serialization;
using BookApi.Models;

namespace BookApi.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        public AuthorDTO() { }

        public AuthorDTO(Author author)
        {
            Id = author.Id;
            FirstName = author.FirstName;
            LastName = author.LastName;
        }
    }
}