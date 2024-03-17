using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordDao recordDao;
        public RecordsController(IRecordDao recordDao)
        {
            this.recordDao = recordDao;
        }

        [HttpGet]
        public ActionResult<List<Record>> GetListOfRecords()
        {
            List<Record> records = recordDao.GetAllRecords();

            return records;
        }

/*        [HttpGet("{artist}/{title}")]
        public ActionResult<Record> GetRecord(string title, string artist, string serial, string releaseYear, string issueYear)
        {
            Record record = recordDao.GetRecord(title, artist, serial, releaseYear, issueYear);

            return record;
        }
*/
        [HttpGet("Record")]
        public ActionResult<Record> GetRecord1(string title, string artist, string serial, string releaseYear, string issueYear)
        {
            Record record = recordDao.GetRecord(title, artist, serial, releaseYear, issueYear);

            return record;
        }


        /*        [HttpGet("{id}")]
                public ActionResult<Record> GetRecordById(int id)
                {
                    Record record = new Record();

                    return record;
                }
        */
        [HttpGet("random")]
        public ActionResult<Record> GetRandomRecord()
        {
            List<Record> records = recordDao.GetAllRecords();
            Random random = new Random();
            int randomIndex = random.Next(0, records.Count);
            Record randomRecord = records[randomIndex];

            return randomRecord;
        }

        [HttpPost("add")]
        public ActionResult<Record> AddRecord(string fileAs, string artist, string title, string releaseYear, string recordLabel, string issueYear, string serialNumber, string pressing, string discNumber, string color, string notes, string needleInfo)
        {
            string[] lineElements = new string[12];
            lineElements[0] = fileAs;
            lineElements[1] = artist;
            lineElements[2] = title;
            lineElements[3] = releaseYear;
            lineElements[4] = recordLabel;
            lineElements[5] = issueYear;
            lineElements[6] = serialNumber;
            lineElements[7] = pressing;
            lineElements[8] = discNumber;
            lineElements[9] = color;
            lineElements[10] = notes;
            lineElements[11] = needleInfo;

            Record recordToAdd = MapToRecord(lineElements);

            bool recordExists = recordDao.CheckRecordExistence(recordToAdd);
            bool recordAdded;

            if (!recordExists)
            {
                recordAdded = recordDao.AddRecordToDb(recordToAdd);
                if (recordAdded)
                {
                    return Ok(recordToAdd);
                }
                else
                {
                    return BadRequest("There was a problem adding record");
                }
            }

            return BadRequest("Record already exists in database");
        }

        private Record MapToRecord(string[] lineElements)
        {
            Record record = new Record();

            int i = 0;
            foreach (string lineElement in lineElements)
            {
                if (lineElement == null)
                {
                    lineElements[i] = "";
                }
                i++;
            }

            record.File = lineElements[0];
            record.Artist = lineElements[1];
            record.Title = lineElements[2];
            /*            if (lineElements[3] != "" && lineElements[3].Length == 4)
                        {
                            record.ReleaseYear = Int32.Parse(lineElements[3]);
                        }
            */
            if (lineElements[3] != "")
            {
                record.ReleaseYear = Int32.Parse(lineElements[3]);
            }
            record.Label = lineElements[4];
            /*            if (lineElements[5] != "" && lineElements[5].Length == 4)
                        {
                            record.IssueYear = Int32.Parse(lineElements[5]);
                        }
            */
            if (lineElements[5] != "")
            {
                record.IssueYear = Int32.Parse(lineElements[5]);
            }
            record.SerialNumber = lineElements[6];
            record.Pressing = lineElements[7];
            if (lineElements[8] != "")
            {
                record.DiscNumber = Int32.Parse(lineElements[8]);
            }
            else
            {
                record.DiscNumber = 1;
            }
            record.Color = lineElements[9];
            record.Notes = lineElements[10];
            record.NeedleInfo = lineElements[11];


            return record;
        }
    }
}
