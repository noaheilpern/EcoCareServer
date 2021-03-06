using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EcoCareServer.Models
{
    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            Sales = new HashSet<Sale>();
        }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        public int Price { get; set; }
        [Required]
        [StringLength(4000)]
        public string Description { get; set; }
        [Required]
        [StringLength(255)]
        public string ImageSource { get; set; }
        public bool Active { get; set; }
        [Required]
        [StringLength(255)]
        public string SellersUsername { get; set; }
        [Key]
        public int ProductId { get; set; }

        [InverseProperty(nameof(Sale.Product))]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
