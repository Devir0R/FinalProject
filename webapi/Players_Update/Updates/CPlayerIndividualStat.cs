using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Players_Update.Updates
{
    public class CPlayerIndividualStat
    {
        public const int GOALS = 0;
        public const int ASSISTS = 1;
        public const int YELLOWCARDS = 2;
        public const int REDCARDS = 3;
        public const int APPEARANCES = 4;
        public const int LINEUPS = 5;
        public const int SUBSTITUTIONS = 6;
        public int StatisticType { get; set; }
        public string Value { get; set; }
    }
}