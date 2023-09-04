using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data.Repositories
{
    internal class GenericRepository<TEntity, TPrimaryKey> : Abstractions.IGenericRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        protected IDbConnection DbConnection;

        public GenericRepository(Abstractions.IDataContext dataContext)
        {
            this.DbConnection = dataContext.CreateConnection();
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            int inserted = 0;

            try
            {
                inserted = await this.DbConnection.InsertAsync<TEntity>(entity);
            }
            catch (Exception ex) { }

            return inserted == 0;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            bool deleted = false;

            try
            {
                deleted = await this.DbConnection.DeleteAsync<TEntity>(entity);
            }
            catch (Exception ex) { }

            return deleted;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            IEnumerable<TEntity> result = null;

            try
            {
                result = await this.DbConnection.GetAllAsync<TEntity>();
            }
            catch (Exception ex) { }

            return result;
        }

        public async Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            TEntity result = null;

            try
            {
                result = await this.DbConnection.GetAsync<TEntity>(id);
            }
            catch (Exception ex) { }

            return result;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            bool updated = false;

            try
            {
                updated = await this.DbConnection.UpdateAsync<TEntity>(entity);
            }
            catch (Exception ex) { }

            return updated;
        }
    }
}