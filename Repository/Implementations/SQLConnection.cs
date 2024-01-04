using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class SQLConnection
    {
        public (SqlConnection, SqlCommand) ConnectToDB()
        {
            string connectionString = "Data Source=.;Initial Catalog=CarDB;Integrated Security=True;Connect Timeout=60;Encrypt=False";

            SqlConnection con = new(connectionString);

            con.Open();

            using SqlCommand cmd = new(null, con);

            cmd.CommandType = CommandType.StoredProcedure;

            return (con, cmd);
        }
    }
}
