using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaWatchLibrary.Model
{
    public class TimeSeries
    {
        public Statistic[] StatisticSeries { get; set; }
        public DateTime[] DateSeries { get; set; }

        public DateTime LastDate()
        {
            return DateSeries[DateSeries.Length - 1];
        }
        public Statistic LastStatistic()
        {
            return StatisticSeries[StatisticSeries.Length - 1];
        }
    }
}
