using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapi.Search_Decorator;

namespace webapi.Logic_Layer
{
    public class PlayersLogic
    {

        public Func<Players,bool> CreatePredicate(string keyword, string club, int ageUp, int ageDown, string nationality, string position,string league)
        {
            PlainSearch plainSearch = new PlainSearch(keyword);
            ClubSearch clubSearch = new ClubSearch(plainSearch, club);
            AgeSearch ageSearch = new AgeSearch(clubSearch, ageUp, ageDown);
            NationalitySearch natioSearch = new NationalitySearch(ageSearch, nationality);
            PositionSearch posSearch = new PositionSearch(natioSearch, position);
            LeagueSearch leagueSearch = new LeagueSearch(posSearch, league);
            return leagueSearch.ReturnPredicate();

        }
    }
}