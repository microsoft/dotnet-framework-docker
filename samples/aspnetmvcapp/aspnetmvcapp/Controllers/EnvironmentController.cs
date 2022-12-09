using System;
using System.Runtime.InteropServices;
using System.Web.Http;

namespace aspnetmvcapp.Controllers
{
    public class EnvironmentController : ApiController
    {
        public EnvironmentInfo GetEnvironment()
        {
            return new EnvironmentInfo();
        }
    }

    public class EnvironmentInfo
    {
        public EnvironmentInfo()
        {
            RuntimeVersion = RuntimeInformation.FrameworkDescription;
            OSVersion = RuntimeInformation.OSDescription;
            OSArchitecture = RuntimeInformation.OSArchitecture.ToString();
            ProcessorCount = Environment.ProcessorCount;
        }
        public string RuntimeVersion { get; set; }
        public string OSVersion { get; set; }
        public string OSArchitecture { get; set; }
        public int ProcessorCount { get; set; }
    }
}
