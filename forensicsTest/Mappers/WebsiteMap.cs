

using CsvHelper.Configuration;
using forensicsTest.Models;

namespace forensicsTest.Mappers
{
    public sealed class WebsiteMap : ClassMap<WebsiteModel>
    {
        public WebsiteMap()
        {
            Map(m => m.Name).Index(0);
            Map(m => m.Website).Index(1);
        }
    }
}
