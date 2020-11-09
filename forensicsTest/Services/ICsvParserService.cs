using forensicsTest.Models;
using System.Collections.Generic;

namespace forensicsTest.Services
{
    public interface ICsvParserService
    {
        List<WebsiteModel> ReadCsvFileToWebsiteModel(string path);
    }
}
