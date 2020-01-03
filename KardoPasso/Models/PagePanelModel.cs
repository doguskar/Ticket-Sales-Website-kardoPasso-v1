using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace KardoPasso.Models
{
    public static class PagePanelModel
    {
        static SqlConnection con = DB_Connection.getConnection();

        public static Dictionary<string, List<Object>> getTeams()
        {
            string queryStr = "SELECT * FROM viewTeams";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static Dictionary<string, List<Object>> getStadiums()
        {
            string queryStr = "SELECT * FROM viewStadiums";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static Dictionary<string, List<Object>> getSportTypes()
        {
            string queryStr = "SELECT * FROM sportType WHERE deletedFlag = 0 ORDER BY sportType";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static Dictionary<string, List<Object>> getUsers()
        {
            string queryStr = "SELECT * FROM viewSummaryUsers";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static Dictionary<string, List<Object>> getEvents()
        {
            string queryStr = "SELECT * FROM viewAllEvents";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static Dictionary<string, List<Object>> getRoles()
        {
            string queryStr = "SELECT * FROM roles";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static string getTopTeams()
        {
            string queryStr = "SELECT * FROM viewTopTeams";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return KardoStaticMethods.getJSONFromDic(dic);
        }
        public static string getTopEvents()
        {
            string queryStr = "SELECT * FROM viewTopEvents";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return KardoStaticMethods.getJSONFromDic(dic);
        }
        public static string getTopStadiums()
        {
            string queryStr = "SELECT * FROM viewTopStadiums";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return KardoStaticMethods.getJSONFromDic(dic);
        }
        public static string getEventsDateGroups()
        {
            string queryStr = "SELECT * FROM viewEventsDateGroups";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return KardoStaticMethods.getJSONFromDic(dic);
        }
        public static bool insertEvent(int hTeamId, int oTeamId, int stadiumId, string eventDate, string description, float price, string picFile)
        {
            string queryStr = "EXEC procInsertEvent " + hTeamId + ", " + oTeamId + ", " + stadiumId + ", '" + eventDate + "', '" + description + "', '" + picFile + "', " + price;
            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool updateEvent(int hTeamId, int oTeamId, int stadiumId, string eventDate, string description, float price, string picFile, int eventId)
        {
            string queryStr = "EXEC procUpdateEvent " + hTeamId + ", " + oTeamId + ", " + stadiumId + ", '" + eventDate + "', '" + description + "', ";
            queryStr += (picFile == "NULL") ? "NULL, " : "'" + picFile + "', ";
            queryStr += price + ", " + eventId;

            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool deleteEvent(int eventId, int userId)
        {
            string queryStr = "EXEC procDeleteEvent " + eventId + ", " + userId;

            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool insertStadium(string name, string fileName, string location, string streetAdd, string state, string city, string country, int sportTypeId)
        {
            string queryStr = "EXEC procInsertStadium '" + name + "', '";
            queryStr += fileName + "', ";
            queryStr += (location.Length == 0) ? "NULL, " : "'" + location + "', ";
            queryStr += "'" + streetAdd + "', ";
            queryStr += (state.Length == 0) ? "NULL, " : "'" + state + "', ";
            queryStr += "'" + city + "', '";
            queryStr += country + "', ";
            queryStr += sportTypeId;

            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        /*public static bool updateStadium(string name, string fileName, string location, string streetAdd, string state, string city, string country, int sportTypeId, int stadiumId)
        {
            string queryStr = "EXEC procUpdateStadium '" + name + "', ";
            queryStr += (fileName == "NULL")? "NULL, ": "'" + fileName + "', ";
            queryStr += (location.Length == 0) ? "NULL, " : "'" + location + "', ";
            queryStr += "'" + streetAdd + "', ";
            queryStr += (state.Length == 0) ? "NULL, " : "'" + state + "', ";
            queryStr += "'" + city + "', '";
            queryStr += country + "', ";
            queryStr += sportTypeId + ", ";
            queryStr += stadiumId;

            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }*/
        public static bool deleteStadium(int stadiumId)
        {
            string queryStr = "DELETE FROM stadiums WHERE stadiumId =  " + stadiumId;

            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool insertTeam(string name, int sportTypeId)
        {
            string queryStr = "EXEC procInsertTeam '" + name + "', " + sportTypeId;

            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool updateTeam(string name, int sportTypeId, int teamId)
        {
            string queryStr = "EXEC procUpdateTeam '" + name + "', " + sportTypeId + ", " + teamId;

            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool deleteTeam(int teamId)
        {
            string queryStr = "DELETE FROM teams WHERE teamId =  " + teamId;

            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool insertSportType(string name)
        {
            string queryStr = "EXEC procInsertSportType '" + name + "'";

            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool deleteSportType(int sportTypeId)
        {
            string queryStr = "DELETE FROM sportType WHERE sportTypeId =  " + sportTypeId;

            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool updateSportType(string name, int sportTypeId)
        {
            string queryStr = "EXEC procUpdateSportType '" + name + "', " + sportTypeId;

            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool updateUser(int roleId, int userId)
        {
            string queryStr = "UPDATE users SET roleId = " + roleId + " WHERE userId=" + userId;

            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool deleteUser(int userId, int deletedById)
        {
            string queryStr = "EXEC procDeleteUser " + userId + ", " + deletedById;

            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static int countNewUsers()
        {
            string queryStr = "SELECT COUNT(userId) AS newUsers FROM users WHERE createdDate > DATEADD(YEAR, -1, GETUTCDATE()) AND deletedFlag = 0";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, Object> dic = KardoGeneralModel.getDicFromSDROneRow(sdr);
            con.Close();

            return (int)dic["newUsers"];
        }
        public static int countNewKps()
        {
            string queryStr = "SELECT COUNT(kardoPassoId) AS newKps FROM kardoPassos WHERE saleDate > DATEADD(YEAR, -1, GETUTCDATE())";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, Object> dic = KardoGeneralModel.getDicFromSDROneRow(sdr);
            con.Close();

            return (int)dic["newKps"];
        }
        public static string getEventFromId(int eventId)
        {
            string queryStr = "SELECT * FROM events WHERE eventId=" + eventId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, Object> dic = KardoGeneralModel.getDicFromSDROneRow(sdr);
            con.Close();

            return KardoStaticMethods.getJSONFromDic(dic);
        }
        /*public static string getStadiumFromId(int stadiumId)
        {
            string queryStr = "SELECT s.*, ts.sportTypeId FROM stadiums s INNER JOIN theStadiumIsSuitableFor ts ON ts.stadiumId = s.stadiumId  WHERE s.stadiumId=" + stadiumId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, Object> dic = KardoGeneralModel.getDicFromSDROneRow(sdr);
            con.Close();

            return KardoStaticMethods.getJSONFromDic(dic);
        }*/
        public static string getTeamFromId(int teamId)
        {
            string queryStr = "SELECT * FROM teams WHERE teamId=" + teamId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, Object> dic = KardoGeneralModel.getDicFromSDROneRow(sdr);
            con.Close();

            return KardoStaticMethods.getJSONFromDic(dic);
        }
        public static string getSportTypeFromId(int sportTypeId)
        {
            string queryStr = "SELECT * FROM sportType WHERE sportTypeId=" + sportTypeId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, Object> dic = KardoGeneralModel.getDicFromSDROneRow(sdr);
            con.Close();

            return KardoStaticMethods.getJSONFromDic(dic);
        }
        public static string getUserFromId(int userId)
        {
            string queryStr = "SELECT * FROM users WHERE userId=" + userId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, Object> dic = KardoGeneralModel.getDicFromSDROneRow(sdr);
            con.Close();

            return KardoStaticMethods.getJSONFromDic(dic);
        }
    }
}