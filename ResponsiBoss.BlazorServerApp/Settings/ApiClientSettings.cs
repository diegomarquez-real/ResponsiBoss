using Microsoft.Extensions.Options;
using ResponsiBoss.Api.Client.Abstractions;
using ResponsiBoss.Api.Client.Options;

namespace ResponsiBoss.BlazorServerApp.Settings
{
    public class ApiClientSettings : IApiClientSettings
    {
        private readonly IAuthTokenProvider _authTokenProvider;
        private readonly IOptions<AppOptions> _appOptions;

        public ApiClientSettings(Api.Client.Abstractions.IAuthTokenProvider authTokenProvider,
            IOptions<AppOptions> appOptions)
        {
            _authTokenProvider = authTokenProvider;
            _appOptions = appOptions;
        }

        public string BaseUrl { get { return _appOptions.Value.ApiBaseUrl; } }
        public bool AllowUntrustedCert { get { return _appOptions.Value.AllowUntrustedCert; } }
        public Api.Client.Abstractions.IAuthTokenProvider AuthTokenProvider => _authTokenProvider;
    }
}