using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EcoCareServerBL.Models
{
    public partial class Goal
    {
        [Key]
        [Column(TypeName = "date")]
        public DateTime DateT { get; set; }
        [Column("Goal")]
        public int Goal1 { get; set; }
        [Required]
        [StringLength(1)]
        public string UserName { get; set; }

        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(RegularUser.Goals))]
        public virtual RegularUser UserNameNavigation { get; set; }
    }
}
