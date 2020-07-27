using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Person.API.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/people")]
    [ApiController]
    public class PersonController : ControllerBase
    {
    }
}
