using ResponsiBoss.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data
{
    public abstract class RepositoryBase
    {
        protected IDbConnection DbConnection;
        public RepositoryBase(IDataContext dataContext)
        {
            this.DbConnection = dataContext.CreateConnection();
        }
    }
}
