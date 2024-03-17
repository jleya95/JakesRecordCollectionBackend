using Capstone.DAO;
using Capstone.Models;
using Capstone.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileReadController : ControllerBase
    {
        private readonly IFileService fileService;
        private readonly IRecordDao recordDao;

        public FileReadController(IFileService fileService, IRecordDao recordDao)
        {
            this.fileService = fileService;
            this.recordDao = recordDao;
        }

        [HttpGet]
        public ActionResult<List<Record>> GenerateListOfRecords()
        {
            List<Record> listOfRecords = fileService.ReadFile();

            for (int i = 0; i < listOfRecords.Count; i++)
            {
/*                bool recordExists = recordDao.GetRecordBySerialNumber(listOfRecords[i]);
*/                bool recordExists = recordDao.CheckRecordExistence(listOfRecords[i]);

                if (!recordExists)
                {
                    bool recordAddedToDb = recordDao.AddRecordToDb(listOfRecords[i]);
                }
            }

            if (listOfRecords.Count != 0)
            {
                return Ok(listOfRecords);
            }
            return BadRequest();
        }
    }
}
