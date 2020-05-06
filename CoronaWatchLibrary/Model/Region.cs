using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaWatchLibrary.Model
{
    public class Region
    {
        public enum Level : ushort
        {
            World = 0,
            Country = 1,
            Province = 2
        }
        public string Name { get; set; }
        public string CountryName { get; protected set; }
        public Coordinate Location { get; set; }
        public string ISOCode { get; set; }
    }
}
