namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ConfigurationText
    {
        public int ID { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public string Text { get; set; }

        public int FlagIdentifier { get; set; }

        [Required]
        [StringLength(200)]
        public string FlagType { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        public int? OrganisationID { get; set; }

        public int? ResellerID { get; set; }

        public string KeyDescription { get; set; }

        public int Ordering { get; set; }

        public int ConfigurationDataType { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Reseller Reseller { get; set; }
    }
}
