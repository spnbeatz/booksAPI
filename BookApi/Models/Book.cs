using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookApi.Models
{
    public class Book
    {
        public int Id {  get; set; }
        [Required]
        [MinLength(1)]
        public string Title { get; set; }
        [Range(0, int.MaxValue)]
        public int Year { get; set; }
        public int AuthorId { get; set; }
        [JsonIgnore]
        public virtual Author? Author { get; set; }

    }
}
