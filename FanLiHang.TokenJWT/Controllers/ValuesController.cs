﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace tokenJWT.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        [Authorize("Bearer")]
        public string Get()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var id = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "ID").Value;
            return $"Hello! {HttpContext.User.Identity.Name}, your ID is:{id}";
        }


    }
}
