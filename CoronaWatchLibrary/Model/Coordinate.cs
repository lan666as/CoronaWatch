using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaWatchLibrary.Model
{
    public class Coordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        Coordinate()
        {
            this.Latitude = 0;
            this.Longitude = 0;
        }

        Coordinate(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public double Distance(Coordinate c)
        {
            double disx = Math.Pow(this.Latitude - c.Latitude, 2.0);
            double disy = Math.Pow(this.Longitude - c.Latitude, 2.0);
            return Math.Sqrt(disx + disy);
        }
    }
}
