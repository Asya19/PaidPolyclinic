namespace PaidPolyclinic.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reception
    {
        public int Id { get; set; }
        public Nullable<int> DoctorID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<System.DateTime> ReceptionDate { get; set; }
        public string Diagnosis { get; set; }
        public Nullable<int> Price { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
