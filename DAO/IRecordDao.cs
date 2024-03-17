using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IRecordDao
    {
        bool CheckRecordExistence(Record record);
        Record GetRecord(string title, string artist, string serial, string releaseYear, string issueYear);

        bool AddRecordToDb(Record record);
        List<Record> GetAllRecords();

    }
}
