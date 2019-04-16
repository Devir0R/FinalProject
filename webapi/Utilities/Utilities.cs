using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Utilities
{
    public class Utilities
    {
        public static long StringDeviceIDToLong(string deviceID)
        {
            return Convert.ToInt64(deviceID, 16);
        }

        public static Players ClonePlayers(Players obj)
        {
            Players ret = new Players
            {
                player_Id = obj.player_Id,
                name = obj.name.Trim(),
                club = obj.club.Trim(),
                nationality = obj.nationality.Trim(),
                in_game = obj.in_game,
                position = obj.position,
                //Users = obj.Users,
                suspended = obj.suspended,
                injured = obj.injured,
                CompetitionStatistics = obj.CompetitionStatistics,
                Position1 = obj.Position1,
                date_of_birth = obj.date_of_birth,
                jerseyNum = obj.jerseyNum
            };
            ret.Position1.Players.Clear();
            return ret;
        }
    }
}