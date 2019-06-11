using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webapi.Logic_Layer;

namespace webapi.Controllers
{
    public class PlayersController : ApiController
    {

        public static readonly string NOT_FOUND_MSG = "Player with id = {0} not found";
        private ILogic Logic = new Logic();

        [HttpGet]
        public HttpResponseMessage Get(string top = "",string keyword = "", string club  = "",int ageup=120,int agedown=0,string nationality="",string position="",string league="")
        {
            List<Players> ret;
            if (top == "")
            {
                string fullName = keyword.ToLower();
                string clubName = club==null? "" : club.ToLower();

                ret = Logic.Search(fullName, clubName, ageup, agedown, nationality, position,league);
                
            }
            else
            {
                if (Int32.TryParse(top, out int res))
                {
                    ret = Logic.GetTopPlayers(res);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, ret);
        }

        [HttpGet]
        public HttpResponseMessage Get(int Id)
        {
            using (projDBEntities entities = new projDBEntities())
            {
                Players obj = Logic.GetPlayerByID(Id);
                if (obj != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,obj);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, String.Format(NOT_FOUND_MSG,obj));
                }
            }
        }

        
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

        [HttpPut]
        public HttpResponseMessage Put(int id,[FromBody]Players player)
        {
            try
            {
                using (projDBEntities entities = new projDBEntities())
                {
                    var entity = entities.Players.FirstOrDefault(e => e.player_Id == id);
                    Update(entity, player);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public void Update(Players e,Players obj)
        {
            e.name = obj.name;
            e.club = obj.club;
            e.nationality = obj.nationality;
            e.in_game = obj.in_game;
            e.position = obj.position;
        }


    }
}
