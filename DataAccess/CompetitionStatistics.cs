//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class CompetitionStatistics
    {
        public int Player_id { get; set; }
        public int Red_cards { get; set; }
        public int Yellow_Cards { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Offsides { get; set; }
        public int Appearance { get; set; }
        public Nullable<int> Clean_Sheets { get; set; }
        public string Competition_name { get; set; }
        public int Competition_year { get; set; }
        public Nullable<bool> Suspension { get; set; }
    
        public virtual Players Players { get; set; }
    }
}
