using ResponsiBoss.Api.Models;
using ResponsiBoss.Api.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Api.Client.Abstractions
{
    public interface IAppointmentClient
    {
        Task<Guid> CreateAppointmentAsync(CreateAppointmentModel createAppointmentModel);
        Task<List<AppointmentModel>> GetAppointmentsAsync();
        Task<AppointmentModel> GetAppointmentAsync(Guid appointmentId);
    }
}