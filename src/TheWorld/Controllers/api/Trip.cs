using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.api
{
    [Route("api/mytrips")]
    public class Trips : Controller
    {
        private ILogger _logger;
        private IWorldRepository _worldRepository;

        public Trips(IWorldRepository wr, ILogger logger)
        {
            _worldRepository = wr;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var trips = AutoMapper.Mapper.Map<IEnumerable<TripViewModel>>(_worldRepository.GetAllTripsWithStops());
            return Json(trips);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel trip)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var newTrip = AutoMapper.Mapper.Map<TheWorld.Models.Trip>(trip);
                    _worldRepository.AddTrip(newTrip);
                    if (_worldRepository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(newTrip);
                    }

                }
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to save new trip", e);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Exception = e.Message });
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "There has been an error" });
        }
    }
}
