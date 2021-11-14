using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EcoCareServerBL.Models
{
    [Table("Seller")]
    public partial class Seller
    {
        [Key]
        [StringLength(255)]
        public string UserName { get; set; }
        [Required]
        [StringLength(255)]
        public string PhoneNum { get; set; }
        [Required]
        [StringLength(255)]
        public string Country { get; set; }

        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(User.Seller))]
        public virtual User UserNameNavigation { get; set; }
    }
}
