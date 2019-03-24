using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webapi.Data_Access_Layer
{
    interface IDataAccess
    {
        Players GetPlayerByID(int id);
        List<Players> GetPlayersByQuery(Func<Players,bool> pred);
    }
}
