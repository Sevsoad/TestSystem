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
    
    public partial class TestRuns
    {
        public TestRuns()
        {
            this.TestResults = new HashSet<TestResults>();
        }
    
        public int Id { get; set; }
        public int AlgorithmId { get; set; }
        public int TestSetId { get; set; }
        public int UserId { get; set; }
        public System.DateTime DateOfRun { get; set; }
        public bool RocCurveCalc { get; set; }
        public string Status { get; set; }
        public Nullable<int> ReTeachNum { get; set; }
        public string RocClassNumber { get; set; }
    
        public virtual Algorithms Algorithms { get; set; }
        public virtual ICollection<TestResults> TestResults { get; set; }
        public virtual TestSets TestSets { get; set; }
        public virtual Users Users { get; set; }
    }
}
