using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EcoCareServer.Models
{
    public partial class Country
    {
        [Key]
        [StringLength(255)]
        public string CountryName { get; set; }
        [Column("EF")]
        public double Ef { get; set; }
    }
}
