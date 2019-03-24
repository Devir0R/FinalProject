using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace webapi.Search_Decorator
{
    public class PlainSearch : ISearchDecorator
    {
        private string _keyword;
        public PlainSearch(string keyword)
        {
            _keyword = keyword.ToLower();
        }

        public Func<Players, bool> ReturnPredicate()
        {
            return new Func<Players, bool>(e => e.name.ToLower().Contains(_keyword));
        }
    }
}