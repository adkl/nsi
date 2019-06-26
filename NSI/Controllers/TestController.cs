using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NSI.Api.Controllers
{
    [Authorization.NsiAuthorization]
    public class TestController : ApiController
    {
        [HttpGet]
        public IHttpActionResult TestAction()
        {
            return Ok(new {
                testKey = "TODO"
            });

            
        }
    }
}
