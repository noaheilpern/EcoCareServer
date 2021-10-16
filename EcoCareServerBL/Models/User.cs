using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EcoCareServerBL.Models
{
    [Index(nameof(Email), Name = "UC_Email", IsUnique = true)]
    public partial class User
    {
        [Key]
        [StringLength(1)]
        public string UserName { get; set; }
        [Required]
        [StringLength(1)]
        public string Email { get; set; }
        [Required]
        [StringLength(1)]
        public string Pass { get; set; }
        [Required]
        [StringLength(1)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(1)]
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
