using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Search_Decorator
{
    public class GoalsSearch : SearchParameter
    {
        public GoalsSearch(ISearchDecorator search,int goalsup,int goalsdown) : base(search)
        {
            Pred = new Func<DataAccess.Players, bool>(
                e=>
                {
                    int goals = 0;
                    foreach (CompetitionStatistics competitionStatistics in e.CompetitionStatistics)
                    {
                        goals += competitionStatistics.Goals;
                    }
                    return goals >= goalsdown && goals <= goalsup;
                }
                );
        }
    }
}