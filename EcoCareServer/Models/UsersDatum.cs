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
        public double DistanceToWork { get; set; }
        public double ElecticityUsagePerWeek { get; set; }
        public int MeatsMeals { get; set; }
        [Key]
        public int DateT { get; set; }
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(RegularUser.UsersData))]
        public virtual RegularUser UserNameNavigation { get; set; }
    }
}
