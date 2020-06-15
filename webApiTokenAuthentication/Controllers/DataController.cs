using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;


namespace webApiTokenAuthentication.Controllers
{
    [RoutePrefix("api")]
    public class DataController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("data/forall")]
       public IHttpActionResult Get()
        {
            return Ok("Now Server time is:" + System.DateTime.Now.ToString());
        }
        [Authorize]
        [HttpGet]
        [Route("data/Authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello " + identity.Name);
        }
        [Authorize(Roles ="admin")]
        [HttpGet]
        [Route("data/Authorized")]
        public IHttpActionResult GetForAuthorized()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var role = identity.Claims.
                Where(a => a.Type == ClaimTypes.Role)
                .Select(a=>a.Value);
            return Ok("Hello " + identity.Name + "Role: "+ string.Join(",",role.ToList())) ;
        }

    }
}
