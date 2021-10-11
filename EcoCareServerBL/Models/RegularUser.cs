using System;
using System.Collections.Generic;

#nullable disable

namespace EcoCareServerBL.Models
{
    public partial class RegularUser
    {
        public RegularUser()
        {
            Goals = new HashSet<Goal>();
            Sales = new HashSet<Sale>();
            UsersData = new HashSet<UsersDatum>();
        }

        public string UserName { get; set; }
        public DateTime Birthday { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int InitialMeatsMeals { get; set; }
        public bool VeganRareMeat { get; set; }
        public bool Vegetarian { get; set; }
        public string Transportation { get; set; }
        public double DistanceToWork { get; set; }
        public double LastElectricityBill { get; set; }
        public int PeopleAtTheHousehold { get; set; }

        public virtual ICollection<Goal> Goals { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<UsersDatum> UsersData { get; set; }
    }
}
