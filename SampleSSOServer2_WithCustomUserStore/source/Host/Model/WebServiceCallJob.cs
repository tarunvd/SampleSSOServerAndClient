namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WebServiceCallJob
    {
        public int ID { get; set; }

        public DateTime Enqueued { get; set; }

        public string OrganisationIdentifier { get; set; }

        public string Url { get; set; }

        public string AdditionalData { get; set; }

        public string Status { get; set; }

        public string Verb { get; set; }

        public DateTime? DoNotExecuteBefore { get; set; }

        public int DequeueCount { get; set; }

        public Guid LockIdentifier { get; set; }

        public DateTime? LockedUntil { get; set; }

        public int JobStatus { get; set; }

        public int Priority { get; set; }

        public int WebServiceMethodType { get; set; }

        public string Content { get; set; }
    }
}
