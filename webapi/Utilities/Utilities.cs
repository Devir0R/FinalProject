using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Utilities
{
    public class Utilities
    {
        public static long StringDeviceIDToLong(string deviceID)
        {
            return Convert.ToInt64(deviceID, 16);
        }

        public static Players ClonePlayersAcyclic(Players obj)
        {
            Players ret = new Players
            {
                player_Id = obj.player_Id,
                name = obj.name.Trim(),
                club = obj.club.Trim(),
                nationality = obj.nationality==null? null : obj.nationality.Trim(),
                in_game = obj.in_game,
                position = obj.position,
                //Users = obj.Users,
                suspended = obj.suspended,
                injured = obj.injured,
                CompetitionStatistics = new List<CompetitionStatistics>(),
                Position1 = obj.Position1,
                date_of_birth = obj.date_of_birth,
                jerseyNum = obj.jerseyNum,
                pic = obj.pic
            };
            ret.Position1.Players.Clear();
            foreach(CompetitionStatistics cs in obj.CompetitionStatistics)
            {
                ret.CompetitionStatistics.Add(CloneCompetitionStatisticsAcyclic(cs));
               
            }
                return ret;
        }

        private static CompetitionStatistics CloneCompetitionStatisticsAcyclic(CompetitionStatistics cs)
        {
            CompetitionStatistics csClone = new CompetitionStatistics()
            {
                Assists = cs.Assists,
                Competition_name = cs.Competition_name,
                Goals = cs.Goals,
                Offsides = cs.Offsides,
                Player_id = cs.Player_id,
                Red_cards = cs.Red_cards,
                Suspension = cs.Suspension,
                Yellow_Cards = cs.Yellow_Cards
            };
            return csClone;
        }
    }
}