using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RecordCollection.Models;
using RecordCollection.Services;
using System;

namespace RecordCollection.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IRecordService recordService;

        public APIController(IRecordService recordService)
        {
            this.recordService = recordService;
        }

        [HttpGet("search")]
        public ActionResult<string> SearchDiscogsForRecord(string title, string artist, string label, string serial, string releaseYear, string issueYear)
        {
            DiscogsSearchRequest request = recordService.GenerateDiscogsSearchObject(title, artist, label, serial, releaseYear, issueYear);
            DiscogsSearchResult searchResults = recordService.SearchDiscogsForRecord(request);
            if (searchResults.Results.Count != 0 && !searchResults.Results[0].Master_Url.IsNullOrEmpty())
            {
                int recordMasterIndex = searchResults.Results[0].Master_Url.LastIndexOf('/');
                int recordMasterId = Int32.Parse(searchResults.Results[0].Master_Url.Substring(recordMasterIndex + 1));
                CoverArt coverArt = recordService.GetCoverArtFromDiscogs(recordMasterId);
                if (coverArt.Images.Count > 0)
                {
                    return Ok(coverArt.Images[0].Resource_Url);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
