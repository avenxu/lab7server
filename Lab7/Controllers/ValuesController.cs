using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab7.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        // Get /uid/payload
        [HttpGet("{uid}/{payload}")]
        public IActionResult Get(string uid, int payload)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var response = new Response(uid);
            try
            {
                if (payload > 100000 || payload < 10) return BadRequest(response);
                response.Payload = new string('*', payload / 2);
                stopwatch.Stop();
                var time = stopwatch.ElapsedMilliseconds;
                response.Delay = time;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Payload = ex.Message;
                return NotFound(response);
            }

        }
    }

    public class Response
    {
        public Response(string uid)
        {
            Uid = uid;
        }
        public string Payload { get; set; }
        public long Delay { get; set; }
        public string Uid { get; set; }

    }
}