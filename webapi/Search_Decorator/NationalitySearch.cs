using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Search_Decorator
{
    public class NationalitySearch : SearchParameter
    {
        public NationalitySearch(ISearchDecorator search,string nationality) : base(search)
        {
            if (nationality == null)
            {
                nationality = "";
            }
            Pred = new Func<DataAccess.Players, bool>(e => e.nationality.ToLower().Contains(nationality.ToLower()));
        }
    }
}