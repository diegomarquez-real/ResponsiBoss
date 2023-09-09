using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Api.Client.Abstractions
{
    public interface IApiClientSettings
    {
        string BaseUrl { get; }

        IAuthTokenProvider AuthTokenProvider { get; }

        /// <summary>
        /// Specifies Whether Or Not The HttpClient Will Require A Valid SSL Cert. 
        /// Setting To True Will Allow Self-Signed, Untrusted, And Expired Certificates.
        /// </summary>
        bool AllowUntrustedCert { get; }
    }
}