using Dapper;
using ResponsiBoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data
{
    public class UserRepository : RepositoryBase,  Abstractions.IUserRepository
    {
        public UserRepository(Abstractions.IDataContext dataContext)
            : base (dataContext)
        {
        }

        public async Task<Guid> CreateAsync(UserProfile entity)
        {
            string sql = @"INSERT INTO [User] ([UserId], [Username], [Password], [EmailAddress], [PhoneNumber], [CreatedOn])
                           VALUES (@UserId, @Username, @Password, @EmailAddress, @PhoneNumber, @CreatedOn)";

            await base.DbConnection.ExecuteScalarAsync(sql, entity);

            return entity.UserId;
        }

        public async Task<Guid> DeleteAsync(Guid userId)
        {
            string sql = @"DELETE FROM [User]
                           WHERE UserId = @UserId";

            await base.DbConnection.ExecuteScalarAsync(sql, new { UserId = userId });

            return userId;
        }

        public async Task<UserProfile> FindByIdAsync(Guid userId)
        {
            var sql = @"SELECT u.*
                        FROM [User] AS u
                        WHERE u.UserId = @UserId";

            var result = await base.DbConnection.QuerySingleAsync<UserProfile>(sql, new { UserId = userId });

            return result;
        }

        public async Task<IEnumerable<UserProfile>> GetAllAsync()
        {
            var sql = @"SELECT u.*
                        FROM [User] AS u";

            var users = await base.DbConnection.QueryAsync<UserProfile>(sql);

            return users;
        }

        public async Task<Guid> UpdateAsync(UserProfile entity)
        {
            string sql = @"UPDATE [User]
                           SET [Username] = @Username, [Password] = @Password, [EmailAddress] = @EmailAddress, [PhoneNumber] = @PhoneNumber
                           WHERE [UserId] = @UserId";
            
            var userId = await base.DbConnection.ExecuteScalarAsync<Guid>(sql, entity);

            return userId;
        }
    }
}