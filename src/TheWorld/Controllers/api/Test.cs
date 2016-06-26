using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Controllers.api
{
    public class Test : Controller
    {
        [HttpGet("api/test")]
        public JsonResult Get()
        {
            return Json(new { Message = "Test Succeeded" });
        }
    }
}
