using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace KardoPasso.Models
{
    public static class DefaultModel
    {
        static SqlConnection con = DB_Connection.getConnection();

        public static Dictionary<string, Object> getUserFromUserId(int userId)
        {
            string queryStr = "EXEC procGetUserFromUserId " + userId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
                sdr = sc.ExecuteReader();
                Dictionary<string, Object> dic = KardoGeneralModel.getDicFromSDROneRow(sdr);
            con.Close();

            return dic;
        }
        public static Dictionary<string, List<Object>> getEvents()
        {
            string queryStr = "SELECT * FROM viewEvents";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
                sdr = sc.ExecuteReader();
                Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static Dictionary<string, List<Object>> getEventsCities()
        {
            string queryStr = "SELECT * FROM viewEventsCities";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
                sdr = sc.ExecuteReader();
                Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static Dictionary<string, List<Object>> getEventsTeams()
        {
            string queryStr = "SELECT * FROM viewEventsTeams";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
                sdr = sc.ExecuteReader();
                Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static string getFilteredEvents(string city, string team, string startDate, string endDate)
        {
            if (team == "0")
                team = "NULL";

            string queryStr = "EXEC procViewEvents " + team + ", ";
            queryStr += (startDate == "Click to Select") ? "NULL, " : "'" + startDate + "', ";
            queryStr += (endDate == "Click to Select") ? "NULL, " : "'" + endDate + "', ";
            queryStr += (city == "0") ? "NULL" : "'" + city + "'";

            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            if(dic.Count != 0)
            {
                for (int curIn = 0; curIn < dic["eventId"].Count; curIn++)
                {
                    dic["eventId"][curIn] = UseKardoEncryption.getEncryptedString(dic["eventId"][curIn].ToString(),100);
                }
            }

            return KardoStaticMethods.getJSONFromDic(dic);
        }

    }
}