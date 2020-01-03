using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace KardoPasso.Models
{
    public static class UserProfileModel
    {
        static SqlConnection con = DB_Connection.getConnection();

        public static string getUserTickets(int userId)
        {
            string queryStr = "EXEC procGetUserTickets " + userId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return KardoStaticMethods.getJSONFromDic(dic);
        }
        public static bool insertMoney(int userId, int money)
        {
            string queryStr = "EXEC procInsertMoney " + userId + ", " + money;
            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool updateDues(int userId)
        {
            string queryStr = "EXEC procUpdateDuesDate " + userId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool updatePassword(int userId, string password)
        {
            string queryStr = "EXEC procUpdatePassword " + userId + ", '" + password + "'";
            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool updateProfile(int userId, string name, string surname, string mail, string phone, string countryCode)
        {
            string queryStr = "EXEC procUpdateProfile " + userId + ", ";
            queryStr += (string.IsNullOrEmpty(name)) ? "NULL, " : "'" + name + "',";
            queryStr += (string.IsNullOrEmpty(surname)) ? "NULL, " : "'" + surname + "',";
            queryStr += (string.IsNullOrEmpty(mail)) ? "NULL, " : "'" + mail + "',";
            queryStr += (string.IsNullOrEmpty(phone)) ? "NULL, " : "'" + phone + "',";
            queryStr += (string.IsNullOrEmpty(phone)) ? "NULL" : "'" + countryCode + "'";
            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool getKardoPasso(int userId)
        {
            string queryStr = "EXEC procInsertKardoPasso " + userId;
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