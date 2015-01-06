namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SystemTask
    {
        public int ID { get; set; }

        public int TaskType { get; set; }

        public DateTime Enqueued { get; set; }

        public DateTime? DoNotExecuteBefore { get; set; }

        public int DequeueCount { get; set; }

        public Guid LockIdentifier { get; set; }

        public DateTime? LockedUntil { get; set; }

        public int JobStatus { get; set; }

        public int Priority { get; set; }

        public int WorkTotal { get; set; }

        public int WorkDone { get; set; }

        public string AdditionalData { get; set; }
    }
}
