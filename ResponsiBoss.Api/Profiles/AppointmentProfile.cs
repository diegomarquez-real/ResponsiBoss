using AutoMapper;

namespace ResponsiBoss.Api.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Data.Models.Appointment, Models.AppointmentModel>()
                .ForMember(am => am.Feature, opt => opt.MapFrom(dm => dm.Location));
            CreateMap<Models.Create.CreateAppointmentModel, Data.Models.Appointment>();
            CreateMap<Models.Update.UpdateAppointmentModel, Data.Models.Appointment>();
        }
    }
}