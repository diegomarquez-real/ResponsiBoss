using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data
{
    public class DataContext : Abstractions.IDataContext
    {
        private readonly IOptions<Options.ConnectionStringOptions> _connectionStringOptions;

        public DataContext(IOptions<Options.ConnectionStringOptions> connectionStringOptions)
        {
            _connectionStringOptions = connectionStringOptions;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionStringOptions.Value.MSSQLConnection);

    }
}