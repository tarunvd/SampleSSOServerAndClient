namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ConfigurationTemplateSection
    {
        public int ID { get; set; }

        public string Content { get; set; }

        public int TemplateSection { get; set; }

        public int? ConfigurationTemplate_ID { get; set; }

        public virtual ConfigurationTemplate ConfigurationTemplate { get; set; }
    }
}
