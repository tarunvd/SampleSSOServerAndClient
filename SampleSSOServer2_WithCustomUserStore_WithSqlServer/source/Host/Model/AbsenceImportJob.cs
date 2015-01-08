namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AbsenceImportJob
    {
        public AbsenceImportJob()
        {
            AbsenceImportInsertUpdates = new HashSet<AbsenceImportInsertUpdate>();
            AbsenceImportJobErrors = new HashSet<AbsenceImportJobError>();
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

        public bool IncludeTotal { get; set; }

        public bool IncludeType { get; set; }

        public bool IncludeReason { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        public virtual ICollection<AbsenceImportInsertUpdate> AbsenceImportInsertUpdates { get; set; }

        public virtual ICollection<AbsenceImportJobError> AbsenceImportJobErrors { get; set; }
    }
}
