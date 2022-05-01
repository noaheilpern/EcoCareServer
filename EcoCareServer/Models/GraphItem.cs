using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EcoCareServer.Models
{
    [Keyless]
    [Table("GraphItem")]
    public partial class GraphItem
    {
        public double ValueFootPrint { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateGraph { get; set; }
    }
}
