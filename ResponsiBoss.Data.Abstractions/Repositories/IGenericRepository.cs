using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data.Abstractions
{
    public interface IGenericRepository<TEntity, TPrimaryKey>
    {
        Task<TEntity> GetByIdAsync(TPrimaryKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TPrimaryKey id);
    }
}