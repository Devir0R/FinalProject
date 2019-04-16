using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Data_Access_Layer
{
    public class SubscribersDataAccess
    {
        internal bool Subscribe(long u_id, int playerID)
        {
            using (projDBEntities entities = new projDBEntities())
            {
                Users user = entities.Users.FirstOrDefault(e => e.User_Id == u_id);
                Players player = entities.Players.FirstOrDefault(e => e.player_Id == playerID);
                if (user != null && player!=null)
                {
                    user.Players.Add(player);
                    entities.SaveChanges();
                    return true;
                    
                }
                else
                {
                    return false;
                }
                
            }
        }
    }
}