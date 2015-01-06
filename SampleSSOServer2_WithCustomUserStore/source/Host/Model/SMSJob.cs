namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SMSJob
    {
        public int ID { get; set; }

        public DateTime Enqueued { get; set; }

        public DateTime? DoNotExecuteBefore { get; set; }

        public int DequeueCount { get; set; }

        public Guid LockIdentifier { get; set; }

        public DateTime? LockedUntil { get; set; }

        public int JobStatus { get; set; }

        public int Priority { get; set; }

        public string MessageBody { get; set; }

        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(255)]
        public string ErrorText { get; set; }
    }
}
