using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webapi.Logic_Layer
{
    interface ILogic
    {
        Players GetPlayerByID(int id);
        List<Players> Search(string keyword,string club, int ageUp, int ageDown, string nationality);
    }
}
