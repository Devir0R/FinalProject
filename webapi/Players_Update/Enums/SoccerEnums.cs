using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Players_Update.Enums
{
    public class SoccerEnums
    {
        public enum ESoccerEventTypes
        {
            Goal = 0,
            YellowCard,
            RedCard,
            MissedPenalty
        }

        public enum ESoccerStages
        {
            CurrResult,
            HalfTime,
            End90Minutes,
            ExtraTime,
            Penalty
        }

    }
}