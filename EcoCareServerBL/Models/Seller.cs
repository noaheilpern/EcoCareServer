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
        [StringLength(1)]
        public string UserName { get; set; }
        [Required]
        [StringLength(1)]
        public string PhoneNum { get; set; }
        [Required]
        [StringLength(1)]
        public string Country { get; set; }
        [Required]
        [StringLength(1)]
        public string City { get; set; }
        [Required]
        [StringLength(1)]
        public string Street { get; set; }
    }
}
