namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reseller
    {
        public Reseller()
        {
            Accounts = new HashSet<Account>();
            ConfigurationFlags = new HashSet<ConfigurationFlag>();
            ConfigurationTemplates = new HashSet<ConfigurationTemplate>();
            ConfigurationTexts = new HashSet<ConfigurationText>();
            Organisations = new HashSet<Organisation>();
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<ConfigurationFlag> ConfigurationFlags { get; set; }

        public virtual ICollection<ConfigurationTemplate> ConfigurationTemplates { get; set; }

        public virtual ICollection<ConfigurationText> ConfigurationTexts { get; set; }

        public virtual ICollection<Organisation> Organisations { get; set; }
    }
}
