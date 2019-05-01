using DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Updates;

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
                jerseyNum = obj.jerseyNum,
                pic = obj.pic
            };
            foreach(CompetitionStatistics cs in ret.CompetitionStatistics)
            {
                cs.Players = null;
                
            }
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

        internal Players UpdatePlayer(CPlayerUpdate val, ref bool isCreated)
        {
            using (projDBEntities entities = new projDBEntities())
            {
                Players obj = entities.Players.FirstOrDefault(e => e.name.ToLower() == val.Name.ToLower());
                if (obj != null)
                {
                    obj.club = val.Competitor;
                    obj.date_of_birth = val.DOB;
                    obj.injured = val.Injury.Active;
                    obj.jerseyNum = val.JerseyNum;
                    obj.nationality = val.Nationality;
                    obj.position = val.Position;
                    obj.suspended = val.Suspension.Active;
                }
                else
                {
                    isCreated = true;
                    obj = new Players()
                    {
                        club = val.Competitor,
                        date_of_birth = val.DOB,
                        injured = val.Injury.Active,
                        jerseyNum = val.JerseyNum,
                        nationality = val.Nationality,
                        Position1 = entities.Position.FirstOrDefault(e=>e.position_num== val.Position),
                        suspended = val.Suspension.Active,
                        in_game = false,
                        name = val.Name,
                        pic = "",
                        position = val.Position
                    };
                    entities.Players.Add(obj);
                }
                try
                {
                    entities.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {

                }
                obj = Utilities.Utilities.ClonePlayersAcyclic(obj);
                return obj;
            }
        }
    }
}

/*
System.Data.Entity.Validation.DbEntityValidationException
  HResult=0x80131920
  Message=Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.
  Source=EntityFramework
  StackTrace:
   at System.Data.Entity.Internal.InternalContext.SaveChanges()
   at System.Data.Entity.Internal.LazyInternalContext.SaveChanges()
   at System.Data.Entity.DbContext.SaveChanges()
   at webapi.Data_Access_Layer.PlayersDataAccess.UpdatePlayer(CPlayerUpdate val, Boolean& isCreated) in C:\Users\devir\source\repos\webapi\webapi\Data_Access_Layer\PlayersDataAccess.cs:line 153
   at webapi.Data_Access_Layer.DataAccessImpl.UpdatePlayer(CPlayerUpdate val, Boolean& isCreated) in C:\Users\devir\source\repos\webapi\webapi\Data_Access_Layer\DataAccess.cs:line 45
   at webapi.Logic_Layer.Logic.Update_A_Player(CPlayerUpdate val, Boolean& isCreated) in C:\Users\devir\source\repos\webapi\webapi\Logic_Layer\Logic.cs:line 85
   at webapi.Logic_Layer.Logic.UpdatePlayers(CPlayerUpdate val) in C:\Users\devir\source\repos\webapi\webapi\Logic_Layer\Logic.cs:line 67
   at webapi.Controllers.PlayerUpdateController.Post(CPlayerUpdate val) in C:\Users\devir\source\repos\webapi\webapi\Controllers\PlayerUpdateController.cs:line 56
   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ActionExecutor.<>c__DisplayClass6_2.<GetExecutor>b__2(Object instance, Object[] methodParameters)
   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ActionExecutor.Execute(Object instance, Object[] arguments)
   at System.Web.Http.Controllers.ReflectedHttpActionDescriptor.ExecuteAsync(HttpControllerContext controllerContext, IDictionary`2 arguments, CancellationToken cancellationToken)



 */
