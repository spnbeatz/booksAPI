using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookApi.Models
{
    public class Author
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
        [JsonIgnore]
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}