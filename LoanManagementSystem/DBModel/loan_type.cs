//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoanManagementSystem.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class loan_type
    {
        public loan_type()
        {
            this.loans = new HashSet<loan>();
        }
    
        public string ID { get; set; }
        public string LOAN_TYPE_ID { get; set; }
        public Nullable<decimal> AMOUNT { get; set; }
        public Nullable<decimal> INSTALLMENT { get; set; }
        public string REMARK { get; set; }
        public Nullable<bool> STATUS { get; set; }
        public string INSERT_USER_ID { get; set; }
        public string UPDATE_USER_ID { get; set; }
        public Nullable<System.DateTime> INSERT_DATETIME { get; set; }
        public Nullable<System.DateTime> UPDATE_DATETIME { get; set; }
    
        public virtual ICollection<loan> loans { get; set; }
    }
}