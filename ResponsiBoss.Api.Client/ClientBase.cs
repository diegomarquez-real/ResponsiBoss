using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Api.Client
{
    public abstract class ClientBase
    {
        protected string _baseUrl;
        private readonly Abstractions.IAuthTokenProvider _authTokenProvider;
        private readonly ILogger _logger;
        private static bool _hasBeenSetup;

        public ClientBase(Abstractions.IApiClientSettings apiClientSettings, 
            ILogger logger)
        {
            _logger = logger;
            _baseUrl = apiClientSettings.BaseUrl;
            _authTokenProvider = apiClientSettings.AuthTokenProvider;

            if (!_hasBeenSetup)
            {
                FlurlHttp.GlobalSettings.BeforeCallAsync = x =>
                {
                    _logger?.LogDebug($"Begin Call To {x?.Request?.Url}.");

                    return Task.CompletedTask;
                };
                FlurlHttp.GlobalSettings.AfterCallAsync = x =>
                {
                    _logger?.LogDebug($"End Call To {x?.Request?.Url} Which Took {x?.Duration?.TotalSeconds} Seconds.");

                    return Task.CompletedTask;
                };

                FlurlHttp.GlobalSettings.OnError = x =>
                {
                    if (x.Response.StatusCode == 401) // Unauthorized Http Status Code
                    {
                        _authTokenProvider?.OnTokenExpired();
                    }
                };

                FlurlHttp.ConfigureClient(_baseUrl, cli =>
                {
                    if (apiClientSettings.AllowUntrustedCert)
                    {
                        cli.Settings.HttpClientFactory = new UntrustedCertClientFactory();
                    }
                });

                _hasBeenSetup = true;
            }
        }

        protected abstract string BaseSegment { get; }

        protected virtual Url UrlBuilderForAnonymous()
        {
            return _baseUrl.AppendPathSegment(this.BaseSegment);
        }

        protected virtual IFlurlRequest UrlBuilder()
        {
            var token = _authTokenProvider.GetAuthToken();

            return _baseUrl.AppendPathSegment(this.BaseSegment)
                           .WithOAuthBearerToken(token.Token);
        }

        protected virtual IFlurlRequest UrlBuilderForRecord(Guid recordId)
        {
            return UrlBuilder().AppendPathSegment(recordId);
        }
        protected virtual IFlurlRequest UrlBuilderForRecord(int recordId)
        {
            return UrlBuilder().AppendPathSegment(recordId);
        }

        protected virtual IFlurlRequest UrlBuilderForRecord(string recordId)
        {
            return UrlBuilder().AppendPathSegment(recordId);
        }
    }
}