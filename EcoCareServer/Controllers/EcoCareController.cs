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
    }
}
