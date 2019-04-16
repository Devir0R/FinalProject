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
    public class SubscribersController : ApiController
    {
        readonly ILogic Logic = new Logic();

        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, new SubscriberFromNW() { deviceID ="EE",playerID=15});

        }

        [HttpPost]
        public HttpResponseMessage Post(SubscriberFromNW user)
        {
            string deviceID = user.deviceID;
            int playerID = user.playerID;
            bool success = Logic.Subscribe(deviceID, playerID);
            if (success)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Added");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed");
            }
        }

    }
}
