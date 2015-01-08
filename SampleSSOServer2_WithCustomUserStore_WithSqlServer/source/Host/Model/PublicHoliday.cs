namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PublicHoliday
    {
        public PublicHoliday()
        {
            PublicHolidayGroups = new HashSet<PublicHolidayGroup>();
        }

        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public string Groups { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        public virtual ICollection<PublicHolidayGroup> PublicHolidayGroups { get; set; }
    }
}
