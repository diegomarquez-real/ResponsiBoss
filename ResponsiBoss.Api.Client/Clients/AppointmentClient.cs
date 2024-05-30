using Flurl.Http;
using Microsoft.Extensions.Logging;
using ResponsiBoss.Api.Models;
using ResponsiBoss.Api.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Api.Client
{
    public class AppointmentClient : ClientBase, Abstractions.IAppointmentClient
    {
        public AppointmentClient(Abstractions.IApiClientSettings apiClientSettings,
            ILogger<AppointmentClient> logger)
            :base(apiClientSettings, logger)
        {
        }

        protected override string BaseSegment => "Appointments";

        public async Task<Guid> CreateAppointmentAsync(CreateAppointmentModel createAppointmentModel)
        {
            return await UrlBuilder()
                .PostJsonAsync(createAppointmentModel)
                .ReceiveJson<Guid>();
        }

        public async Task<List<AppointmentModel>> GetAppointmentsAsync()
        {
            return await UrlBuilder()
                .GetJsonAsync<List<AppointmentModel>>();
        }

        public async Task<AppointmentModel> GetAppointmentAsync(Guid appointmentId)
        {
            return await UrlBuilderForRecord(appointmentId)
                .GetJsonAsync<AppointmentModel>();
        }
    }
}