namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Organisation
    {
        public Organisation()
        {
            Accounts = new HashSet<Account>();
            ConfigurationFlags = new HashSet<ConfigurationFlag>();
            ConfigurationTemplates = new HashSet<ConfigurationTemplate>();
            ConfigurationTexts = new HashSet<ConfigurationText>();
            PhoneNumbers = new HashSet<PhoneNumber>();
            ReportDefinitions = new HashSet<ReportDefinition>();
        }

        public int ID { get; set; }

        [Required]
        public string Identifier { get; set; }

        [Required]
        public string Name { get; set; }

        public int ResellerID { get; set; }

        public string ChampionHomePageMessage { get; set; }

        public string ManagerHomePageMessage { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        public string AdministratorHomePageMessage { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<ConfigurationFlag> ConfigurationFlags { get; set; }

        public virtual ICollection<ConfigurationTemplate> ConfigurationTemplates { get; set; }

        public virtual ICollection<ConfigurationText> ConfigurationTexts { get; set; }

        public virtual Reseller Reseller { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public virtual ICollection<ReportDefinition> ReportDefinitions { get; set; }
    }
}
