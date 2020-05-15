using System;
using System.Collections.Generic;

namespace CoronaWatchLibrary
{
    public class TimeSeries
    {
        public List<Statistic> StatisticSeries { get; set; }
        public List<DateTime> DateSeries { get; set; }
        public string Source { get; set; }

        public TimeSeries()
        {
            this.StatisticSeries = new List<Statistic>();
            this.DateSeries = new List<DateTime>();
        }
        public TimeSeries(List<Statistic> statistic, List<DateTime> date)
        {
            this.StatisticSeries = statistic;
            this.DateSeries = date;
        }
        public TimeSeries(List<Statistic> statistic, List<DateTime> date, string source)
        {
            this.StatisticSeries = statistic;
            this.DateSeries = date;
            this.Source = source;
        }

        public void Add(Statistic statistic, DateTime dateTime)
        {
            this.StatisticSeries.Add(statistic);
            this.DateSeries.Add(dateTime);
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
