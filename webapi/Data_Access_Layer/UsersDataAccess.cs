using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Data_Access_Layer
{
    public class UsersDataAccess
    {
        public bool AddUser(Users user)
        {
            try
            {
                using (projDBEntities entities = new projDBEntities())
                {
                    if (entities.Users.FirstOrDefault(e => e.User_Id == user.User_Id) == null)
                    {
                        entities.Users.Add(user);
                        entities.NotificationsSettings.Add(user.NotificationsSettings);
                        entities.SaveChanges();
                        
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        internal Users GetUserByID(int id)
        {
            using (projDBEntities entities = new projDBEntities())
            {
                Users obj = entities.Users.FirstOrDefault(e => e.User_Id == id);
                if (obj != null)
                {
                    return EliminateCycles(obj);
                }
                else return null;
            }
        }

        private Users EliminateCycles(Users obj)
        {
            obj.NotificationsSettings.Users = null;
            foreach(Players p in obj.Players)
            {
                p.Users = null;
                p.CompetitionStatistics = null;
                p.Position1 = null;
            }
            return obj;
        }

        internal bool AddUser(long u_id)
        {
            Users user = new Users()
            {
                User_Id = u_id,
                Players = new List<Players>(),
                NotificationsSettings = new NotificationsSettings()
                {
                    Assists = false,
                    Clean_sheets = false,
                    Goals = false,
                    Red_cards = false,
                    Yellow_cards = false,
                    User_id = u_id,
                }
            };
            user.NotificationsSettings.Users = user;
            
            return AddUser(user);
        }

        internal List<Players> GetUserPlayers(long u_id)
        {
            List<Players> ret = new List<Players>();
            using (projDBEntities entities = new projDBEntities())
            {
                Users user = entities.Users.FirstOrDefault(e => e.User_Id == u_id);
                if (user != null)
                {
                    foreach(Players p in user.Players)
                    {
                        ret.Add(Utilities.Utilities.ClonePlayersAcyclic(p));
                    }
                }
            }
            return ret;
        }
    }
}