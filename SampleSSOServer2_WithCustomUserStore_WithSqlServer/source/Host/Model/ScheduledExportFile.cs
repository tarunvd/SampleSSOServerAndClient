namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScheduledExportFile
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Identifier { get; set; }

        public string FileName { get; set; }

        public string UniqueIdentifier { get; set; }

        public DateTime ExportedOn { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }
    }
}
