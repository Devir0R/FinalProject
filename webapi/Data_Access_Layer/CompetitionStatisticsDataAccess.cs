using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using Updates;

namespace webapi.Data_Access_Layer
{
    public class CompetitionStatisticsDataAccess
    {
        internal void UpdateStatistics(CPlayerUpdate val, HashSet<KeyValuePair<string, int>> competitions, List<KeyValuePair<KeyValuePair<string, int>, Func<CompetitionStatistics, bool>>> updates, int player_Id)
        {
            using (projDBEntities entities = new projDBEntities())
            {
                AddMissingCompetitions(competitions, player_Id, entities);
                entities.SaveChanges();

            }
            using (projDBEntities entities = new projDBEntities())
            {
                foreach (KeyValuePair<KeyValuePair<string, int>, Func<CompetitionStatistics, bool>> update in updates)
                {
                    for (int i = 0; i < 5; i++)
                        try
                        {
                            CompetitionStatistics compToUpdate = entities.CompetitionStatistics.FirstOrDefault(e => e.Player_id == player_Id && e.Competition_name.Trim() == update.Key.Key.Trim() && e.Competition_year == update.Key.Value);
                            if (compToUpdate != null)
                            {
                                update.Value.Invoke(compToUpdate);
                                break;
                            }
                        }
                        catch (NullReferenceException) { }

                }
                try
                {
                    entities.SaveChanges();
                }
                catch (Exception)
                {

                }

            }

        }

        private static void AddMissingCompetitions(HashSet<KeyValuePair<string, int>> competitions, int player_Id, projDBEntities entities)
        {
            foreach (KeyValuePair<string, int> comp in competitions)
            {
                bool exists = false;
                foreach (CompetitionStatistics stat in entities.CompetitionStatistics)
                {
                    if (stat.Competition_name.Trim() == comp.Key.Trim() && stat.Competition_year == comp.Value
                        && stat.Player_id == player_Id)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    entities.CompetitionStatistics.Add(CreateCompetitionsStat(player_Id, comp));
                }
            }
        }

        private static CompetitionStatistics CreateCompetitionsStat(int player_Id, KeyValuePair<string, int> comp)
        {
            return new CompetitionStatistics()
            {
                Competition_name = comp.Key,
                Competition_year = comp.Value,
                Assists = 0,
                Goals = 0,
                Offsides = 0,
                Player_id = player_Id,
                Red_cards = 0,
                Suspension = false,
                Yellow_Cards = 0
            };
        }
    }
}