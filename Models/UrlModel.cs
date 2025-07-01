using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortUrlMvc.Models
{
    public class UrlModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string OriginalUrl { get; set; } = string.Empty;
        public string ShortCode { get; set; } = string.Empty;
        public string? ShortUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}