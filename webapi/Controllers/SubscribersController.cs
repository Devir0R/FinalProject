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
        public HttpResponseMessage Post(SubscriberFromNW sub)
        {
            string deviceID = sub.deviceID;
            int playerID = sub.playerID;
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

        [HttpDelete]
        public HttpResponseMessage Delete(SubscriberFromNW sub)
        {
            string deviceID = sub.deviceID;
            int playerID = sub.playerID;
            bool success = Logic.UnSubscribe(deviceID, playerID);
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
