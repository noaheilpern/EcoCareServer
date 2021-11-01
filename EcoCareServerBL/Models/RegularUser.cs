using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EcoCareServerBL.Models
{
    [Table("RegularUser")]
    public partial class RegularUser
    {
        public RegularUser()
        {
            Goals = new HashSet<Goal>();
            Sales = new HashSet<Sale>();
            UsersData = new HashSet<UsersDatum>();
        }

        [Key]
        [StringLength(255)]
        public string UserName { get; set; }
        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }
        [Required]
        [StringLength(255)]
        public string Country { get; set; }
        public int InitialMeatsMeals { get; set; }
        public bool VeganRareMeat { get; set; }
        public bool Vegetarian { get; set; }
        [Required]
        [StringLength(255)]
        public string Transportation { get; set; }
        public double DistanceToWork { get; set; }
        public double LastElectricityBill { get; set; }
        public int PeopleAtTheHousehold { get; set; }

        [ForeignKey(nameof(UserName))]
        [InverseProperty(nameof(User.RegularUser))]
        public virtual User UserNameNavigation { get; set; }
        [InverseProperty(nameof(Goal.UserNameNavigation))]
        public virtual ICollection<Goal> Goals { get; set; }
        [InverseProperty(nameof(Sale.BuyerUserNameNavigation))]
        public virtual ICollection<Sale> Sales { get; set; }
        [InverseProperty(nameof(UsersDatum.UserNameNavigation))]
        public virtual ICollection<UsersDatum> UsersData { get; set; }
    }
}
