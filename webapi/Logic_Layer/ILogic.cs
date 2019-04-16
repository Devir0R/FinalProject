using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.Players_Update.Updates;

namespace webapi.Logic_Layer
{
    interface ILogic
    {
        Players GetPlayerByID(int id);
        List<Players> Search(string keyword,string club, int ageUp, int ageDown, string nationality);
        bool AddUser(int id);
        Users GetUserByID(int id);
        void UpdateSetting(int id, int red, int yellow, int assists, int goals, int sheets);
        string UpdatePlayers(CPlayerUpdate val);
        List<Players> GetTopPlayers(int res);
        bool AddUser(string deviceID);
        bool Subscribe(string deviceID, int playerID);
        List<Players> GetUserPlayers(string deviceID);
    }
}
