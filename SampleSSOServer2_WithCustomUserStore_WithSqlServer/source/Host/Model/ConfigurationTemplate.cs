namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ConfigurationTemplate
    {
        public ConfigurationTemplate()
        {
            ConfigurationTemplateSections = new HashSet<ConfigurationTemplateSection>();
        }

        public int ID { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public string ModelName { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        public int Ordering { get; set; }

        public int? OrganisationID { get; set; }

        public int? ResellerID { get; set; }

        public int TemplateId { get; set; }

        public int TemplateType_Value { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Reseller Reseller { get; set; }

        public virtual ICollection<ConfigurationTemplateSection> ConfigurationTemplateSections { get; set; }
    }
}
