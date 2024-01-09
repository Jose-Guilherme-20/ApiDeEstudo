using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_Api.Context
{
    public class DapperContext
    {
        private readonly DbConnection _conn;

        public DapperContext(DbConnection conn)
        {
            _conn = conn;
        }
        public DbConnection DapperConnection
        {
            get
            {
                return _conn;
            }
        }
    }
}