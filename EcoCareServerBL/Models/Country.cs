using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EcoCareServerBL.Models
{
    public partial class Country
    {
        [Key]
        [Column("Country")]
        [StringLength(255)]
        public string Country1 { get; set; }
        [Column("EF")]
        public double Ef { get; set; }
    }
}
