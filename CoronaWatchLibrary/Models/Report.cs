using System;

namespace CoronaWatchLibrary
{
    public class Report
    {
        public DateTime LastUpdate { get; set; }
        public Statistic Statistic { get; set; }
        public string Source { get; set; }

        public Report()
        {

        }
        public Report(Statistic statistic)
        {
            this.Statistic = statistic;
        }
        public Report(Statistic statistic, string source)
        {
            this.Statistic = statistic;
            this.Source = source;
        }
        public Report(DateTime date, Statistic statistic)
        {
            this.LastUpdate = date;
            this.Statistic = statistic;
        }
        public Report(DateTime date, Statistic statistic, string source)
        {
            this.LastUpdate = date;
            this.Statistic = statistic;
            this.Source = source;
        }
    }
}
