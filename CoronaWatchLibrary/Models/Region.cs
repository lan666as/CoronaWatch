using System.Collections.Generic;

namespace CoronaWatchLibrary
{
    public class Region
    {
        public enum EnumLevel : ushort
        {
            World = 0,
            Country = 1,
            Province = 2
        }
        public string Name { get; set; }
        public string ParentName { get; protected set; }
        public Coordinate Location { get; set; }
        public string ISOCode { get; set; }
        public string Slug { get; set; }
        public EnumLevel Level { get; set; }
        public List<Region> Children { get; set; }

        public TimeSeries TimeSeries { get; set; }
        public Report Report { get; set; }

        public Region()
        {

        }
        public Region(string name, EnumLevel level, string slug, string isoCode)
        {
            this.Name = name;
            this.Level = level;
            this.Slug = slug;
            this.ISOCode = isoCode;
        }
        public Region(string name, EnumLevel level, Coordinate location)
        {
            this.Name = name;
            this.Level = level;
            this.Location = location;
            this.Children = new List<Region>();
        }

        // Added new Constructor
        public Region(string name, string isoCode, EnumLevel level, Coordinate location, string parentName)
        {
            this.Name = name;
            this.Level = level;
            this.Location = location;
            this.ParentName = parentName;
            this.ISOCode = isoCode;
            this.Children = new List<Region>();
        }

    }
}
