using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoCareServerBL.Models
{
    public partial class DatasCategory
    {
            [Required]
            [StringLength(255)]
            public string CategoryName { get; set; }
            [Key]
            public int CategoryId { get; set; }
        
    }
}
