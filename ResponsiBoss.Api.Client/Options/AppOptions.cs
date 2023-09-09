using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Api.Client.Options
{
    public class AppOptions
    {
        public string ApiBaseUrl { get; set; }
        public bool AllowUntrustedCert { get; set; }
    }
}