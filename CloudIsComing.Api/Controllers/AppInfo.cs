using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CloudIsComing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppInfoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AppInfoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public AppInfo GetAppInfo()
        {
            var appInfo = new AppInfo(_configuration);
            return appInfo;
        }
    }

    public class AppInfo
    {
        private IConfiguration Configuration { get; }
        public string MachineName { get; }
        public string Version { get; }

        public AppInfo(IConfiguration configuration)
        {
            Configuration = configuration;
            MachineName = Environment.MachineName;
            Version = Configuration["Version"];
        }
    }
}
