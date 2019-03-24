using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Search_Decorator
{
    public class AgeSearch : SearchParameter
    {
        public AgeSearch(ISearchDecorator search,int upperBoundary, int bottomBoundary) : base(search)
        {
            base.Pred = new Func<Players, bool>(
                e=> {
                        int age = (DateTime.Now- e.date_of_birth).Value.Days / 365;
                        return age >= bottomBoundary && age <= upperBoundary;
                    }
                );
        }
    }
}