namespace PaidPolyclinic.Models
{
    using System.Collections.Generic;
    
    public partial class Specialization
    {
        public Specialization()
        {
            this.Doctors = new HashSet<Doctor>();
        }
    
        public int Id { get; set; }
        public string SpecializationName { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
