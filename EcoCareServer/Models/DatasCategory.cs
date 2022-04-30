using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EcoCareServer.Models
{
    public partial class DatasCategory
    {
        public DatasCategory()
        {
            UsersData = new HashSet<UsersDatum>();
        }

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; }
        [Key]
        public int CategoryId { get; set; }

        [InverseProperty(nameof(UsersDatum.Category))]
        public virtual ICollection<UsersDatum> UsersData { get; set; }
    }
}
