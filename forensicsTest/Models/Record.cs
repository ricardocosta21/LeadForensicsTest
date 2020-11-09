using System;
namespace forensicsTest.Models
{
    public class Record
    {
        public string Name { get; private set; }
        public string Website { get; private set; }
        public bool HasGoogle { get; private set; }
        public double ScanDuration { get; private set; }

        public Record(string name, string website, bool hasGoogle, double scanDuration)
        {
            Name = name;
            Website = website;
            HasGoogle = hasGoogle;
            ScanDuration = scanDuration;
        }       
    }
}
