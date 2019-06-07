using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Updates;
using webapi.Logic_Layer;

namespace webapi.Controllers
{
    public class PlayerUpdateController : ApiController
    {
        readonly ILogic Logic = new Logic();

        [HttpGet]
        public HttpResponseMessage Get(int Id)
        {
            ParsableValue<string> league = new ParsableValue<string>("LigaBBVA");
            league.Parse(league.Value);
            CPlayerUpdate pa = new CPlayerUpdate()
            {
                Competition = "LigaBBVA",
                DOB = DateTime.Now,
                Competitor = "BBB",
                FormationPosition = 11,
                Injury = new CAthleteInjuryUpdate()
                {
                    Active = true,
                    AddedToGames = new List<long>(),
                    ExceptedReturn = "expected return",
                    LastUpdate = DateTime.Now,
                    Reason = "reason",
                    RemovedFromGames = new List<long>(),
                    StartDate = DateTime.Now
                },
                Suspension = new CAthleteSuspensionUpdate()
                {
                    Active = false,
                    StartDate = DateTime.Now,
                    Competition = "comp",
                    Country = "country",
                    GamesBannedLeft = 1,
                    GamesCount = 5,
                    SuspensionType = 5
                },
                JerseyNum = 55,
                Message = "hey!",
                Name = "Daddy",
                Nationality = "German",
                Position = 15,
                Statistics = new List<CAthleteStatisticsUpdate>()
                {
                    new CAthleteStatisticsUpdate()
                    {
                        Competition = league,
                        Country = new ParsableValue<int>(6),
                        Season = new ParsableValue<int>(2019),
                        
                        Stats = new List<CPlayerIndividualStat>()
                        {
                            new CPlayerIndividualStat()
                            {
                                StatisticType = 3,
                                Value = "9"
                            },
                             new CPlayerIndividualStat()
                            {
                                StatisticType = 2,
                                Value = "2"
                            },
                            new CPlayerIndividualStat()
                            {
                                StatisticType = 4,
                                Value = "22"
                            }
                        }
                    }
                }
            };
            return Request.CreateResponse(HttpStatusCode.OK, pa);
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] CPlayerUpdate val)
        {
            int resp = Logic.UpdatePlayers(val);
            if (resp>= 0)
            {
                return Request.CreateResponse(HttpStatusCode.Created,resp);
            }
            else
            {

            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}
