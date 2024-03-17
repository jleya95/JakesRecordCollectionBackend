namespace Capstone.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string File { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Label { get; set; }
        public int IssueYear { get; set; }
        public string SerialNumber { get; set; }
        public string Pressing { get; set; }
        public int DiscNumber { get; set; }
        public string Color { get; set; }
        public string Notes { get; set; }
        public string NeedleInfo { get; set; }
    }
}
