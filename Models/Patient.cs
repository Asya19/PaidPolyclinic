namespace PaidPolyclinic.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patient
    {
        public Patient()
        {
            this.Receptions = new HashSet<Reception>();
        }
    
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
    
        public virtual ICollection<Reception> Receptions { get; set; }
    }
}
