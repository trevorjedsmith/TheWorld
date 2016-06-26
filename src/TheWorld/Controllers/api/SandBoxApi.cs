using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Controllers.api
{
    public class SandBoxApi : Controller
    {
        [HttpGet]
        public JsonResult Get()
        {
            return Json(new { Message = "Successful Get()" });
        }
    }
}
