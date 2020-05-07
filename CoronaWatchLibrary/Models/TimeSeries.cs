using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaWatchLibrary
{
    public class TimeSeries
    {
        public List<Statistic> StatisticSeries { get; set; }
        public List<DateTime> DateSeries { get; set; }

        TimeSeries()
        {
            StatisticSeries = new List<Statistic>();
            DateSeries = new List<DateTime>();
        }
        TimeSeries(List<Statistic> statistic, List<DateTime> date)
        {
            StatisticSeries = statistic;
            DateSeries = date;
        }

        public DateTime LastUpdate()
        {
            return DateSeries[DateSeries.Count - 1];
        }
        public Statistic LastStatistic()
        {
            return StatisticSeries[StatisticSeries.Count - 1];
        }

    }
}
