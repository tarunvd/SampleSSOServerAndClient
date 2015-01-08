namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PhoneNumber
    {
        public int ID { get; set; }

        [Required]
        public string DDI { get; set; }

        public string NGN { get; set; }

        public int PhoneNumberUsage { get; set; }

        public int? OrganisationID { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
