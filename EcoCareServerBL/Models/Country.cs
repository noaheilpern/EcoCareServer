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
        public Country()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [StringLength(255)]
        public string CountryName { get; set; }
        [Column("EF")]
        public double Ef { get; set; }

        [InverseProperty(nameof(User.CountryNavigation))]
        public virtual ICollection<User> Users { get; set; }
    }
}
