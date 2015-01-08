namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CustomFieldImportJob
    {
        public CustomFieldImportJob()
        {
            CustomFieldImportInsertUpdates = new HashSet<CustomFieldImportInsertUpdate>();
            CustomFieldImportJobErrors = new HashSet<CustomFieldImportJobError>();
        }

        public int ID { get; set; }

        public string FileName { get; set; }

        public string FileIdentifier { get; set; }

        public string StatusMessage { get; set; }

        public DateTime Enqueued { get; set; }

        public DateTime? Completed { get; set; }

        public string User { get; set; }

        public DateTime? DoNotExecuteBefore { get; set; }

        public int DequeueCount { get; set; }

        public Guid LockIdentifier { get; set; }

        public DateTime? LockedUntil { get; set; }

        public int JobStatus { get; set; }

        public int Priority { get; set; }

        public string OrganisationIdentifier { get; set; }

        public int FailedRecords { get; set; }

        public int TotalRecords { get; set; }

        public int Inserts { get; set; }

        public int Updates { get; set; }

        public bool ImportConfirmed { get; set; }

        public int JobType { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        public virtual ICollection<CustomFieldImportInsertUpdate> CustomFieldImportInsertUpdates { get; set; }

        public virtual ICollection<CustomFieldImportJobError> CustomFieldImportJobErrors { get; set; }
    }
}
