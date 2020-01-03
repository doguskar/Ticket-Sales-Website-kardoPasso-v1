using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace KardoPasso.Models
{
    public static class EventDetailModel
    {
        static SqlConnection con = DB_Connection.getConnection();

        public static Dictionary<string, List<Object>> getEventTicketInfo(int eventId)
        {
            string queryStr = "EXEC procEventTicketInfo " + eventId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
                sdr = sc.ExecuteReader();
                Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static Dictionary<string, Object> getEventInfo(int eventId)
        {
            string queryStr = "EXEC procViewTheEvent " + eventId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
                sdr = sc.ExecuteReader();
                Dictionary<string, Object> dic = KardoGeneralModel.getDicFromSDROneRow(sdr);
            con.Close();

            return dic;
        }
        public static string getSectionInfo(int eventId, int ssId)
        {
            string queryStr = "EXEC procEventTicketInfoOneSec " + eventId + ", " + ssId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, Object> dic = KardoGeneralModel.getDicFromSDROneRow(sdr);
            con.Close();

            return KardoStaticMethods.getJSONFromDic(dic);
        }
        public static bool insertSale(int eventId, int sectionId, int kardoPassoId, int amount, int userId)
        {
            string queryStr = "EXEC procInsertSale " + eventId + ", " + sectionId + ", " + kardoPassoId + ", " + amount + ", " + userId;
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