using EcoCareServerBL.Models;
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
