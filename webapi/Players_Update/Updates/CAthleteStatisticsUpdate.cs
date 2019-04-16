using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Players_Update.Updates
{

    public class CAthleteStatisticsUpdate
    {
        public CAthleteStatisticsUpdate()
        {
            Stats = new List<CPlayerIndividualStat>();
        }

        public CAthleteStatisticsUpdate(IList<CPlayerIndividualStat> stats) : this()
        {
            Stats = stats;
        }

        public string Country { get; set; }

        public string Competition { get; set; }

        public int Season { get; set; }

        public IList<CPlayerIndividualStat> Stats { get; set; }

        public void AddStat(int StatType, string val)
        {
            if (Stats == null || Stats.IsReadOnly)
            {
                Stats = new List<CPlayerIndividualStat>(Stats);
            }
            Stats.Add(new CPlayerIndividualStat() { StatisticType = StatType, Value = val });
        }
    }
}