using DataAccess;
using System;
using System.Collections.Generic;
using Updates;
using webapi.Players_Update.Enums;

namespace webapi.Logic_Layer
{
    public class CompetitionsStatisticsLogic
    {
        public List<KeyValuePair<KeyValuePair<string,int>, Func<CompetitionStatistics, bool>>> GenerateUpdates(IList<CAthleteStatisticsUpdate> statistics)
        {
            List<KeyValuePair<KeyValuePair<string, int>, Func<CompetitionStatistics, bool>>> updates = new List<KeyValuePair<KeyValuePair<string, int>, Func<CompetitionStatistics, bool>>>();
            foreach (CAthleteStatisticsUpdate update in statistics)
            {
                foreach(CPlayerIndividualStat indStat in update.Stats)
                {
                    updates.Add(new KeyValuePair<KeyValuePair<string, int>, Func<CompetitionStatistics, bool>>(new KeyValuePair<string, int>(update.Competition.ParsedValue,update.Season.ParsedValue),
                        e =>
                            {
                                Int32.TryParse(indStat.Value, out int val);
                                switch (indStat.StatisticType)
                                {
                                    case (int)ESoccerPlayerStatistics.Goals:
                                        e.Goals = val;
                                        break;
                                    case (int)ESoccerPlayerStatistics.Assists:
                                        e.Assists = val;
                                        break;
                                    case (int)ESoccerPlayerStatistics.RedCards:
                                        e.Red_cards = val;
                                        break;
                                    case (int)ESoccerPlayerStatistics.YellowCards:
                                        e.Yellow_Cards = val;
                                        break;
                                    case (int)ESoccerPlayerStatistics.Appearences:
                                        e.Appearance = val;
                                        break;
                                }
                                return true;
                            }
                        ));
                }
            }

            return updates;
        }

        public HashSet<KeyValuePair<string, int>> GetCompetitionsFromUpdate(IList<CAthleteStatisticsUpdate> statistics)
        {
            HashSet<KeyValuePair<string,int>> competitions = new HashSet<KeyValuePair<string, int>>();
            foreach(CAthleteStatisticsUpdate update in statistics)
            {
                competitions.Add(new KeyValuePair<string, int>(update.Competition.ParsedValue,update.Season.ParsedValue));
            }
            return competitions;
        }
    }
}