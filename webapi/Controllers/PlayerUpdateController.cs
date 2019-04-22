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
            CPlayerUpdate pa = new CPlayerUpdate
            {
                Club = "P.S.G",
                Competitions = new List<string>()
                {
                    "LigaBBVA",
                    "PremierLeague"
                },
                DOB = DateTime.Now,
                FormationPosition = 11,
                Injury = new CAthleteInjuryUpdate(),
                Suspension = new CAthleteSuspensionUpdate(),
                JerseyNum = 55,
                Message = "hey!",
                Name = "Daddy",
                Nationality = "German",
                Position = 15,
                Statistics = new List<CAthleteStatisticsUpdate>()
                {
                    new CAthleteStatisticsUpdate()
                    {
                        Competition = "hey",
                        Country = "hey",
                        Season = 2012,
                        Stats = null
                    }
                }
            };
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

/*
 {"Name":"Daddy",
 "Competitions":[],
 "Club":"P.S.G",
 "Nationality":"German",
 "JerseyNum":55,
 "Position":15,
 "FormationPosition":11,
 "DOB":"2019-04-22T16:26:48.3755968+03:00",
 "Statistics":[],
 "Injury":
    {
    "Reason":null,
    "StartDate":"0001-01-01T00:00:00",
    "ExceptedReturn":null,
    "LastUpdate":"0001-01-01T00:00:00",
    "Active":true
    },
    "Suspension":
        {
        "SuspensionType":0,
        "Country":null,
        "Competition":null,
        "StartDate":"0001-01-01T00:00:00",
        "GamesCount":0,
        "GamesBannedLeft":0,
        "Active":true
        },
    "Message":"hey!"
  }
    
    */

