using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaWatchLibrary
{
    public class Report
    {
        public DateTime LastUpdate { get; set; }
        public Statistic Statistic { get; set; }

        public Report()
        {

        }
        public Report(Statistic statistic)
        {
            this.Statistic = statistic;
        }
        public Report(DateTime date, Statistic statistic)
        {
            this.LastUpdate = date;
            this.Statistic = statistic;
        }
    }
}
