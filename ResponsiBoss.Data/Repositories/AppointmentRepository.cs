using Microsoft.Extensions.Logging;
using ResponsiBoss.Data.Abstractions;
using ResponsiBoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data
{
    public class AppointmentRepository : GenericRepository<Appointment, Guid>, Abstractions.IAppointmentRepository
    {
        public AppointmentRepository(IDataContext dataContext,
            IUserContext userContext,
            ILogger<UserProfileRepository> logger)
            : base(dataContext, userContext)
        {
        }
    }
}