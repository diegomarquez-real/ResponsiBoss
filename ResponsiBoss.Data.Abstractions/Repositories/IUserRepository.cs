using ResponsiBoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data.Abstractions
{
    public interface IUserRepository
    {
        Task<UserProfile> FindByIdAsync(Guid userId);
        Task<IEnumerable<UserProfile>> GetAllAsync();
        Task<Guid> CreateAsync(UserProfile user);
        Task<Guid> DeleteAsync(Guid userId);
        Task<Guid> UpdateAsync(UserProfile user);
    }
}