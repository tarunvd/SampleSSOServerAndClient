namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Group
    {
        public Group()
        {
            GroupChilds = new HashSet<GroupChild>();
        }

        [Key]
        public int Key { get; set; }

        public Guid ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Tenant { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual ICollection<GroupChild> GroupChilds { get; set; }
    }
}
