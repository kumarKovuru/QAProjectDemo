using emids.QA.Application.Common.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace emids.QA.Application.Test
{
    public class SystemConfig
    {
        public IApplicationConfiguration _appConfig;
        public SystemConfig()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _appConfig = new ApplicationConfiguration()
            {
                DatabaseConnectionString = config["DatabaseConnectionString"],
                WebAPIUrl = config["WebAPIUrl"]
            };
        }
    }
}
