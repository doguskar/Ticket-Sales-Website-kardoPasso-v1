using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace KardoPasso.Models
{
    public static class EditStadiumModel
    {
        static SqlConnection con = DB_Connection.getConnection();
        public static Dictionary<string, Object> getStadium(int stadiumId)
        {
            string queryStr = "EXEC procViewTheStadium " + stadiumId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, Object> dic = KardoGeneralModel.getDicFromSDROneRow(sdr);
            con.Close();

            return dic;
        }
        public static Dictionary<string, List<Object>> getSSections(int stadiumId)
        {
            string queryStr = "EXEC procViewTheStadiumSections " + stadiumId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static string getStadiumFromId(int stadiumId)
        {
            string queryStr = "SELECT s.*, ts.sportTypeId FROM stadiums s INNER JOIN theStadiumIsSuitableFor ts ON ts.stadiumId = s.stadiumId  WHERE s.stadiumId=" + stadiumId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, Object> dic = KardoGeneralModel.getDicFromSDROneRow(sdr);
            con.Close();

            return KardoStaticMethods.getJSONFromDic(dic);
        }
        public static bool updateStadium(string name, string fileName, string location, string streetAdd, string state, string city, string country, int sportTypeId, int stadiumId)
        {
            string queryStr = "EXEC procUpdateStadium '" + name + "', ";
            queryStr += (fileName == "NULL") ? "NULL, " : "'" + fileName + "', ";
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
        }
        public static Dictionary<string, List<Object>> getSecCategories()
        {
            string queryStr = "SELECT * FROM sSectionCategories ORDER BY priceRatio";
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, List<Object>> dic = KardoGeneralModel.getDicFromSDR(sdr);
            con.Close();

            return dic;
        }
        public static string getSectionFromId(int sectionId)
        {
            string queryStr = "SELECT ss.*, ssc.categoryName, ssc.priceRatio FROM stadiumSections ss INNER JOIN sSectionCategories ssc ON ss.categoryId = ssc.categoryId WHERE ss.sectionId=" + sectionId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            SqlDataReader sdr;
            con.Open();
            sdr = sc.ExecuteReader();
            Dictionary<string, Object> dic = KardoGeneralModel.getDicFromSDROneRow(sdr);
            con.Close();

            return KardoStaticMethods.getJSONFromDic(dic);
        }

        public static bool insertSection(string name, int capacity, int categoryId, int stadiumId)
        {
            string queryStr = "EXEC procInsertStadiumSection '" + name + "', " + capacity + ", " + categoryId + ", " + stadiumId;
            SqlCommand sc = new SqlCommand(queryStr, con);
            con.Open();
            int result = sc.ExecuteNonQuery();
            con.Close();
            if (result > 0)
                return true;
            return false;
        }
        public static bool updateSection(string name, int capacity, int categoryId, int sectionId)
        {
            string queryStr = "EXEC procUpdateSection '" + name + "', " + capacity + ", " + categoryId + ", " + sectionId;
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