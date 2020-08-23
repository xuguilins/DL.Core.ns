using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyTestController : ControllerBase
    {
        /// <summary>
        /// 获取结果集合
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetActionResult")]
        public IActionResult GetActionResult()
        {
            return new JsonResult("abc");
        }
    }
}