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
        Task<UserProfile> FindByIdAsync(Guid id);
        Task<IEnumerable<UserProfile>> GetAllAsync();
        Task<Guid> CreateAsync(UserProfile entity);
        Task<Guid> DeleteAsync(Guid id);
        Task<Guid> UpdateAsync(UserProfile entity);
    }
}