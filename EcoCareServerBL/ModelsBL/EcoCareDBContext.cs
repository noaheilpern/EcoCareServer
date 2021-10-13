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

        //public User Login(string email, string pswd)
        //{
        //    User user = this.Users
        //        .Where(u => u.Email == email && u.Pass == pswd).FirstOrDefault();

        //    return user;
        //}
    }
}
