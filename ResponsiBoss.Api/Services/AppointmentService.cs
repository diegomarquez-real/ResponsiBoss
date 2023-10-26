using AutoMapper;
using ResponsiBoss.Api.Models;
using ResponsiBoss.Api.Models.Create;
using ResponsiBoss.Api.Models.Update;
using ResponsiBoss.Data.Abstractions;
using ResponsiBoss.Data.Models;

namespace ResponsiBoss.Api.Services
{
    public class AppointmentService : Abstractions.IAppointmentService
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IMapper mapper,
            IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentModel> FindByIdAsync(Guid appointmentId)
        {
            var appointment = await _appointmentRepository.FindByIdAsync(appointmentId);

            var appointmentModel = _mapper.Map<Appointment, AppointmentModel>(appointment);

            return appointmentModel;
        }

        public async Task<List<AppointmentModel>> GetAppointmentsAsync()
        {
            var appointments = await _appointmentRepository.GetAllAsync();

            var appointmentModels = _mapper.Map<IEnumerable<Appointment>, List<AppointmentModel>>(appointments);

            return appointmentModels;
        }

        public async Task<Guid> CreateAppointmentAsync(CreateAppointmentModel createAppointmentModel)
        {
            var appointment = _mapper.Map<CreateAppointmentModel, Appointment>(createAppointmentModel);

            var result = await _appointmentRepository.CreateAsync(appointment);

            return result.AppointmentId;
        }

        public async Task UpdateAppointmentAsync(AppointmentModel appointmentModel, UpdateAppointmentModel updateAppointmentModel)
        {
            var appointment = _mapper.Map<UpdateAppointmentModel, Appointment>(updateAppointmentModel);
            appointment.AppointmentId = appointmentModel.AppointmentId;
            appointment.CreatedBy = appointmentModel.CreatedBy;
            appointment.CreatedOn = appointmentModel.CreatedOn;

            await _appointmentRepository.UpdateAsync(appointment);
        }

        public async Task DeleteAppointmentAsync(Guid appointmentId)
        {
            await _appointmentRepository.DeleteAsync(appointmentId);
        }
    }
}