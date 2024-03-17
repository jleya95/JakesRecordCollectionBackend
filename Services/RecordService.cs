using RecordCollection.Models;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth;

namespace RecordCollection.Services
{
    public class RecordService : IRecordService
    {
        private static RestClient client = null;
        private RestClientOptions options = new RestClientOptions(baseUrl: "https://api.discogs.com")
        {
            Authenticator = OAuth1Authenticator.ForAccessToken(
                consumerKey: "WJBVGFnwtOKaLhYdiXTN",
                consumerSecret: "ETBSIjcNuGcpmNISpnjenMOUHLwssluf",
                token: "jyRvFJfQrGujtIdgsabmiVYFQhRHcVEbaBwrSSYn",
                tokenSecret: "ASAKRAcrauTqFMpPqHfyOvlwtnSuvHssgwzMgPjm",
                OAuthSignatureMethod.PlainText)
        };


        public RecordService()
        {
            if (client == null)
            {
                client = new RestClient(options);
            }
        }

        public DiscogsSearchResult SearchDiscogsForRecord(DiscogsSearchRequest request)
        {
            RestRequest restRequest = new RestRequest(options.BaseUrl + "database/search");
            restRequest.AddParameter("title", request.Title);
            restRequest.AddParameter("artist", request.Artist);
            restRequest.AddParameter("label", request.Label);
            restRequest.AddParameter("releaseYear", request.ReleaseYear);
            restRequest.AddParameter("issueYear", request.IssueYear);
            restRequest.AddParameter("format", request.Format);

            DiscogsSearchResult response = client.Get<DiscogsSearchResult>(restRequest);

            return response;
        }

        public CoverArt GetCoverArtFromDiscogs(int id)
        {
            RestRequest restRequest = new RestRequest(options.BaseUrl + $"masters/{id}");

            CoverArt coverArt = client.Get<CoverArt>(restRequest);

            return coverArt;
        }

        public DiscogsSearchRequest GenerateDiscogsSearchObject(string title, string artist, string label, string serial, string releaseYear, string issueYear)
        {
            DiscogsSearchRequest request = new DiscogsSearchRequest();

            request.Title = title;
            request.Artist = artist;
            request.Label = label;
            request.ReleaseYear = releaseYear;
            request.IssueYear = issueYear;
            request.SerialNumber = serial;

            return request;
        }
    }
}

