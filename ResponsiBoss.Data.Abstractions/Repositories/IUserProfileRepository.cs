using ResponsiBoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data.Abstractions
{
    public interface IUserProfileRepository : IGenericRepository<UserProfile, Guid>
    {
        Task<UserProfile> FindByEmailAsync(string email);
    }
}