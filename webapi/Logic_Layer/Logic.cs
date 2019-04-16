using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using webapi.Data_Access_Layer;
using webapi.Players_Update.Updates;

namespace webapi.Logic_Layer
{
    public class Logic : ILogic
    {
        readonly IDataAccess DA = new DataAccessImpl();
        readonly PlayersLogic PlayersL = new PlayersLogic();
        readonly UsersLogic UsersL = new UsersLogic();
        readonly NotificationsSettingLogic setting = new NotificationsSettingLogic();
        readonly CompetitionsStatisticsLogic stat = new CompetitionsStatisticsLogic();


        public Players GetPlayerByID(int id)
        {
            return DA.GetPlayerByID(id);
        }

        public List<Players> Search(string keyword, string club, int ageUp, int ageDown, string nationality)
        {

            Func<Players, bool> pred = PlayersL.CreatePredicate(keyword, club, ageUp, ageDown, nationality);
            return DA.GetPlayersByQuery(pred);
        }

        public bool AddUser(int id)
        {
            Users value = new Users()
            {
                User_Id = id,
                NotificationsSettings = new NotificationsSettings(),
                Players = new List<Players>()
            };
            return DA.AddUser(value);
        }

        public Users GetUserByID(int id) {
            return DA.GetUserByID(id);
        }

        public void UpdateSetting(int id, int red, int yellow, int assists, int goals, int sheets)
        {
            bool red_flag = red == 1;
            bool yellow_flag = yellow == 1;
            bool assists_flag = assists == 1;
            bool goals_flag = goals == 1;
            bool sheets_flag = sheets == 1;
            DA.UpdateSetting(id,
                red_flag,
                yellow_flag,
                assists_flag,
                goals_flag,
                sheets_flag);
        }

        public string UpdatePlayers(CPlayerUpdate val)
        {
            Players player = Update_A_Player(val);
            if (player!=null)
            {
                return "NOT FOUND";
            }
            UpdateStatistics(val,player);
            return "SUCCESS";
        }

        private void UpdateStatistics(CPlayerUpdate val, Players player)
        {
            HashSet<string> competitions = stat.GetCompetitionsFromUpdate(val.Statistics);
            List<KeyValuePair<string, Func<CompetitionStatistics, bool>>> updates = stat.GenerateUpdates(val.Statistics);
            DA.UpdateStatistics(val,competitions, updates,player);
        }

        public Players Update_A_Player(CPlayerUpdate val)
        {
            Players p = new Players()
            {
                club = val.Club,
                date_of_birth = val.DOB,
                in_game = false,
                jerseyNum = val.JerseyNum,
                nationality = val.Nationality,
                position = val.Position,
                injured = val.Injury.Active,
                suspended = val.Suspension.Active,
            };
            return DA.UpdatePlayer(val.Name, p);
            
        }

        public List<Players> GetTopPlayers(int res)
        {
            List<Players> raw = DA.GetTopPlayers(res);
            return EliminateCycles(raw);
            
        }

        private List<Players> EliminateCycles(List<Players> raw)
        {
            foreach(Players p in raw)
            {
                p.Users.Clear();
            }
            return raw;
        }

        public bool Subscribe(string deviceID, int playerID)
        {
            long u_id = Utilities.Utilities.StringDeviceIDToLong(deviceID);
            return DA.Subscribe(u_id, playerID);
        }

        public bool AddUser(string deviceID)
        {
            long u_id = Utilities.Utilities.StringDeviceIDToLong(deviceID);
            return DA.AddUser(u_id);
        }

        public List<Players> GetUserPlayers(string deviceID)
        {
            long u_id = Utilities.Utilities.StringDeviceIDToLong(deviceID);
            return DA.GetUserPlayers(u_id);
        }
    }
}