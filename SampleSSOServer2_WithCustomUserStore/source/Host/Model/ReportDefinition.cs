namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ReportDefinition
    {
        public ReportDefinition()
        {
            Organisations = new HashSet<Organisation>();
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string ReportPath { get; set; }

        public string HelpUrl { get; set; }

        public bool SendUserId { get; set; }

        public bool SendHierarchy { get; set; }

        public bool SendTier3 { get; set; }

        public bool SendASMCaseStatus { get; set; }

        public string Roles { get; set; }

        public bool AllOrganisations { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        [Required]
        public string Key { get; set; }

        public virtual ICollection<Organisation> Organisations { get; set; }
    }
}
