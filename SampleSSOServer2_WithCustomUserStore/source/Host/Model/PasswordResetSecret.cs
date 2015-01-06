namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PasswordResetSecret
    {
        [Key]
        public int Key { get; set; }

        public int ParentKey { get; set; }

        public Guid PasswordResetSecretID { get; set; }

        [Required]
        [StringLength(150)]
        public string Question { get; set; }

        [Required]
        [StringLength(150)]
        public string Answer { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
