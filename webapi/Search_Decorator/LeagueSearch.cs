using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Search_Decorator
{
    public class LeagueSearch : SearchParameter
    {
        public LeagueSearch(ISearchDecorator dec, string league) : base(dec)
        {
            if (league == null)
            {
                league = "";
            }
            base.Pred = (e =>
                (e.league ==null  && league =="" ) 
                ||
                (e.league != null && e.league.ToLower().Contains(league.ToLower())));
        }
    }
}