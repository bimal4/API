using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace TestJreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppSettings _mySettings;

        public HomeController(IOptions<AppSettings> settings)
        {
            //This is always null
            _mySettings = settings.Value;
        }
    }
}