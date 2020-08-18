using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Shifts.Models;

namespace Shifts.Controllers
{
    public class WorkerController : ApiController
    {
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(WorkerDB.GetAllWorkers());
            }
            catch (Exception ex)
            {
                return BadRequest("could not get all the Workers ! \n  --" + ex.Message);
            }
        }

        public Worker Get(string email, string password)
        {
            return WorkerDB.GetWorkerByEmailAndPassword(email, password);
        }

        public IHttpActionResult Post([FromBody]Worker val)
        {
            try
            {
                Worker res = WorkerDB.InsertWorkerToDb(val);
                if (res == null)
                {
                    return Content(HttpStatusCode.BadRequest, $"could not insert Worker {val.ToString()} or already exists!");
                }
                return Created(new Uri(Url.Link("GetWorkerById", new { id = res.WorkerID })), res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }

        public IHttpActionResult Put(Worker Worker2Update)
        {
            try
            {
                    int res = WorkerDB.UpdateWorker(Worker2Update);
                    if (res == 1)
                    {
                        return Ok();
                    }
                    return Content(HttpStatusCode.NotModified, $"Worker with id {Worker2Update.WorkerID} exsits but could not be modified!!!");
                            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

    }
}