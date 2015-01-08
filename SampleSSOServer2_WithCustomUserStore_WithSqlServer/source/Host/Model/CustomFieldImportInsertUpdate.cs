namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CustomFieldImportInsertUpdate
    {
        public int ID { get; set; }

        public int RowNumber { get; set; }

        public string RowData { get; set; }

        public bool Insert { get; set; }

        public bool Update { get; set; }

        public int CustomFieldImportJobId { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        public virtual CustomFieldImportJob CustomFieldImportJob { get; set; }
    }
}
