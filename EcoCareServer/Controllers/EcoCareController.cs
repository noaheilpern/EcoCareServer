﻿using EcoCareServerBL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoCareServer.Controllers
{
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

        [Route("AddData")]
        [HttpPost]

        public bool AddUserData([FromBody] UsersDatum data)
        {
            if (data != null)
            {
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

        [Route("RegisterUser")]
        [HttpPost]

        public RegularUser RegisterUser([FromBody] RegularUser u)
        {

            if (u != null)
            {
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

        [Route("GetCountries")]
        [HttpGet]

        public List<Country> GetCountries()
        {
            
                return context.Countries.ToList<Country>();

          

        }

        [Route("GetUserData")]
        [HttpGet]

        public List<UsersDatum> GetData(int categoryId, string userName)
        {
            User u = HttpContext.Session.GetObject<User>("theUser");
            if (u != null)
            {
                DateTime today = DateTime.Today;
                return context.UsersData.Where(d => d.CategoryId == categoryId && (today - d.DateT).TotalDays < 31
                 && d.UserName.Equals(userName)).ToList();
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
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
