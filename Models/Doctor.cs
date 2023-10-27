namespace PaidPolyclinic.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Doctor
    {
        public Doctor()
        {
            this.Receptions = new HashSet<Reception>();
        }
    
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public Nullable<int> SpecializationID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public byte[] Image { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<Reception> Receptions { get; set; }
    }
}
