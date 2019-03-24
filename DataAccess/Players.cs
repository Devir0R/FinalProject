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
    
    public partial class Players
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Players()
        {
            this.CompetitionStatistics = new HashSet<CompetitionStatistics>();
            this.Users = new HashSet<Users>();
        }
    
        public int player_Id { get; set; }
        public string name { get; set; }
        public string club { get; set; }
        public Nullable<int> jerseyNum { get; set; }
        public string nationality { get; set; }
        public Nullable<int> position { get; set; }
        public Nullable<System.DateTime> date_of_birth { get; set; }
        public bool injured { get; set; }
        public bool suspended { get; set; }
        public bool in_game { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompetitionStatistics> CompetitionStatistics { get; set; }
        public virtual Position Position1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
