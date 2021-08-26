using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DapperCore.DAL
{
    public class SqlDAL
    {
        private readonly SqlConnection conn;
        private readonly IConfiguration configuration;
        private readonly SqlDataAdapter sqlDataAdapter;
        public SqlDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            conn = new SqlConnection(configuration.GetConnectionString("DbConnection"));
            sqlDataAdapter = new SqlDataAdapter();
        }
        private SqlConnection OpenConnection()
        {
            if (conn.State == System.Data.ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }
    

    }
}
