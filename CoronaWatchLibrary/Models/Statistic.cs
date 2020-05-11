using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaWatchLibrary
{
    public class Statistic
    {
        // Added StatisticID for DB purpose
        public string StatisticID { get; set; }
        public int ConfirmedCases { get; set; }
        public int RecoveredCases { get; set; }
        public int DeathCases { get; set; }
        public int ActiveCases { get; set; }

        public Statistic()
        {
            this.ConfirmedCases = 0;
            this.RecoveredCases = 0;
            this.DeathCases = 0;
            this.ActiveCases = 0;
        }

        public Statistic(int confirmed, int recovered, int death)
        {
            this.ConfirmedCases = confirmed;
            this.RecoveredCases = recovered;
            this.DeathCases = death;
            this.ActiveCases = this.ConfirmedCases - this.DeathCases - this.RecoveredCases;
        }
    }
}
