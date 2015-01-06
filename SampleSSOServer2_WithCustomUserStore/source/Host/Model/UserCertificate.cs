namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserCertificate
    {
        [Key]
        public int Key { get; set; }

        public int ParentKey { get; set; }

        [Required]
        [StringLength(150)]
        public string Thumbprint { get; set; }

        [StringLength(250)]
        public string Subject { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
