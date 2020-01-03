using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace KardoPasso.Models
{
    public static class KardoGeneralModel
    {
        public static Dictionary<string, List<Object>> getDicFromSDR(SqlDataReader sdr) 
        {
            Dictionary<string, List<Object>> dic = new Dictionary<string, List<Object>>();
            int drCount = 0;
            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    if (drCount == 0)
                        dic.Add(sdr.GetName(i), new List<Object>());
                    string curKey = sdr.GetName(i).ToString();
                    Object curVal = sdr.GetValue(i);
                    dic[curKey].Add(curVal);
                }
                drCount++;
            }

            return dic;
        }
        public static Dictionary<string, Object> getDicFromSDROneRow(SqlDataReader sdr)
        {
            Dictionary<string, Object> dic = new Dictionary<string, Object>();
            if (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    string curKey = sdr.GetName(i).ToString();
                    Object curVal = sdr.GetValue(i);
                    dic.Add(curKey, curVal);
                }
            }

            return dic;
        }

    }
}