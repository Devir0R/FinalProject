using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Data_Access_Layer
{
    public class PlayersDataAccess
    {
        public Players GetPlayersByID(int Id)
        {
            using (projDBEntities entities = new projDBEntities())
            {
                Players obj = entities.Players.FirstOrDefault(e => e.player_Id == Id);
                if (obj != null)
                {
                    return ClonePlayers(obj);
                }
                else return null;
            }
        }

        internal List<Players> GetPlayersByQuery(Func<Players, bool> pred)
        {
            List<Players> ret = new List<Players>();
            using (projDBEntities entities = new projDBEntities())
            {
                foreach (Players p in entities.Players)
                {
                    if (pred.Invoke(p))
                    {
                        ret.Add(ClonePlayers(p));
                    }
                }
            }
            return ret;
        }

        public Players ClonePlayers(Players obj)
        {
            Players ret = new Players
            {
                player_Id = obj.player_Id,
                name = obj.name.Trim(),
                club = obj.club.Trim(),
                nationality = obj.nationality.Trim(),
                in_game = obj.in_game,
                position = obj.position,
                //Users = obj.Users,
                suspended = obj.suspended,
                injured = obj.injured,
                CompetitionStatistics = obj.CompetitionStatistics,
                //Position1 = obj.Position1,
                date_of_birth = obj.date_of_birth,
                jerseyNum = obj.jerseyNum
            };
            return ret;
        }
    }
}