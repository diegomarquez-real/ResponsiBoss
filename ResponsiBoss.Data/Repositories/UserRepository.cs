using ResponsiBoss.Data.Abstractions;
using ResponsiBoss.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data
{
    public class UserRepository : GenericRepository<UserProfile, Guid>, Abstractions.IUserRepository
    {
        public UserRepository(IDataContext dataContext)
            :base(dataContext)
        {
        }
    }
}