using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Logic_Layer;
using webapi.Utilities;

namespace webapi.Controllers
{
    public class UsersController : ApiController
    {
        public static readonly string NOT_FOUND_MSG = "User with id = {0} not found";
        readonly ILogic Logic = new Logic();

        [HttpPost]
        public HttpResponseMessage Post([FromBody] UserFromNW user)
        {
            string deviceID = user.deviceID;
            bool success = Logic.AddUser(deviceID);
            if (success)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Added");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed");
            }

        }

        [HttpGet]
        public HttpResponseMessage Get(int Id)
        {
            Users obj = Logic.GetUserByID(Id);
            if (obj != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, String.Format(NOT_FOUND_MSG, obj));

            }
        }


        [HttpGet]
        public HttpResponseMessage GetUserPlayers(string deviceID)
        {
            List<Players> userPlayers = Logic.GetUserPlayers(deviceID);
            return Request.CreateResponse(HttpStatusCode.OK, userPlayers);
        }

    }
}
