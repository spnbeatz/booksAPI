using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookApi.Models
{
    public class Book
    {
        public int Id {  get; set; }
        [Required]
        [MinLength(1)]
        [DisplayName("title")]
        public string Title { get; set; }
        [Range(0, int.MaxValue)]
        [DisplayName("year")]
        public int Year { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [DisplayName("author")]
        public virtual Author? Author { get; set; }

    }
}
