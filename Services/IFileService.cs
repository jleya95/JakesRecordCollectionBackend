using Capstone.Models;
using System.Collections.Generic;
namespace Capstone.Services
{
    public interface IFileService
    {
        List<Record> ReadFile();
    }
}
