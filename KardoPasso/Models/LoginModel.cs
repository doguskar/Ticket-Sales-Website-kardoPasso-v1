using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace KardoPasso.Models
{
    public static class LoginModel
    {
        static SqlConnection con = DB_Connection.getConnection();

        public static Dictionary<string, List<Object>> getUserFromUsernameOrMail(string key) 
        {
            string queryStr = "EXEC procFindUser '" + key + "'";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
                sdr = sc.ExecuteReader();
                Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static Dictionary<string, List<Object>> getUserFromUserId(int userId)
        {
            string queryStr = "EXEC procGetUserFromUserId " + userId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
                sdr = sc.ExecuteReader();
                Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static Dictionary<string, List<Object>> getUsersFromUserIds(string userIds)
        {
            string queryStr = "EXEC procGetUsersFromUserIds '" + userIds + "'";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
                sdr = sc.ExecuteReader();
                Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static Dictionary<string, List<Object>> getUserFromUUID(string UUID)
        {
            string queryStr = "EXEC procGetUserFromUUID '" + UUID + "'";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
                sdr = sc.ExecuteReader();
                Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }

        public static bool isUsernameAvaible(string username) 
        {
            string queryStr = "SELECT TOP 1 username FROM users WHERE username='" + username.ToLower() + "'";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                con.Close();
                return true;
            }
            con.Close();
            return false;
        }

        public static bool isEMailAvaible(string eMail)
        {
            string queryStr = "SELECT TOP 1 email FROM emails WHERE email='" + eMail.ToLower() + "'";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                con.Close();
                return true;
            }
            con.Close();
            return false;
        }

        public static bool insertUser(string username, string email, string password, string name, string surname, string bornDate, string languagePreference)
        {
            string queryStr = "EXEC procInsertUser '"
                + username + "', '" 
                + email + "', '" 
                + password + "', '" 
                + name + "', '" 
                + surname + "', '" 
                + bornDate + "', '" 
                + languagePreference + "'";
            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if(result > 0)
                return true;
            return false;
        }

        public static bool setAKeyForEMail(int userId, string activationKey)
        {
            string queryStr = "UPDATE emails SET activationKey = '" + activationKey + "'" + 
                " WHERE userId = " + userId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }

        public static bool verifyEMail(int userId)
        {
            string queryStr = "EXEC procVerifyMail " + userId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static Dictionary<string, List<Object>> getAuthentications(int userId)
        {
            string queryStr = "EXEC procGetAuthenticationsLast30Min " + userId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static bool setAuthentication(string ipAdd, string country, string city, string location, string hostname, string password, Boolean status, int userId)
        {
            string queryStr = "EXEC procInsertAuthentications ";
            queryStr += (ipAdd == "NULL") ? "NULL, " : "'" + ipAdd + "', ";
            queryStr += (country == "NULL") ? "NULL, " : "'" + country + "', ";
            queryStr += (city == "NULL") ? "NULL, " : "'" + city + "', ";
            queryStr += (location == "NULL") ? "NULL, " : "'" + location + "', ";
            queryStr += (hostname == "NULL") ? "NULL, " : "'" + hostname + "', ";
            queryStr += "'" + password + "', ";
            queryStr += (status) ? "1, " : "0, ";
            queryStr += userId;
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