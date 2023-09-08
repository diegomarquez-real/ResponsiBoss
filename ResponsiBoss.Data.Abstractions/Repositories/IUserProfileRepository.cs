using ResponsiBoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data.Abstractions
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> FindByIdAsync(Guid userId);
        Task<UserProfile> FindByEmailAsync(string email);
        Task<IEnumerable<UserProfile>> GetAllAsync();
        Task<Guid> CreateAsync(UserProfile user);
        Task<Guid> UpdateAsync(UserProfile user);
        Task DeleteAsync(Guid userId);
    }
}