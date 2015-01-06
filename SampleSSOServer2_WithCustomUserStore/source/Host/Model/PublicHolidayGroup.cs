namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PublicHolidayGroup
    {
        public PublicHolidayGroup()
        {
            PublicHolidays = new HashSet<PublicHoliday>();
        }

        public int ID { get; set; }

        public int HolidayGroup { get; set; }

        public string Description { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        public virtual ICollection<PublicHoliday> PublicHolidays { get; set; }
    }
}
