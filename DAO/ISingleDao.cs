using RecordCollection.Models;
using System.Collections.Generic;

namespace RecordCollection.DAO
{
    public interface ISingleDao
    {
        SingleEP GetSingle(string title, string artist, string serial, string releaseYear, string issueYear);
        bool CheckSingleExistence(SingleEP single);
        bool AddSingleToDb(SingleEP single);
        public List<SingleEP> GetAllSingles();
    }
}
