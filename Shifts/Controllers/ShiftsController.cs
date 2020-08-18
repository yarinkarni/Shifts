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
    public class ShiftsController : ApiController
    {
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(ShiftsDB.GetAllShifts());
            }
            catch (Exception ex)
            {
                return BadRequest("could not get all the shifts ! \n  --" + ex.Message);
            }
        }
        public IHttpActionResult Post([FromBody]Shift val)
        {
            try
            {
                Shift res = ShiftsDB.InsertShiftToDb(val);
                if (res == null)
                {
                    return Content(HttpStatusCode.BadRequest, $"could not insert shifts {val.ToString()} or already exists!");
                }
                return Created(new Uri(Url.Link("GetShiftByDayAndName", new { day = res.Day,name = res.Name })), res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }
        public IHttpActionResult Put([FromBody] Shift s)
        {
            try
            {
               int res = ShiftsDB.UpdateShifts(s);
               if (res == 1)
               {
                 return Ok();
               }
               return Content(HttpStatusCode.NotModified, $"not update");          
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

    }
}
