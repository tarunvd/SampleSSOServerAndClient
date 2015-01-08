namespace SampleSSOServer2.Model
{
    using System;

    public partial class EmailJob
    {
        public int ID { get; set; }

        public DateTime Enqueued { get; set; }

        public DateTime? DoNotExecuteBefore { get; set; }

        public int DequeueCount { get; set; }

        public Guid LockIdentifier { get; set; }

        public DateTime? LockedUntil { get; set; }

        public int JobStatus { get; set; }

        public int Priority { get; set; }

        public string Email_From { get; set; }

        public string Email_To { get; set; }

        public string Email_Cc { get; set; }

        public string Email_Bcc { get; set; }

        public string Email_ReplyTo { get; set; }

        public string Email_Subject { get; set; }

        public string Email_TextBody { get; set; }

        public string Email_HtmlBody { get; set; }

        public string Email_Tag { get; set; }

        public string MessageID { get; set; }
    }
}
