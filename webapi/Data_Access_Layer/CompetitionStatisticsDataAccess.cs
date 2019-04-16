using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using webapi.Players_Update.Updates;

namespace webapi.Data_Access_Layer
{
    public class CompetitionStatisticsDataAccess
    {
        internal void UpdateStatistics(CPlayerUpdate val, HashSet<string> competitions, List<KeyValuePair<string, Func<CompetitionStatistics, bool>>> updates, Players player)
        {
            using (projDBEntities entities = new projDBEntities())
            {
                AddMissingCompetitions(competitions, player, entities);
                
                foreach(KeyValuePair<string, Func<CompetitionStatistics, bool>> update in updates)
                {
                    CompetitionStatistics comp = entities.CompetitionStatistics.FirstOrDefault(e => e.Player_id == player.player_Id && e.Competition_name==update.Key);
                    update.Value.Invoke(comp);

                }
                entities.SaveChanges();

            }
        }

        private static void AddMissingCompetitions(HashSet<string> competitions, Players player, projDBEntities entities)
        {
            foreach (string comp in competitions)
            {
                bool exists = false;
                foreach (CompetitionStatistics stat in entities.CompetitionStatistics)
                {
                    if (stat.Competition_name == comp)
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

        private static CompetitionStatistics CreateCompetitionsStat(Players player, string comp)
        {
            return new CompetitionStatistics()
            {
                Competition_name = comp,
                Players = player,
                Assists = 0,
                Goals = 0,
                Injury = false,
                Offsides = 0,
                Player_id = player.player_Id,
                Red_cards = 0,
                Suspension = false,
                Yellow_Cards = 0
            };
        }
    }
}