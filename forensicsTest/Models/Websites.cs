using CsvHelper.Configuration.Attributes;

namespace forensicsTest.Models
{
    public class WebsiteModel
    {
        [Name(Constants.CsvHeaders.Name)]
        public string Name { get; set; }
        [Name(Constants.CsvHeaders.Website)]
        public string Website { get; set; }
    }
}
