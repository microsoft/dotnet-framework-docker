using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace aspnetmvcapp.Controllers
{
    public class ValuesController : ApiController
    {
        public int[] GetValues()
        {
            var values = new int[] { 1, 2, 3, 4 };
            return values;
        }
    }
}
