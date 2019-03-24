using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace webapi.Data_Access_Layer
{
    public class DataAccessImpl : IDataAccess
    {
        readonly PlayersDataAccess PlayersDA = new PlayersDataAccess();

        public Players GetPlayerByID(int id)
        {
            return PlayersDA.GetPlayersByID(id);
        }

        public List<Players> GetPlayersByQuery(Func<Players, bool> pred)
        {
            return PlayersDA.GetPlayersByQuery(pred);
        }
    }
}