using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Updates;

namespace webapi.Logic_Layer
{
    interface ILogic
    {
        Players GetPlayerByID(int id);
        List<Players> Search(string keyword,string club, int ageUp, int ageDown, string nationality, string position,string league);
        bool AddUser(int id);
        Users GetUserByID(int id);
        void UpdateSetting(string deviceID, int red, int yellow, int assists, int goals, int sheets,int apps);
        int UpdatePlayers(CPlayerUpdate val);
        List<Players> GetTopPlayers(int res);
        NotificationsSettings GetNotificationsSettingByID(string deviceID);
        bool AddUser(string deviceID);
        bool Subscribe(string deviceID, int playerID);
        List<Players> GetUserPlayers(string deviceID);
        bool UnSubscribe(string deviceID, int playerID);
    }
}
