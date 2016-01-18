//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test_Google_Api.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class restaurants
    {
        public restaurants()
        {
            this.usercomments = new HashSet<usercomments>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> NetworkId { get; set; }
        public string Phones { get; set; }
        public string WorkTime { get; set; }
        public string KitchenType { get; set; }
        public Nullable<int> SumAmount { get; set; }
        public string Propositions { get; set; }
        public string Music { get; set; }
        public string Children { get; set; }
        public string Address { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Lattitude { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> KitchenMark { get; set; }
        public Nullable<int> InteriorMark { get; set; }
        public string ReviewText { get; set; }
        public string CustomLabel { get; set; }
        public Nullable<System.DateTime> DateOfCreation { get; set; }
        public Nullable<int> ServiceMark { get; set; }
    
        public virtual districts districts { get; set; }
        public virtual networks networks { get; set; }
        public virtual ICollection<usercomments> usercomments { get; set; }
    }
}
