namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LinkedAccountClaim
    {
        [Key]
        public int Key { get; set; }

        public int ParentKey { get; set; }

        [Required]
        [StringLength(30)]
        public string ProviderName { get; set; }

        [Required]
        [StringLength(100)]
        public string ProviderAccountID { get; set; }

        [Required]
        [StringLength(150)]
        public string Type { get; set; }

        [Required]
        [StringLength(150)]
        public string Value { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
