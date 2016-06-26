using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Controllers.api
{
    [Route("api/trips/{tripname}/stops")]
    public class Stop :Controller
    {
        private ILogger _logger;
        private IWorldRepository _repo;
        private CoordService cooRdService;

        public Stop(IWorldRepository repo, ILogger logger,CoordService coo)
        {
            _repo = repo;
            _logger = logger;
            cooRdService = coo;
        }
        [HttpGet]
        public JsonResult Get(string tripName)
        {
            try
            {
                var results = _repo.GetTripByName(tripName);
                if(results == null)
                {
                    return Json(null);
                }
                return Json(AutoMapper.Mapper.Map<IEnumerable<StopViewModel>>(results.Stops));
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to get stop for trip", e);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = "Error occured finding trip name" });
            }
        }

        public async Task<JsonResult> Post(string tripName, [FromBody]StopViewModel stop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Map to e
                    TheWorld.Models.Stop newStop = AutoMapper.Mapper.Map<TheWorld.Models.Stop>(stop);
                    var coordResult = await cooRdService.Lookup(stop.Name);
                    _repo.AddStop(tripName,newStop);

                    if (!coordResult.Success)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(new { Message = coordResult.Message});
                    }

                    newStop.Longitude = coordResult.Longitude;
                    newStop.Latitude = coordResult.Latitude;

                    if (_repo.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(AutoMapper.Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogError("Failed to save new stop", e);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = "Failed to save new stop" });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed to save new stop" });
        }
    }
}
