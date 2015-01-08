namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GroupChild
    {
        [Key]
        public int Key { get; set; }

        public int ParentKey { get; set; }

        public Guid ChildGroupID { get; set; }

        public virtual Group Group { get; set; }
    }
}
