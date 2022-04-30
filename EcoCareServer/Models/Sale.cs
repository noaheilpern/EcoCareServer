using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EcoCareServer.Models
{
    public partial class Sale
    {
        [Required]
        [StringLength(255)]
        public string BuyerUserName { get; set; }
        [Required]
        [StringLength(255)]
        public string SellerUserName { get; set; }
        public int ProductId { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateBought { get; set; }
        public int PriceBought { get; set; }
        [Key]
        public int SaleId { get; set; }

        [ForeignKey(nameof(BuyerUserName))]
        [InverseProperty(nameof(RegularUser.Sales))]
        public virtual RegularUser BuyerUserNameNavigation { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Sales")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(SellerUserName))]
        [InverseProperty(nameof(Seller.Sales))]
        public virtual Seller SellerUserNameNavigation { get; set; }
    }
}
