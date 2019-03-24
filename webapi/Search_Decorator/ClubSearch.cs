using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Search_Decorator
{
    public class ClubSearch : SearchParameter
    {

        public ClubSearch(ISearchDecorator search,string club) : base(search)
        {
            base.Pred = (e => e.club.ToLower().Contains(club.ToLower()));
        }
    }
}