namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TwoFactorAuthToken
    {
        [Key]
        public int Key { get; set; }

        public int ParentKey { get; set; }

        [Required]
        [StringLength(100)]
        public string Token { get; set; }

        public DateTime Issued { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
