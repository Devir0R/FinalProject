using webapi.Search_Decorator;

namespace webapi.Logic_Layer
{
    internal class PositionSearch : SearchParameter
    {

        public PositionSearch(ISearchDecorator natioSearch, string position) :base(natioSearch)
        {
            if (position == null)
            {
                position = "";
            }
            base.Pred = (e => e.Position1.FormationPosition.ToLower().Contains(position.ToLower()));
        }
    }
}