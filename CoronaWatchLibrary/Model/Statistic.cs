using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaWatchLibrary.Model
{
    public class Statistic
    {
        public int ConfirmedCases { get; set; }
        public int RecoveredCases { get; set; }
        public int DeathCases { get; set; }

        Statistic()
        {
            this.ConfirmedCases = 0;
            this.RecoveredCases = 0;
            this.DeathCases = 0;
        }

        Statistic(int confirmed, int recovered, int death)
        {
            this.ConfirmedCases = confirmed;
            this.RecoveredCases = recovered;
            this.DeathCases = death;
        }
        public int ActiveCases
        {
            get
            {
                return this.ConfirmedCases - this.DeathCases - this.RecoveredCases;
            }
        }
    }
}
