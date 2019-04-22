using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using webapi.Players_Update.Updates;

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

        public Players UpdatePlayer(string name,Players newValues)
        {
            return PlayersDA.UpdatePlayer(name, newValues);
        }

        public void UpdateStatistics(CPlayerUpdate val,HashSet<string> competitions, List<KeyValuePair<string, Func<CompetitionStatistics, bool>>> updates, Players player)
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