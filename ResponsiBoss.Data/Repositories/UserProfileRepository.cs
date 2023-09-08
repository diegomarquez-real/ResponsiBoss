using Dapper;
using Microsoft.Extensions.Logging;
using ResponsiBoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data
{
    public class UserProfileRepository : RepositoryBase,  Abstractions.IUserProfileRepository
    {
        private readonly ILogger<UserProfileRepository> _logger;

        public UserProfileRepository(Abstractions.IDataContext dataContext, 
            ILogger<UserProfileRepository> logger)
            : base (dataContext)
        {
            _logger = logger;
        }

        public async Task<UserProfile> FindByIdAsync(Guid userId)
        {
            try
            {
                var sql = @"SELECT u.*
                            FROM [UserProfile] AS u
                            WHERE u.UserId = @UserId";

                return await base.DbConnection.QuerySingleAsync<UserProfile>(sql, new { UserId = userId });
            }
            catch (Exception) { throw; }
        }

        public async Task<UserProfile> FindByEmailAsync(string email)
        {
            try
            {
                var sql = @"SELECT u.*
                            FROM [UserProfile] AS u
                            WHERE u.EmailAddress = @EmailAddress";

                return await base.DbConnection.QuerySingleAsync<UserProfile>(sql, new { EmailAddress = email });
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<UserProfile>> GetAllAsync()
        {
            try
            {
                var sql = @"SELECT u.*
                            FROM [UserProfile] AS u";

                return await base.DbConnection.QueryAsync<UserProfile>(sql);
            }
            catch (Exception) { throw; }
        }

        public async Task<Guid> CreateAsync(UserProfile user)
        {
            try
            {
                string sql = @"INSERT INTO [UserProfile] ([UserId], [EmailAddress], [PasswordHash], [FirstName], [LastName], [PhoneNumber])
                               VALUES (@UserId, @EmailAddress, @PasswordHash, @FirstName, @LastName, @PhoneNumber)";

                await base.DbConnection.ExecuteScalarAsync(sql, user);

                return user.UserId;
            }
            catch (Exception) { throw; }
        }

        public async Task<Guid> UpdateAsync(UserProfile user)
        {
            try
            {
                string sql = @"UPDATE [UserProfile]
                               SET [EmailAddress] = @EmailAddress, [PasswordHash] = @PasswordHash, [FirstName] = @FirstName, 
                                   [LastName] = @LastName, [PhoneNumber] = @PhoneNumber
                               WHERE [UserId] = @UserId";

                return await base.DbConnection.ExecuteScalarAsync<Guid>(sql, user);
            }
            catch (Exception) { throw; }
        }

        public async Task DeleteAsync(Guid userId)
        {
            try
            {
                string sql = @"DELETE FROM [UserProfile]
                               WHERE UserId = @UserId";

                await base.DbConnection.ExecuteScalarAsync(sql, new { UserId = userId });
            }
            catch (Exception) { throw; }
        }
    }
}