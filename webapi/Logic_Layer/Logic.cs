using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using webapi.Data_Access_Layer;
using webapi.Search_Decorator;

namespace webapi.Logic_Layer
{
    public class Logic : ILogic
    {
        readonly IDataAccess DA = new DataAccessImpl();
        readonly PlayersLogic PlayersL = new PlayersLogic();

        public Players GetPlayerByID(int id)
        {
            return DA.GetPlayerByID(id);
        }

        public List<Players> Search(string keyword, string club, int ageUp, int ageDown, string nationality)
        {
            PlainSearch plainSearch = new PlainSearch(keyword);
            ClubSearch clubSearch = new ClubSearch(plainSearch, club);
            AgeSearch ageSearch = new AgeSearch(clubSearch, ageUp, ageDown);
            NationalitySearch natioSearch = new NationalitySearch(ageSearch, nationality);
            return DA.GetPlayersByQuery(natioSearch.ReturnPredicate());
        }
    }
}