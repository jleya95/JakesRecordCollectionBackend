namespace RecordCollection.Models
{
    public class DiscogsSearchRequest
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Label { get; set; }
        public string ReleaseYear { get; set; }
        public string IssueYear { get; set; }
        public string SerialNumber { get; set; }
        public string Format { get; set; } = "Vinyl";
    }
}
