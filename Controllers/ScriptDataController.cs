using Microsoft.AspNetCore.Mvc;
using RecordCollection.DAO;

namespace RecordCollection.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScriptDataController : ControllerBase
    {
        private readonly IDataForScript dataForScriptDao;
        public ScriptDataController(IDataForScript dataForScriptDao) {
            this.dataForScriptDao = dataForScriptDao;
        }

        [HttpGet("records")]
        public ActionResult<string> GetInsertValuesStringForScript()
        {
            string insertValues = dataForScriptDao.GetRecordsValuesForSqlScript();

            return insertValues;
        }

        [HttpGet("singles")]
        public ActionResult<string> GetSinglesInsertValueStringForScript()
        {
            string insertValues = dataForScriptDao.GetSinglesValuesForScript();

            return insertValues;
        }
    }
}
