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
        bool AddUser(Users value);
        Users GetUserByID(int id);
        void UpdateSetting(int id, bool red_flag, bool yellow_flag, bool assists_flag, bool goals_flag, bool sheets_flag);
        Players UpdatePlayer(string name, Players p);
        void UpdateStatistics(Players_Update.Updates.CPlayerUpdate val, HashSet<string> competitions, List<KeyValuePair<string, Func<CompetitionStatistics, bool>>> updates, Players player);
        List<Players> GetTopPlayers(int res);
        bool Subscribe(long u_id, int playerID);
        bool AddUser(long u_id);
        List<Players> GetUserPlayers(long u_id);
    }
}
