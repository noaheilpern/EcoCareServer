using EcoCareServerBL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoCareServerBL.Models
{
    public partial class EcoCareDBContext: DbContext
    {
        public void AddRegularUser(RegularUser ru)
        {
            this.RegularUsers.Add(ru);
            this.SaveChanges();
        }
        public void AddSeller(Seller s)
        {
            this.Sellers.Add(s);
            this.SaveChanges(); 
        }
        public User Login(string email, string pswd)
        {
            User user = this.Users
                .Where(u => u.Email == email && u.Pass == pswd).FirstOrDefault();

            return user;
        }
        public void UpdateSeller(Seller s)
        {
            this.Sellers.Update(s);
            this.SaveChanges(); 
        }
        public void UpdateUser(RegularUser ru)
        {
            this.RegularUsers.Update(ru);
            this.SaveChanges();
        }
    }
}
