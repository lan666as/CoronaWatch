using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaWatchLibrary
{
    public class CoronaWatch
    {
        public Region World = new Region("World", Region.EnumLevel.World, new Coordinate());
        public CoronaWatch()
        {
            World.Report = JHUDataService.GetReports();
        }
    }
}
