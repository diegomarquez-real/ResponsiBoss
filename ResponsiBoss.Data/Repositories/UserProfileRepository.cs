using Dapper;
using Microsoft.Extensions.Logging;
using ResponsiBoss.Data.Abstractions;
using ResponsiBoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data
{
    public class UserProfileRepository : GenericRepository<UserProfile, Guid>,  Abstractions.IUserProfileRepository
    {
        private readonly ILogger<UserProfileRepository> _logger;

        public UserProfileRepository(IDataContext dataContext,
            IUserContext userContext,
            ILogger<UserProfileRepository> logger)
            : base (dataContext, userContext)
        {
            _logger = logger;
        }

        public async Task<UserProfile> FindByEmailAsync(string email)
        {
            try
            {
                var sql = @"SELECT u.*
                            FROM UserProfile AS u
                            WHERE u.EmailAddress = @EmailAddress";

                return await base.DbConnection.QuerySingleOrDefaultAsync<UserProfile>(sql, new { EmailAddress = email });
            }
            catch (Exception) { throw; }
        }
    }
}