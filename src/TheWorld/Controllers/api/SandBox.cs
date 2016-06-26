using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Controllers.api
{
    public class SandBox : Controller
    {
        [HttpGet("api/sandbox")]
        public JsonResult Get()
        {
            return Json(new { Message = "This is a test api method" });
        }
    }
}
