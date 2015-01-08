namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CustomFieldImportJobError
    {
        public int ID { get; set; }

        public int RowNumber { get; set; }

        public string ErrorMessage { get; set; }

        public int CustomFieldImportJobId { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        public virtual CustomFieldImportJob CustomFieldImportJob { get; set; }
    }
}
