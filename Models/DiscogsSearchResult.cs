using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RecordCollection.Models
{
    public class DiscogsSearchResult
    {
        [JsonIgnore]
        public Pagination Pagination { get; set; }
        public List<Result> Results { get; set; } = new List<Result>();
    }

    public class Pagination
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public int PerPage { get; set; }
        public int Items { get; set; }
        public URLs Urls { get; set; }
    }

    public class Result
    {
        public string Country { get; set; }
        public string Year { get; set; }
        public List<string> Format { get; set; } = new List<string>();
        public List<string> Label { get; set; } = new List<string>();
        public string Type { get; set; }
        public List<string> Genre { get; set; } = new List<string>();
        public List<string> Style { get; set; } = new List<string>();
        public int Id { get; set; }
        public List<string> Barcode { get; set; } = new List<string>();
        public UserData UserData { get; set; }
        public int MasterId { get; set; }
        public string Master_Url { get; set; }
        public string Uri { get; set; }
        public string Catno { get; set; } // Category Number
        public string Title { get; set; }
        public string Thumb { get; set; }
        public string CoverImage { get; set; }
        public string Resource_Url { get; set; }
        public Community Community { get; set; }
    }

    public class UserData
    {
        public bool InWantlist { get; set; }
        public bool InCollection { get; set; }
    }

    public class Community
    {
        public int Want { get; set; }
        public int Have { get; set; }
    }

    public class URLs
    {
        public string Last { get; set; }
        public string Next { get; set; }
    }

}
