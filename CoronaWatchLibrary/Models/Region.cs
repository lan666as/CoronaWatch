using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public EnumLevel Level { get; set; }
        public List<Region> Children { get; set; }

        public TimeSeries TimeSeries { get; set; }
        public Report Report { get; set; }

        public Region()
        {

        }
        public Region(string name, EnumLevel level, Coordinate location)
        {
            this.Name = name;
            this.Level = level;
            this.Location = location;
            this.Children = new List<Region>();
        }
        public Region(string name, EnumLevel level, Coordinate location, string parentName)
        {
            this.Name = name;
            this.Level = level;
            this.Location = location;
            this.ParentName = parentName;
            this.Children = new List<Region>();
        }

    }
}
