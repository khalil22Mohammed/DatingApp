using System.Text.Json.Serialization;

namespace FirstApp.Entities
{
    public class Photo
    {
        public int id { get; set; }

        public required string Url { get; set; }    
        public string? PoblicId  { get; set; }
        // (Foreign key) Navigation property
        [JsonIgnore]
        public Member Member { get; set; } = null!;
        public string MemberId { get; set; } = string.Empty;

    }
}
