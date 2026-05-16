using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookApi.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [DisplayName("first_name")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        [DisplayName("last_name")]
        public string LastName { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
