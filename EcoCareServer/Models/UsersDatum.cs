using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EcoCareServer.Models
{
    public partial class UsersDatum
    {
        [Key]
        public int CategoryId { get; set; }
        public double CategoryValue { get; set; }
        [Key]
        [Column(TypeName = "date")]
        public DateTime DateT { get; set; }
        [Key]
        [StringLength(255)]
        public string UserName { get; set; }

        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(RegularUser.UsersData))]
        public virtual RegularUser UserNameNavigation { get; set; }
    }
}
