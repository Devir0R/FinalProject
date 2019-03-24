using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webapi.Search_Decorator
{
    public interface ISearchDecorator
    {
        Func<Players, bool> ReturnPredicate();
    }
}
