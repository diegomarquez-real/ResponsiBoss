using ResponsiBoss.Data.Abstractions;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace ResponsiBoss.Data
{
    public abstract class GenericRepository<TEntity, TPrimaryKeyType> : Abstractions.IGenericRepository<TEntity, TPrimaryKeyType> where TEntity : class
    {
        protected IDbConnection DbConnection;
        private readonly IUserContext _userContext;

        protected bool StampWithCreatedBy { get; set; } = true;
        protected bool StampWithUpdatedBy { get; set; } = true;
        protected bool StampWithCreatedOn { get; set; } = true;
        protected bool StampWithUpdatedOn { get; set; } = true;

        public GenericRepository(IDataContext dataContext,
            IUserContext userContext)
        {
            this.DbConnection = dataContext.CreateConnection();
            _userContext = userContext;
        }

        public async Task<TEntity> FindByIdAsync(TPrimaryKeyType entityId)
        {
            try
            {
                string sql = @$"SELECT * 
                                FROM {GenericExtensions.GetTableName<TEntity>()} 
                                WHERE {GenericExtensions.GetKeyColumnName<TEntity>()} = @EntityId";

                return await this.DbConnection.QuerySingleAsync<TEntity>(sql, new { EntityId = entityId });
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                string sql = @$"SELECT * 
                                FROM {GenericExtensions.GetTableName<TEntity>()}";

                return await this.DbConnection.QueryAsync<TEntity>(sql);
            }
            catch (Exception) { throw; }
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            try
            {
                this.TrySetCreatedBy(entity);
                this.TrySetCreatedDate(entity);

                string sql = @$"INSERT INTO {GenericExtensions.GetTableName<TEntity>()} ({GenericExtensions.GetColumns<TEntity>(excludeKey: true)})
                                OUTPUT INSERTED.*
                                VALUES ({GenericExtensions.GetPropertyNames<TEntity>(excludeKey: true)})";

                return await this.DbConnection.QuerySingleAsync<TEntity>(sql, entity);
            }
            catch (Exception) { throw; }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                this.TrySetUpdatedBy(entity);
                this.TrySetUpdatedDate(entity);

                string sql = @$"UPDATE {GenericExtensions.GetTableName<TEntity>()} SET ";

                foreach (var property in GenericExtensions.GetProperties<TEntity>(excludeKey: true))
                {
                    var columnAttr = property.GetCustomAttribute<ColumnAttribute>();

                    sql += ($"{columnAttr?.Name ?? property.Name} = @{property.Name},");
                }

                sql = sql.Remove(sql.Length - 1) + " ";

                sql += @$"OUTPUT INSERTED.* 
                          WHERE {GenericExtensions.GetKeyColumnName<TEntity>()} = @{GenericExtensions.GetKeyPropertyName<TEntity>()}";

                return await this.DbConnection.QuerySingleAsync<TEntity>(sql, entity);
            }
            catch (Exception) { throw; }
        }

        public async Task DeleteAsync(TPrimaryKeyType entityId)
        {
            try
            {
                string sql = @$"DELETE FROM {GenericExtensions.GetTableName<TEntity>()} 
                                WHERE {GenericExtensions.GetKeyColumnName<TEntity>()} = @EntityId";

                await this.DbConnection.ExecuteScalarAsync(sql, new { EntityId = entityId } );
            }
            catch (Exception) { throw; }
        }

        #region Additional Functionality

        protected void TrySetCreatedBy(object entity)
        {
            if (!StampWithCreatedBy) 
                return;

            var p = entity.GetType().GetProperty("CreatedBy");

            if (p == null)
                return;

            p.SetValue(entity, _userContext.CurrentUserIdentifier);
        }

        protected void TrySetUpdatedBy(object entity)
        {
            if (!StampWithUpdatedBy) return;
            var p = entity.GetType().GetProperty("UpdatedBy");

            if (p == null)
                return;

            p.SetValue(entity, _userContext.CurrentUserIdentifier);
        }

        protected void TrySetCreatedDate(object entity)
        {
            if (!StampWithCreatedOn) 
                return;

            var p = entity.GetType().GetProperty("CreatedOn");

            if (p == null)
                return;

            p.SetValue(entity, DateTime.UtcNow);
        }

        protected void TrySetUpdatedDate(object entity)
        {
            if (!StampWithUpdatedOn) return;

            var p = entity.GetType().GetProperty("UpdatedOn");

            if (p == null)
                return;

            p.SetValue(entity, DateTime.UtcNow);
        }

        #endregion
    }
}