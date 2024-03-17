using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RecordCollection.Models
{
    public class CoverArt
    {
        public List<Image> Images { get; set; } = new List<Image>();
    }
    public class Image
    {
        [JsonIgnore]
        public int Image_Id { get; set; }
        [JsonIgnore]
        public int Discogs_Id { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
        public string Resource_Url { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
