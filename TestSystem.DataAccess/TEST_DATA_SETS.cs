//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestSystem.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class TEST_DATA_SETS
    {
        public TEST_DATA_SETS()
        {
            this.TEST_RUNS = new HashSet<TEST_RUNS>();
        }
    
        public int Id { get; set; }
        public int TestCreatorId { get; set; }
        public System.DateTime DateOfCreation { get; set; }
        public int NumberOfRuns { get; set; }
        public string TestDescription { get; set; }
        public string TestFieldOfUse { get; set; }
    
        public virtual ICollection<TEST_RUNS> TEST_RUNS { get; set; }
        public virtual USERS USERS { get; set; }
    }
}