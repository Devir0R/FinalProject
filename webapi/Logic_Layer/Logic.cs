﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using webapi.Data_Access_Layer;
using Updates;

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

        public void UpdateSetting(string deviceID, int red, int yellow, int assists, int goals, int sheets, int apps)
        {
            bool red_flag = red == 1;
            bool yellow_flag = yellow == 1;
            bool assists_flag = assists == 1;
            bool goals_flag = goals == 1;
            bool sheets_flag = sheets == 1;
            bool appearances_flage = apps == 1;
            DA.UpdateSetting(Utilities.Utilities.StringDeviceIDToLong(deviceID),
                red_flag,
                yellow_flag,
                assists_flag,
                goals_flag,
                sheets_flag,
                appearances_flage);
        }

        public int UpdatePlayers(CPlayerUpdate val)
        {
            bool isCreated = false;
            Players player = Update_A_Player(val,ref isCreated);
            int player_Id = -1;
            if (isCreated)
            {
                player_Id = player.player_Id; ;
            }
            UpdateStatistics(val,player);
            return player_Id;
        }

        private void UpdateStatistics(CPlayerUpdate val, Players player)
        {
            HashSet<KeyValuePair<string,int>> competitions = stat.GetCompetitionsFromUpdate(val.Statistics);
            List<KeyValuePair<KeyValuePair<string,int>, Func<CompetitionStatistics, bool>>> updates = stat.GenerateUpdates(val.Statistics);
            DA.UpdateStatistics(val,competitions, updates,player);
        }

        public Players Update_A_Player(CPlayerUpdate val, ref bool isCreated)
        {           
            return DA.UpdatePlayer(val, ref isCreated);
            
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

        public bool UnSubscribe(string deviceID, int playerID)
        {
            long u_id = Utilities.Utilities.StringDeviceIDToLong(deviceID);
            return DA.UnSubscribe(u_id, playerID);
        }

        public NotificationsSettings GetNotificationsSettingByID(string deviceID)
        {
            return DA.GetNotificationsSettingByID( deviceID);
        }
    }
}