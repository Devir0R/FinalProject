using DataAccess;
using System;
using System.Collections;
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
                    return ClonePlayersDA(obj);
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
                        ret.Add(ClonePlayersDA(p));
                    }
                }
            }
            return ret;
        }

        public Players ClonePlayersDA(Players obj)
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
                Position1 = obj.Position1,
                date_of_birth = obj.date_of_birth,
                jerseyNum = obj.jerseyNum
            };
            ret.Position1.Players.Clear();
            ret.Position1.Position1 = ret.Position1.Position1.Trim();
            ret.Position1.FormationPosition = ret.Position1.FormationPosition.Trim();
            return ret;
        }

        internal List<Players> GetTopPlayers(int res)
        {
            Players[] arr = new Players[res];
            List<Players> ret = new List<Players>();
            using (projDBEntities entities = new projDBEntities())
            {
                foreach(Players p in entities.Players)
                {
                    EnterToListIfInTheTop5(arr, p);
                }
                foreach(Players p in arr)
                {
                    ret.Add(ClonePlayersDA(p));
                }

            }
            return ret;
        }

        private void EnterToListIfInTheTop5(Players[] ret, Players p)
        {
            int i = 0;
            while (i<ret.Length && ret[i] != null )
            {
                i++;
            }
            if (i < ret.Length)
            {
                ret[i] = p;
            }
            else
            {
                Players it = p;
                Players tmp = null;
                for (int j = 0;j<ret.Length;j++)
                {
                    if(ret[j].Users.Count < it.Users.Count)
                    {
                        tmp = ret[j];
                        ret[j] = it;
                        it = tmp;
                        break;
                    }
                }
            }
        }

        internal Players UpdatePlayer(string name, Players newValues)
        {
            using (projDBEntities entities = new projDBEntities())
            {
                Players obj = entities.Players.FirstOrDefault(e => e.name.ToLower() == name.ToLower());
                if (obj != null)
                {
                    obj.club = newValues.club;
                    obj.date_of_birth = newValues.date_of_birth;
                    obj.injured = newValues.injured;
                    obj.jerseyNum = newValues.jerseyNum;
                    obj.nationality = newValues.nationality;
                    obj.position = newValues.position;
                    obj.suspended = newValues.suspended;
                    entities.SaveChanges();
                    return obj;
                }
                else return null;
            }
        }
    }
}