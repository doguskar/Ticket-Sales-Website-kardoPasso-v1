using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace KardoPasso.Models
{
    public static class DB_Connection
    {
        public static SqlConnection getConnection()
        {
            SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["kardoPasso"].ConnectionString);
            return con;
        }
    }

    
}