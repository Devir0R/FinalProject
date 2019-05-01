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
        internal void UpdateStatistics(CPlayerUpdate val, HashSet<KeyValuePair<string,int>> competitions, List<KeyValuePair<KeyValuePair<string, int>, Func<CompetitionStatistics, bool>>> updates, Players player)
        {
            using (projDBEntities entities = new projDBEntities())
            {
                AddMissingCompetitions(competitions, player, entities);
                entities.SaveChanges();
                foreach (KeyValuePair<KeyValuePair<string, int>, Func<CompetitionStatistics, bool>> update in updates)
                {
                    CompetitionStatistics comp = entities.CompetitionStatistics.FirstOrDefault(e => e.Player_id == player.player_Id && e.Competition_name.Trim() == update.Key.Key.Trim() && e.Competition_year == update.Key.Value);
                    update.Value.Invoke(comp);

                }
                try
                {
                    entities.SaveChanges();
                } catch(Exception e)
                {

                }

            }
        }

        private static void AddMissingCompetitions(HashSet<KeyValuePair<string,int>> competitions, Players player, projDBEntities entities)
        {
            foreach (KeyValuePair<string,int> comp in competitions)
            {
                bool exists = false;
                foreach (CompetitionStatistics stat in entities.CompetitionStatistics)
                {
                    if (stat.Competition_name.Trim() == comp.Key.Trim() && stat.Competition_year == comp.Value
                        && stat.Player_id == player.player_Id)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    entities.CompetitionStatistics.Add(CreateCompetitionsStat(player, comp));
                }
            }
        }

        private static CompetitionStatistics CreateCompetitionsStat(Players player, KeyValuePair<string,int> comp)
        {
            return new CompetitionStatistics()
            {
                Competition_name = comp.Key,
                Competition_year = comp.Value,
                Assists = 0,
                Goals = 0,
                Offsides = 0,
                Player_id = player.player_Id,
                Red_cards = 0,
                Suspension = false,
                Yellow_Cards = 0
            };
        }
    }
}