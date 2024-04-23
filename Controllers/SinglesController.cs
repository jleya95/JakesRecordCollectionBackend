using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RecordCollection.DAO;
using RecordCollection.Models;
using System;
using System.Collections.Generic;

namespace RecordCollection.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SinglesController : ControllerBase
    {
        private readonly ISingleDao _singleDao;
        public SinglesController(ISingleDao singleDao)
        {
            _singleDao = singleDao;
        }

        [HttpGet]
        public ActionResult<List<SingleEP>> GetListOfSingles()
        {
            List<SingleEP> singles = _singleDao.GetAllSingles();
            return singles;
        }

        /*        [HttpGet("{artist}/{title}")]
                public ActionResult<SingleEP> GetSingle(string title, string artist, string serial, string releaseYear, string issueYear)
                {
                    SingleEP singleEP = _singleDao.GetSingle(title, artist, serial, releaseYear, issueYear);
                    return singleEP;
                }
        */
        [HttpGet("Single")]
        public ActionResult<SingleEP> GetSingle1(string title, string artist, string serial, string releaseYear, string issueYear)
        {
            SingleEP singleEP = _singleDao.GetSingle(title, artist, serial, releaseYear, issueYear);
            return singleEP;
        }


        [HttpGet("random")]
        public ActionResult<SingleEP> GetRandomSingle()
        {
            List<SingleEP> singles = _singleDao.GetAllSingles();
            Random random = new Random();
            int randomIndex = random.Next(0, singles.Count);
            SingleEP randomSingle = singles[randomIndex];

            return randomSingle;
        }

        /*        [HttpPost("add")]
                public ActionResult<SingleEP> AddSingle(string fileAs, string artist, string title, string releaseYear, string recordLabel, string issueYear, string serialNumber, string pressing, string color, string notes)
                {
                    SingleEP singleToAdd = MapFormToSingle(fileAs, artist, title, releaseYear, recordLabel, issueYear, serialNumber, pressing, color, notes);
                    bool singleExists = _singleDao.CheckSingleExistence(singleToAdd);
                    bool singleAdded;

                    if(!singleExists)
                    {
                        singleAdded = _singleDao.AddSingleToDb(singleToAdd);
                        if(singleAdded)
                        {
                            return Ok(singleToAdd);
                        } else
                        {
                            return BadRequest("There was a problem adding single");
                        }
                    }

                    return BadRequest("Single already exists in database");
                }
        */
        [HttpPost("add")]
        public ActionResult<SingleEP> AddSingle(string fileAs, string artist, string title, string releaseYear, string recordLabel, string issueYear, string serialNumber, string pressing, string color, string notes)
        {
            SingleEP singleToAdd = MapFormToSingle(fileAs, artist, title, releaseYear, recordLabel, issueYear, serialNumber, pressing, color, notes);
            bool singleExists = _singleDao.CheckSingleExistence(singleToAdd);
            bool singleAdded;

            if (!singleExists)
            {
                singleAdded = _singleDao.AddSingleToDb(singleToAdd);
                if (singleAdded)
                {
                    return Ok(singleToAdd);
                }
                else
                {
                    return BadRequest("There was a problem adding single");
                }
            }

            return BadRequest("Single already exists in database");
        }

        private SingleEP MapFormToSingle(string fileAs, string artist, string title, string releaseYear, string recordLabel, string issueYear, string serialNumber, string pressing, string color, string notes)
        {
            SingleEP single = new SingleEP();

            single.File = fileAs;
            single.Artist = artist;
            single.Title = title;
            if (!releaseYear.IsNullOrEmpty())
            {
                single.ReleaseYear = Convert.ToInt16(releaseYear);
            }
            if (!recordLabel.IsNullOrEmpty())
            {
                single.Label = recordLabel;
            }
            else
            {
                single.Label = "";
            }
            if (!issueYear.IsNullOrEmpty())
            {
                single.IssueYear = Convert.ToInt16(issueYear);
            }
            if (!serialNumber.IsNullOrEmpty())
            {
                single.SerialNumber = serialNumber;
            }
            else
            {
                single.SerialNumber = "";
            }
            if (!pressing.IsNullOrEmpty())
            {
                single.Pressing = pressing;
            }
            else
            {
                single.Pressing = "";
            }
            if (!color.IsNullOrEmpty())
            {
                single.Color = color;
            }
            else
            {
                single.Color = "";
            }
            if (!notes.IsNullOrEmpty())
            {
                single.Notes = notes;
            }
            else
            {
                single.Notes = "";
            }

            return single;
        }

    }
}
