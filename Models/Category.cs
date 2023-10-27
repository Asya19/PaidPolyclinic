namespace PaidPolyclinic.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Category
    {
        public Category()
        {
            this.Doctors = new HashSet<Doctor>();
        }
    
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> Bet { get; set; }
    
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
