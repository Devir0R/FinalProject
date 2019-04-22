using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Logic_Layer;

namespace webapi.Controllers
{
    public class NotificationsSettingController : ApiController
    {
        readonly ILogic Logic = new Logic();

        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, "controlled exists!");
        }

        public HttpResponseMessage Get(string deviceID)
        {
            return Request.CreateResponse(HttpStatusCode.Accepted, Logic.GetNotificationsSettingByID(deviceID));
        }

        [HttpPut]
        public HttpResponseMessage Put(string deviceID, int red = 0, int yellow = 0, int assists = 0, int goals = 0, int sheets = 0,int apps=0)
        {
            try
            {
                Logic.UpdateSetting(deviceID, red, yellow, assists, goals, sheets,apps);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
