using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Logic_Layer;
using webapi.Players_Update.Updates;

namespace webapi.Controllers
{
    public class PlayerUpdateController : ApiController
    {
        readonly ILogic Logic = new Logic();

        [HttpGet]
        public HttpResponseMessage Get(int Id)
        {
            CPlayerUpdate pa = new CPlayerUpdate();
            pa.Club = "P.S.G";
            pa.Competitions = new List<string>();
            pa.DOB = DateTime.Now;
            pa.FormationPosition = 11;
            pa.Injury = new CAthleteInjuryUpdate();
            pa.Suspension = new CAthleteSuspensionUpdate();
            pa.JerseyNum = 55;
            pa.Message = "hey!";
            pa.Name = "Daddy";
            pa.Nationality = "German";
            pa.Position = 15;
            pa.Statistics = new List<CAthleteStatisticsUpdate>();
            return Request.CreateResponse(HttpStatusCode.OK, pa);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] CPlayerUpdate val)
        {
            string resp = Logic.UpdatePlayers(val);
            if(resp== "NOT FOUND")
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }
    }
}
