using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using Updates;

namespace webapi.Data_Access_Layer
{
    public class DataAccessImpl : IDataAccess
    {
        readonly PlayersDataAccess PlayersDA = new PlayersDataAccess();
        readonly UsersDataAccess UsersDA = new UsersDataAccess();
        readonly NotificationsSettingsDataAccess SettingDA = new NotificationsSettingsDataAccess();
        readonly CompetitionStatisticsDataAccess StatDA = new CompetitionStatisticsDataAccess();
        readonly SubscribersDataAccess subsDA = new SubscribersDataAccess();

        public bool AddUser(Users value)
        {
            return UsersDA.AddUser(value);
        }

        public Players GetPlayerByID(int id)
        {
            return PlayersDA.GetPlayersByID(id);
        }

        public List<Players> GetPlayersByQuery(Func<Players, bool> pred)
        {
            return PlayersDA.GetPlayersByQuery(pred);
        }

        public Users GetUserByID(int id)
        {
            return UsersDA.GetUserByID(id);
        }

        public void UpdateSetting(long id, bool red_flag, bool yellow_flag, bool assists_flag, bool goals_flag, bool sheets_flag,bool appearances_flage)
        {
            SettingDA.UpdateSetting(id, red_flag, yellow_flag, assists_flag, goals_flag, sheets_flag, appearances_flage);
        }

        public Players UpdatePlayer(CPlayerUpdate val,ref bool isCreated)
        {
            return PlayersDA.UpdatePlayer(val,ref isCreated);
        }

        public void UpdateStatistics(CPlayerUpdate val,HashSet<KeyValuePair<string,int>> competitions, List<KeyValuePair<KeyValuePair<string, int>, Func<CompetitionStatistics, bool>>> updates, Players player)
        {
            StatDA.UpdateStatistics(val,competitions, updates,player);
        }

        public List<Players> GetTopPlayers(int res)
        {
            return PlayersDA.GetTopPlayers(res);
        }

        public bool Subscribe(long u_id, int playerID)
        {
            return subsDA.Subscribe(u_id, playerID);
        }

        public bool AddUser(long u_id)
        {
            return UsersDA.AddUser(u_id);
        }

        public List<Players> GetUserPlayers(long u_id)
        {
            return UsersDA.GetUserPlayers(u_id);
        }

        public bool UnSubscribe(long u_id, int playerID)
        {
            return subsDA.UnSubscribe(u_id, playerID);
        }

        public NotificationsSettings GetNotificationsSettingByID(string deviceID)
        {
            return SettingDA.GetNotificationsSettingByID(deviceID);
        }
    }
}