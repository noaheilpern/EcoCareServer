using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EcoCareServer.Models
{
    [Index(nameof(Email), Name = "UC_Email", IsUnique = true)]
    public partial class User
    {
        [Key]
        [StringLength(255)]
        public string UserName { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(255)]
        public string Pass { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        [Required]
        [StringLength(255)]
        public string Country { get; set; }

        [InverseProperty("UserNameNavigation")]
        public virtual RegularUser RegularUser { get; set; }
        [InverseProperty("UserNameNavigation")]
        public virtual Seller Seller { get; set; }
    }
}
