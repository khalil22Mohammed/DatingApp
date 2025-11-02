namespace FirstApp.Entities
{
    public class photo
    {
        public int id { get; set; }

        public required string Url { get; set; }    
        public string? PoblicId  { get; set; }
        // (Foreign key) Navigation property
        public Member Member { get; set; } = null!;
        public string MemberId { get; set; } = null!;

    }
}
