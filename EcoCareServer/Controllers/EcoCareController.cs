using EcoCareServerBL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoCareServer.Controllers
{
    static class Constants
    {
        public const double MEAT_EMISSION_FACTOR = 7.726;
        public const double AVERAGE_CAR_EMISSION = 0.1684;
        public const int WORK_DAYS = 5;
        public const int DAYS_A_WEEK = 7;
        public const double WEEKS_A_MONTH = 4; 
        
    }

    [Route("EcoCareAPI")]
    [ApiController]
    public class EcoCareController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        EcoCareDBContext context;
        public EcoCareController(EcoCareDBContext context)
        {
            this.context = context;
        }
        #endregion

        [Route("DeleteItem")]
        [HttpPost]

        public bool DeleteItem([FromBody] Product p)
        {

            if (p != null)
            {
                try
                {
                    this.context.DeleteProduct(p);
                    HttpContext.Session.SetObject("theUser", p);
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    context.SaveChanges();
                    //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }
        [Route("DecreaseStars")]
        [HttpPost]

        public bool DecreaseStars([FromQuery]int productId, string userName)
        {
            try
            {
                RegularUser ru = null;
                foreach (RegularUser u in context.RegularUsers)
                {
                    if (u.UserName.Equals(userName))
                        ru = u;
                }
                int starsToDecrease = 0;
                string sellerUserName = null;  
                foreach (Product p in context.Products)
                {
                    if (p.ProductId == productId)
                    {
                        starsToDecrease = p.Price;
                        sellerUserName = p.SellersUsername; 
                    }
                }
                if(ru != null && starsToDecrease != 0)
                {
                    ru.Stars = ru.Stars - starsToDecrease;

                    this.context.UpdateUser(ru);
                    HttpContext.Session.SetObject("theUser", ru);
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    context.SaveChanges();


                    if (sellerUserName != null)
                    {
                        Sale s = new Sale
                        {
                            SellerUserName = sellerUserName,
                            BuyerUserName = ru.UserName,
                            DateBought = DateTime.Today,
                            PriceBought = starsToDecrease,
                            ProductId = productId,


                        };
                        this.context.AddSale(s);
                        HttpContext.Session.SetObject("sale", s);
                        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                        context.SaveChanges();
                        //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                        return true;
                    }
                    else
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                        return false;
                    }
                    //add sales to seller sales 

                    //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!

                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return false; 
        }
        [Route("UpdateProduct")]
        [HttpPost]

        public bool UpdateProduct([FromBody] Product p)
        {

            if (p != null)
            {
                try
                {

                    this.context.UpdateProduct(p);
                    HttpContext.Session.SetObject("theUser", p);
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    context.SaveChanges();
                    //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                    return true;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    return false; 
                }
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }

        }
        [Route("UpdateSeller")]
        [HttpPost]

        public bool UpdateSeller([FromBody] Seller s)
        {
            if (s != null)
            {
                this.context.UpdateSeller(s);
                HttpContext.Session.SetObject("theUser", s);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                context.SaveChanges();
                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return true;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }



        [Route("UpdateUser")]
        [HttpPost]

        public bool UpdateUser([FromBody] RegularUser ru)
        {
            if (ru != null)
            {
                this.context.UpdateUser(ru);
                HttpContext.Session.SetObject("theUser", ru);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                context.SaveChanges();
                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return true;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }
        [Route("GetEF")]
        [HttpGet]
        public double GetEF(string country)
        {
            return context.Countries.Where(c => c.CountryName.Equals(country)).FirstOrDefault().Ef; 

        }

        [Route("CalculateCarbonFootprint")]
        [HttpPost]

        //calculates the carbon footprint of the user and the goals for his carbon footprint

        private double CalculateCarbonFootprint(RegularUser user)
        {
            double userFootprint = 0;
            userFootprint += user.InitialMeatsMeals * Constants.MEAT_EMISSION_FACTOR;
            userFootprint += user.DistanceToWork * Constants.DAYS_A_WEEK * 2 * Constants.AVERAGE_CAR_EMISSION;


            return 0; 
        }
        [Route("AddData")]
        [HttpPost]

        public bool AddUserData([FromBody] UsersDatum data, [FromQuery] double ef)
        {
            if (data != null)
            {
                switch(data.CategoryId)
                {
                    case 1:
                        data.CarbonFootprint = data.CategoryValue * Constants.MEAT_EMISSION_FACTOR;
                        break;
                    case 2:
                        data.CarbonFootprint = data.CategoryValue * Constants.AVERAGE_CAR_EMISSION;
                        break;
                    case 3:
                        data.CarbonFootprint = data.CategoryValue * ef;
                        break;
                    default:
                        data.CarbonFootprint = 0;
                        break; 


                }
                this.context.AddData(data);
                HttpContext.Session.SetObject("theData", data);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                context.SaveChanges();
                return true;
            }

            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }

        [Route ("AddProduct")]
        [HttpPost]
        public Product AddProduct([FromBody] Product p)
        {

            if (p != null)
            {
                this.context.AddProduct(p);
                HttpContext.Session.SetObject("theProduct", p);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                context.SaveChanges();
                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return p;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }


        [Route("RegisterUser")]
        [HttpPost]

        public RegularUser RegisterUser([FromBody] RegularUser u)
        {

            if (u != null)
            {
                double carbonFootprint = 0;
                carbonFootprint += Constants.AVERAGE_CAR_EMISSION * Constants.WORK_DAYS * 2 * u.DistanceToWork;
                carbonFootprint += Constants.MEAT_EMISSION_FACTOR * Constants.DAYS_A_WEEK * u.InitialMeatsMeals;
                carbonFootprint += u.LastElectricityBill / 4 * GetEF(u.UserNameNavigation.Country);
                //u.UserCarebonFootprint = carbonFootprint; 
                        this.context.AddRegularUser(u);
                HttpContext.Session.SetObject("theUser", u);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                context.SaveChanges();
                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return u;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("RegisterBusinessOwner")]
        [HttpPost]
        public Seller RegisterBusinessOwner([FromBody] Seller u)
        {


            //Check user name and password
            if (u != null)
            {
                this.context.AddSeller(u);
                HttpContext.Session.SetObject("theUser", u);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                context.SaveChanges();
                //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                return u;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("GetProducts")]
        [HttpGet]
        //returns only active products
        public List<Product> GetProducts()
        {
            return context.Products.Where(p => p.Active == true).ToList<Product>(); 
        }

        [Route("GetCountries")]
        [HttpGet]

        public List<Country> GetCountries()
        {
            
                return context.Countries.ToList<Country>();

          

        }

        [Route("GetUserData")]
        [HttpGet]

        public List<UsersDatum> GetUserData(int categoryId, string userName)
        {
            User u = HttpContext.Session.GetObject<User>("theUser");
            if (u != null)
            {
                DateTime today = DateTime.Today;
                List<UsersDatum> usersData =  context.UsersData.Where(d => d.CategoryId == categoryId && (today - d.DateT).TotalDays < 31
                 && d.UserName.Equals(userName)).ToList();
                 
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
            return null;
        }


        [Route("IsUserNameExist")]
        [HttpGet]
        public Boolean IsUserNameExist([FromQuery] string userName)
        {
            //If username is null the request is bad
            if (userName == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return true;
            }
            foreach (User u in context.Users)
            {
                if (u.UserName.Equals(userName))
                    return true;
            }
            return false;


        }

        [Route("IsRegularUser")]
        [HttpGet]

        public Boolean IsRegularUser([FromQuery] string userName)
        {
            //If username is null the request is bad
            if (userName == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return false;
            }
            foreach (RegularUser u in context.RegularUsers)
            {
                if (u.UserName.Equals(userName))
                    return true;
            }
            return false;

        }
        [Route("GetUserData")]
        [HttpGet]

        public User GetUserData([FromQuery] string userName)
        {
            //If username is null the request is bad
            if (userName == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }
            foreach (User u in context.Users)
            {
                if (u.UserName.Equals(userName))
                    return u;
            }
            return null;

        }

        [Route("GetCountryEF")]
        [HttpGet]

        public double GetCountryEF([FromQuery] string country)
        {
            try
            {
                return context.Countries.Where(c => c.CountryName.Equals(country)).FirstOrDefault().Ef;

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return -1; 
            }

        }
        [Route("GetSellerData")]
        [HttpGet]

        public Seller GetSellerData([FromQuery] string userName)
        {
            //If username is null the request is bad
            if (userName == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }
            foreach (Seller s in context.Sellers)
            {
                if (s.UserName.Equals(userName))
                    return s;
            }
            return null;

        }

        [Route ("GetSellerGraphsData")]
        [HttpGet]

        public List<GraphItem> GetSellerGraphsData([FromBody] ICollection<Sale> sales)
        {
            try
            {
                //calculate first day of each week
                DateTime today = DateTime.Today;

                DateTime firstWeekStartDay = today.AddDays(1 - (int)today.DayOfWeek);


                DateTime secondWeekStartDay = firstWeekStartDay.AddDays(-7);

                DateTime thirdWeekStartDay = secondWeekStartDay.AddDays(-7);

                DateTime fourthWeekStartDay = thirdWeekStartDay.AddDays(-7);

                //get the data of each week
                List<Sale> firstWeek = sales.Where(s => (s.DateBought - firstWeekStartDay).TotalDays < 7 && firstWeekStartDay.DayOfWeek <= s.DateBought.DayOfWeek
                ).ToList();

                List<Sale> secondWeek = sales.Where(s => (s.DateBought - secondWeekStartDay).TotalDays < 7 && secondWeekStartDay.DayOfWeek <= s.DateBought.DayOfWeek
                ).ToList();

                List<Sale> thirdWeek = sales.Where(s => (s.DateBought - thirdWeekStartDay).TotalDays < 7 && thirdWeekStartDay.DayOfWeek <= s.DateBought.DayOfWeek
                ).ToList();

                List<Sale> fourthWeek = sales.Where(s => (s.DateBought - fourthWeekStartDay).TotalDays < 7 && fourthWeekStartDay.DayOfWeek <= s.DateBought.DayOfWeek
                ).ToList();
                //calculate the sum of the carbon footprint

                double footprintSum = 0;
                foreach (Sale s in firstWeek)
                {
                    double inSehkels = (double)s.PriceBought / 10;
                    double inCO2 = (inSehkels / 5) * 7560;
                    footprintSum += inCO2;
                }

                GraphItem first = new GraphItem
                {
                    DateGraph = firstWeekStartDay,
                    ValueFootPrint = footprintSum,
                };

                footprintSum = 0;
                foreach (Sale s in secondWeek)
                {
                    double inSehkels = (double)s.PriceBought / 10;
                    double inCO2 = (inSehkels / 5) * 7560;
                    footprintSum += inCO2;
                }

                GraphItem second = new GraphItem
                {
                    DateGraph = secondWeekStartDay,
                    ValueFootPrint = footprintSum,
                };

                footprintSum = 0;
                foreach (Sale s in thirdWeek)
                {
                    double inSehkels = (double)s.PriceBought / 10;
                    double inCO2 = (inSehkels / 5) * 7560;
                    footprintSum += inCO2;
                }

                GraphItem third = new GraphItem
                {
                    DateGraph = thirdWeekStartDay,
                    ValueFootPrint = footprintSum,
                };

                footprintSum = 0;
                foreach (Sale s in fourthWeek)
                {
                    double inSehkels = (double)s.PriceBought / 10;
                    double inCO2 = (inSehkels / 5) * 7560;
                    footprintSum += inCO2;
                }

                GraphItem fourth = new GraphItem
                {
                    DateGraph = fourthWeekStartDay,
                    ValueFootPrint = footprintSum,
                };



                List<GraphItem> graphItems = new List<GraphItem>();
                graphItems.Add(first);
                graphItems.Add(second);
                graphItems.Add(third);
                graphItems.Add(fourth);

                //return a list of every week carbon footprint data
                return graphItems;

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return null; 
            }



        }

        //[Route("CalcCarbonFootprint")]
        //[HttpGet]
        private void CalcCarbonFootprint(UsersDatum u, double ef)
        {
            if (u != null)
            {
                switch (u.CategoryId)
                {
                    case 1:
                        u.CarbonFootprint = u.CategoryValue * Constants.MEAT_EMISSION_FACTOR;
                        break;
                    case 2:
                        u.CarbonFootprint = u.CategoryValue * Constants.AVERAGE_CAR_EMISSION;
                        break;
                    case 3:
                        u.CarbonFootprint = u.CategoryValue * ef;
                        break;
                    default:
                        u.CarbonFootprint = null;
                        break;


                }
                this.context.UpdateData(u);
                HttpContext.Session.SetObject("theData", u);
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                context.SaveChanges();
            }

            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            }
        }

        [Route ("GetUserGraphsData")]
        [HttpGet]
        public List<GraphItem> GetUserGraphsData([FromQuery] string userName)
        {
            try
            {
                //If username is null the request is bad
                if (userName == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return null;
                }
                //calculate first day of each week
                DateTime today = DateTime.Today;
               
                DateTime firstWeekStartDay = today.AddDays(-(int)today.DayOfWeek);
                    
                
                DateTime secondWeekStartDay = firstWeekStartDay.AddDays(-7);

                DateTime thirdWeekStartDay = secondWeekStartDay.AddDays(-7);

                DateTime fourthWeekStartDay = thirdWeekStartDay.AddDays(-7);

                //get the data of each week

                //לחבדוק בעצמי את התנאים
                List<UsersDatum> data = context.UsersData.Where(d => d.UserName.Equals(userName)).ToList();

                List<UsersDatum> firstWeek = new List<UsersDatum>(), secondWeek = new List<UsersDatum>(), thirdWeek = new List<UsersDatum>(), fourthWeek = new List<UsersDatum>();
              
                foreach(UsersDatum d in data)
                {
                    if (d.CarbonFootprint == null)
                    {
                        User u = context.Users.Where(u => u.UserName.Equals(d.UserName)).FirstOrDefault();
                        CalcCarbonFootprint(d, GetEF(u.Country)); 

                    }
                    if ((d.DateT - firstWeekStartDay).TotalDays < 7 && (d.DateT - firstWeekStartDay).TotalDays >= 0)
                        firstWeek.Add(d);
                    if ((d.DateT - secondWeekStartDay).TotalDays < 7 && (d.DateT - secondWeekStartDay).TotalDays >= 0)
                        secondWeek.Add(d);
                    if ((d.DateT - thirdWeekStartDay).TotalDays < 7 && (d.DateT - thirdWeekStartDay).TotalDays >= 0)
                        thirdWeek.Add(d);
                    if ((d.DateT - fourthWeekStartDay).TotalDays < 7 && (d.DateT - fourthWeekStartDay).TotalDays >= 0)
                        fourthWeek.Add(d);
                }
                //calculate the sum of the carbon footprint

               double footprintSum = 0; 
               foreach (UsersDatum ud in firstWeek)
               {
                    footprintSum += (double)ud.CarbonFootprint; 
               }

                GraphItem first = new GraphItem
                {
                    DateGraph = firstWeekStartDay.Date,
                    ValueFootPrint = footprintSum,
                };

                footprintSum = 0; 
                foreach (UsersDatum ud in secondWeek)
                {
                    if (ud.CarbonFootprint != null)
                        footprintSum += (double)ud.CarbonFootprint;
                }

                GraphItem second = new GraphItem
                {
                    DateGraph = secondWeekStartDay.Date,
                    ValueFootPrint = footprintSum,
                };

                footprintSum = 0;
                foreach (UsersDatum ud in thirdWeek)
                {
                    if(ud.CarbonFootprint != null)
                        footprintSum += (double)ud.CarbonFootprint;

                }

                GraphItem third = new GraphItem
                {
                    DateGraph = thirdWeekStartDay.Date,
                    ValueFootPrint = footprintSum,
                };

                footprintSum = 0;
                foreach (UsersDatum ud in fourthWeek)
                {
                    footprintSum += (double)ud.CarbonFootprint;
                }

                GraphItem fourth = new GraphItem
                {
                    DateGraph = fourthWeekStartDay.Date,
                    ValueFootPrint = footprintSum,
                };



                List<GraphItem> graphItems = new List<GraphItem>();
                graphItems.Add(first);
                graphItems.Add(second);
                graphItems.Add(third);
                graphItems.Add(fourth);

                //return a list of every week carbon footprint data
                return graphItems; 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null; 
            }



        }


        [Route("GetRegularUserData")]
        [HttpGet]

        public RegularUser GetRegularUserData([FromQuery] string userName)
        {
            //If username is null the request is bad
            if (userName == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return null;
            }
            foreach (RegularUser u in context.RegularUsers)
            {
                if (u.UserName.Equals(userName))
                    return u;
            }
            return null;

        }

        [Route("IsDataExist")]
        [HttpGet]
        public bool IsDataExist([FromQuery] int categoryId, string userName)
        {
            //IQueryable<UsersDatum> list = context.UsersData.Where(d => d.CategoryId == categoryId && d.UserName.Equals(userName));
            //if (list == null)
            //    return false;
            if (context.UsersData.Where(d => d.CategoryId == categoryId && d.UserName.Equals(userName)).FirstOrDefault() == null)
                return false;
            if (categoryId > 0)
            {
                DateTime today = DateTime.Today;
                DateTime then = context.UsersData
                    .Where(d => d.CategoryId == categoryId && d.UserName.Equals(userName))
                    .FirstOrDefault().DateT;
                TimeSpan ts = today.Subtract(then);
                if (ts.Days >= 7)
                    return false;
                if ((int)today.DayOfWeek < (int)then.DayOfWeek)
                    return false;
                else
                    return true; 
            }
            return false; 
        }

        [Route("GetCategoryId")]
        [HttpGet]
        public int GetCategoryId([FromQuery] string category)
        {
            if (category != null)
            {
                return context.DatasCategories
                    .Where(d => d.CategoryName.Equals(category)).FirstOrDefault().CategoryId;
            }
            else
                return - 1;
        }

    [Route("IsEmailExist")]
        [HttpGet]
        public Boolean IsEmailExist([FromQuery] string email)
        {
            //If email is null the request is bad
            if (email == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return true;
            }
            foreach (User u in context.Users)
            {
                if (u.Email.Equals(email))
                    return true;
            }
            return false;


        }

        [Route("Login")]
        [HttpGet]
        public User Login([FromQuery] string email, [FromQuery] string pass)
        {
            User user = context.Login(email, pass);

            try
            {
                //Check user name and password
                if (user != null)
                {
                    HttpContext.Session.SetObject("theUser", user);

                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                    //Important! Due to the Lazy Loading, the user will be returned with all of its contects!!
                    return user;
                }


                else
                {

                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

    }
}
