using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Data_Access_Layer
{
    public class NotificationsSettingsDataAccess
    {
        internal void UpdateSetting(int id, bool red_flag, bool yellow_flag, bool assists_flag, bool goals_flag, bool sheets_flag)
        {
            using (projDBEntities entities = new projDBEntities())
            {
                NotificationsSettings entity = entities.NotificationsSettings.FirstOrDefault(e => e.User_id == id);
                entity.Red_cards = red_flag;
                entity.Yellow_cards = yellow_flag;
                entity.Assists = assists_flag;
                entity.Goals = goals_flag;
                entity.Clean_sheets = sheets_flag;
                entities.SaveChanges();

            }
        }
    }
}