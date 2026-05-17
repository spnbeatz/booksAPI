using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class CreateBook
{
    [JsonPropertyName("title")]
    [Required]
    [MinLength(1)]
    public string Title { get; set; }

    [JsonPropertyName("year")]
    [Range(0, int.MaxValue)]
    public int Year { get; set; }

    [JsonPropertyName("authorId")]
    [Range(0, int.MaxValue)]
    public int AuthorId { get; set; }
}
