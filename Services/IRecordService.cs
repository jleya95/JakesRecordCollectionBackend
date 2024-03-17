using RecordCollection.Models;

namespace RecordCollection.Services
{
    public interface IRecordService
    {
        DiscogsSearchResult SearchDiscogsForRecord(DiscogsSearchRequest request);
        CoverArt GetCoverArtFromDiscogs(int id);

        DiscogsSearchRequest GenerateDiscogsSearchObject(string title, string artist, string label, string serial, string releaseYear, string issueYear);
    }
}
