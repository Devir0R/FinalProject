using DataAccess;
using System;
using System.Collections.Generic;
using webapi.Players_Update.Updates;

namespace webapi.Logic_Layer
{
    public class CompetitionsStatisticsLogic
    {
        internal List<KeyValuePair<string, Func<CompetitionStatistics, bool>>> GenerateUpdates(IList<CAthleteStatisticsUpdate> statistics)
        {
            List<KeyValuePair<string,Func<CompetitionStatistics, bool>>> updates = new List<KeyValuePair<string, Func<CompetitionStatistics, bool>>>();
            foreach (CAthleteStatisticsUpdate update in statistics)
            {
                foreach(CPlayerIndividualStat indStat in update.Stats)
                {
                    updates.Add(new KeyValuePair<string,Func<CompetitionStatistics, bool>>(update.Competition+ "-" + update.Season,
                        e =>
                            {
                                Int32.TryParse(indStat.Value, out int val);
                                switch (indStat.StatisticType)
                                {
                                    case CPlayerIndividualStat.GOALS:
                                        e.Goals = val;
                                        break;
                                    case CPlayerIndividualStat.ASSISTS:
                                        e.Assists = val;
                                        break;
                                    case CPlayerIndividualStat.REDCARDS:
                                        e.Red_cards = val;
                                        break;
                                    case CPlayerIndividualStat.YELLOWCARDS:
                                        e.Yellow_Cards = val;
                                        break;
                                }
                                return true;
                            }
                        ));
                }
            }

            return updates;
        }

        internal HashSet<string> GetCompetitionsFromUpdate(IList<CAthleteStatisticsUpdate> statistics)
        {
            HashSet<string> competitions = new HashSet<string>();
            foreach(CAthleteStatisticsUpdate update in statistics)
            {
                competitions.Add(update.Competition + "-" + update.Season);
            }
            return competitions;
        }
    }
}