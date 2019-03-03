using PlayersDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webapi.Controllers
{
    public class PlayersController : ApiController
    {

        public static readonly string NOT_FOUND_MSG = "Player with id = {0} not found";

        [HttpGet]
        public HttpResponseMessage Get(string search="")
        {
            using (projDBEntities entities = new projDBEntities())
            {
                List<Players> ret = new List<Players>();
                foreach(Players p in entities.Players)
                {
                    string fullName = (p.name + "-" + p.last_name).ToLower();
                    if (fullName.Contains(search.ToLower()))
                    {
                        ret.Add(ClonePlayers(p));
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK,ret);
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(int Id)
        {
            using (projDBEntities entities = new projDBEntities())
            {
                Players obj = entities.Players.FirstOrDefault(e => e.player_Id == Id);
                if (obj != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, ClonePlayers( obj));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, String.Format(NOT_FOUND_MSG,obj));
                }
                //return new Players(obj);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Players value)
        {
            try
            {
                using (projDBEntities entities = new projDBEntities())
                {
                    entities.Players.Add(value);
                    entities.SaveChanges();

                    var msg = Request.CreateResponse(HttpStatusCode.Created, value);
                    msg.Headers.Location = new Uri(Request.RequestUri
                        + (Request.RequestUri.ToString().EndsWith("/") ? "" : "/")
                        + value.player_Id.ToString());
                    return msg;
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (projDBEntities entities = new projDBEntities())
                {
                    var entity = entities.Players.FirstOrDefault(e => e.player_Id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(NOT_FOUND_MSG, id));
                    }
                    else
                    {
                        entities.Players.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public void Put(int id,[FromBody]Players player)
        {
            using (projDBEntities entities = new projDBEntities())
            {
                var entity = entities.Players.FirstOrDefault(e=>e.player_Id==id);
                Update(entity,player);
            }

        }

        public void Update(Players e,Players obj)
        {
            e.name = obj.name;
            e.last_name = obj.last_name;
            e.club = obj.club;
            e.nationality = obj.nationality;
            e.in_game = obj.in_game;
            e.position = obj.position;
        }

        public Players ClonePlayers(Players obj)
        {
            Players ret = new Players
            {
                player_Id = obj.player_Id,
                name = obj.name.Trim(),
                last_name = obj.last_name.Trim(),
                club = obj.club.Trim(),
                nationality = obj.nationality.Trim(),
                in_game = obj.in_game,
                position = obj.position.Trim(),
                Users = obj.Users,
                CompetitionStatistics = obj.CompetitionStatistics
            };
            return ret;
        }


    }
}
