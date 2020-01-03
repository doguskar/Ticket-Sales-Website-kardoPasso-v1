using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace KardoPasso.Models
{
    public static class PermissionsModel
    {
        static SqlConnection con = DB_Connection.getConnection();
        public static Dictionary<string, List<Object>> getRoles()
        {
            string queryStr = "SELECT * FROM viewRoles";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static bool updatePermission(string column, int roleId, bool isChecked)
        {
            string queryStr = "UPDATE permisions SET ";
            queryStr += column + "=";
            queryStr += (isChecked) ? "1 " : "0 ";
            queryStr += "WHERE roleId = " + roleId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool insertRole(string roleName)
        {
            string queryStr = "procInsertRole '" + roleName + "'";
            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
    }
}