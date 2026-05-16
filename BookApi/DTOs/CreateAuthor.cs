using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookApi.DTOs
{
    public class CreateAuthor
    {
        
        [Required]
        [MinLength(1)]
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        
        [Required]
        [MinLength(1)]
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
    }
}
