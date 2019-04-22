using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Data_Access_Layer
{
    public class NotificationsSettingsDataAccess
    {
        internal void UpdateSetting(long id, bool red_flag, bool yellow_flag, bool assists_flag, bool goals_flag, bool sheets_flag, bool appearances_flage)
        {
            using (projDBEntities entities = new projDBEntities())
            {
                NotificationsSettings entity = entities.NotificationsSettings.FirstOrDefault(e => e.User_id == id);
                entity.Red_cards = red_flag;
                entity.Yellow_cards = yellow_flag;
                entity.Assists = assists_flag;
                entity.Goals = goals_flag;
                entity.Clean_sheets = sheets_flag;
                entity.Appearance = appearances_flage;
                entities.SaveChanges();

            }
        }

        internal NotificationsSettings GetNotificationsSettingByID(string deviceID)
        {
            long id = Utilities.Utilities.StringDeviceIDToLong(deviceID);
            using (projDBEntities entities = new projDBEntities())
            {
                NotificationsSettings set = entities.NotificationsSettings.FirstOrDefault(e => e.User_id == id);
                return CloneSetting(set);

            }
        }

        private NotificationsSettings CloneSetting(NotificationsSettings set)
        {
            NotificationsSettings ret = new NotificationsSettings()
            {
                Users = null,
                Assists = set.Assists,
                User_id = set.User_id,
                Clean_sheets = set.Clean_sheets,
                Goals = set.Goals,
                Red_cards = set.Red_cards,
                Yellow_cards = set.Yellow_cards

            };
            return ret;
        }
    }
}