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

        [HttpPut]
        public HttpResponseMessage Put(int id, int red = 0, int yellow = 0, int assists = 0, int goals = 0, int sheets = 0)
        {
            try
            {
                Logic.UpdateSetting(id, red, yellow, assists, goals, sheets);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
