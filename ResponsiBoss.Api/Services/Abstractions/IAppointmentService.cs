using ResponsiBoss.Api.Models;
using ResponsiBoss.Api.Models.Create;
using ResponsiBoss.Api.Models.Update;

namespace ResponsiBoss.Api.Services.Abstractions
{
    public interface IAppointmentService
    {
        Task<AppointmentModel> FindByIdAsync(Guid appointmentId);
        Task<List<AppointmentModel>> GetAppointmentsAsync();
        Task<Guid> CreateAppointmentAsync(CreateAppointmentModel createAppointmentModel);
        Task UpdateAppointmentAsync(AppointmentModel appointmentModel, UpdateAppointmentModel updateAppointmentModel);
        Task DeleteAppointmentAsync(Guid appointmentId);
    }
}