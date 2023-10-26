using ResponsiBoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data.Abstractions
{
    public interface IAppointmentRepository : IGenericRepository<Appointment, Guid>
    {
    }
}