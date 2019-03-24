using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace webapi.Search_Decorator
{
    public abstract class SearchParameter : ISearchDecorator
    {
        private ISearchDecorator TmpSearch;
        protected Func<Players, bool> Pred;

        public SearchParameter(ISearchDecorator search)
        {
            TmpSearch = search;
        }

        public Func<Players, bool> ReturnPredicate()
        {
            Func<Players, bool> basePred = TmpSearch.ReturnPredicate();
            return new Func<Players, bool>(e => Pred.Invoke(e) && basePred.Invoke(e));
        }
    }
}