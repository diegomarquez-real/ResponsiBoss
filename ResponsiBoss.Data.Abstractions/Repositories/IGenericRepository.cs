using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data.Abstractions
{
    public interface IGenericRepository<TEntity, TPrimaryKeyType>
    {
        Task<TEntity> FindByIdAsync(TPrimaryKeyType id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TPrimaryKeyType entityId);
    }
}