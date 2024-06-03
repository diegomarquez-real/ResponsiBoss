using AutoMapper;

namespace ResponsiBoss.Api.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Data.Models.Appointment, Models.AppointmentModel>();
            CreateMap<Models.Create.CreateAppointmentModel, Data.Models.Appointment>();
            CreateMap<Models.Update.UpdateAppointmentModel, Data.Models.Appointment>();
        }
    }
}