using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CLDV_Framework.Models
{
    public class Invoice
    {

        public virtual Job Jobs { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual JobType JobType { get; set; }
        public virtual Material Material { get; set; }
        public virtual Job_Material Job_Material{ get; set; }
        public virtual  Job_Employee Job_Employee { get; set; }

    }
}