using System;
using System.Collections.Generic;
using System.Text;

namespace emids.QA.Application.Common.Config
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public string DatabaseConnectionString { get; set; }
        public string WebAPIUrl { get; set; }
    }
    public interface IApplicationConfiguration
    {
        string DatabaseConnectionString { get; set; }
        string WebAPIUrl { get; set; }
    }
}
