using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

public static class KardoStaticMethods
{
    //UPDATED 2019.04.26
    public static string creatUniqString()
    {
        return "";
    }
    public static string creatUniqString(int numberOfChar)
    {
        string result = "";
        Random random = new Random();
        string endOfString = "";
        if(numberOfChar > 18){
            string[] now = getNowDateTimeUTC().Split(':','-',' ');
            endOfString = "-" + now[0] + now[1] + now[2] + now[3] + now[4] + now[5] + now[6];

            for (int i = 0; i < numberOfChar - 18; i++)
            {
                int rSelection = random.Next(3);
                switch (rSelection)
                {
                    case 0: result += random.Next(0, 10); break; //numbers
                    case 1: result += (char)random.Next(65, 91); break; // upper letters
                    case 2: result += (char)random.Next(97, 122); break; // lower letters
                }
            }

            return result + endOfString;
        }

        for (int i = 0; i < numberOfChar; i++)
        {
            int rSelection = random.Next(3);
            switch (rSelection)
            {
                case 0: result += random.Next(0, 10); break; //numbers
                case 1: result += (char)random.Next(65, 91); break; // upper letters
                case 2: result += (char)random.Next(97, 122); break; // lower letters
            }
        }
        return result;
    }

    public static string getNowDay()
    {
        string[] now = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture).Split(' ', '\t', '/', ':', '.', '-');
        return now[0];
    }
    public static string getNowMonth()
    {
        string[] now = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture).Split(' ', '\t', '/', ':', '.', '-');
        return now[1];
    }
    public static string getNowYear()
    {
        string[] now = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture).Split(' ', '\t', '/', ':', '.', '-');
        return now[2];
    }
    public static string getNowHour()
    {
        string[] now = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture).Split(' ', '\t', '/', ':', '.', '-');
        return now[3];
    }
    public static string getNowMinute()
    {
        string[] now = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture).Split(' ', '\t', '/', ':', '.', '-');
        return now[4];
    }
    public static string getNowSecond()
    {
        string[] now = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture).Split(' ', '\t', '/', ':', '.', '-');
        return now[5];
    }
    public static string getNowMilliSecond()
    {
        string[] now = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture).Split(' ', '\t', '/', ':', '.', '-');
        return now[6];
    }
    public static string getNowDate()
    {
        string[] now = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture).Split(' ', '\t', '/', ':', '.', '-');
        return now[2] + "-" + now[1] + "-" + now[0];
    }
    public static string getNowTime()
    {
        string[] now = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture).Split(' ', '\t', '/', ':', '.', '-');
        return now[3] + ":" + now[4] + ":" + now[5] + ":" + now[6];
    }
    public static string getNowDateTimeUTC() 
    {
        string now = DateTime.UtcNow.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss':'fff");
        return now;
    }
    public static string getNowDateUTC()
    {
        string nowDate = DateTime.UtcNow.ToString("yyyy'-'MM'-'dd");
        return nowDate;
    }
    public static string getNowTimeUTC()
    {
        string nowTime = DateTime.UtcNow.ToString("HH':'mm':'ss':'fff");
        return nowTime;
    }
    public static int compareTimeAsMinute(string time1, string time2) 
    {
        //This Function returns difference between two times as minutes
        //If there is a parameter, it will compare to time with nowTime.
        string[] arrTime1 = time1.Split(':');
        string[] arrTime2 = time2.Split(':');
        int result = 0;
        if (arrTime1.Length <= 4 && arrTime1.Length >= 3 && arrTime2.Length <= 4 && arrTime2.Length >= 3)
        {
            result += Math.Abs((Convert.ToInt32(arrTime1[0]) - Convert.ToInt32(arrTime2[0])) * 60);
            result += Math.Abs((Convert.ToInt32(arrTime1[1]) - Convert.ToInt32(arrTime2[1])));
        }
        return result;
    }
    public static int compareTimeAsMinute(string time1)
    {
        return compareTimeAsMinute(getNowTimeUTC(), time1);
    }
    public static int compareTimeAsSecond(string time1, string time2)
    {
        //This Function returns difference between two times as seconds
        //If there is a parameter, it will compare to time with nowTime.
        string[] arrTime1 = time1.Split(':');
        string[] arrTime2 = time2.Split(':');
        int result = 0;
        if (arrTime1.Length <= 4 && arrTime1.Length >= 3 && arrTime2.Length <= 4 && arrTime2.Length >= 3)
        {
            result += Math.Abs((Convert.ToInt32(arrTime1[0]) - Convert.ToInt32(arrTime2[0])) * 60 * 60);
            result += Math.Abs((Convert.ToInt32(arrTime1[1]) - Convert.ToInt32(arrTime2[1])) * 60);
            result += Math.Abs((Convert.ToInt32(arrTime1[2]) - Convert.ToInt32(arrTime2[2])));
        }
        return result;
    }
    public static int compareTimeAsSecond(string time1)
    {
        return compareTimeAsSecond(getNowTime(), time1);
    }
    public static int compareTimeAsMilliSecond(string time1, string time2)
    {
        //This Function returns difference between two times as milliseconds
        //If there is a parameter, it will compare to time with nowTime.
        string[] arrTime1 = time1.Split(':');
        string[] arrTime2 = time2.Split(':');
        int result = 0;
        if (arrTime1.Length == 4  && arrTime2.Length == 4)
        {
            result += Math.Abs((Convert.ToInt32(arrTime1[0]) - Convert.ToInt32(arrTime2[0])) * 60 * 60 * 100);
            result += Math.Abs((Convert.ToInt32(arrTime1[1]) - Convert.ToInt32(arrTime2[1])) * 60 * 100);
            result += Math.Abs((Convert.ToInt32(arrTime1[2]) - Convert.ToInt32(arrTime2[2])) * 100);
            result += Math.Abs((Convert.ToInt32(arrTime1[3]) - Convert.ToInt32(arrTime2[3])));
        }
        return result;
    }
    public static int compareTimeAsMilliSecond(string time1)
    {
        return compareTimeAsMilliSecond(getNowTime(), time1);
    }

    public static string createKardoUID()
    {
        return (new KardoUID()).getUID();
    }
    public static string FirstCharToUpper(string input)
    {

       /* string str = input;
        str.ToTitleCase(TitleCase.All);*/
        if (String.IsNullOrEmpty(input))
            throw new ArgumentException("ARGH!");
        return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
    }
    public static string getJSONFromDic(Dictionary<string, List<Object>> dic)
    {
        string jsonString = "{";
        foreach (string key in dic.Keys)
        {
            if (jsonString == "{")
                jsonString += "\"" + key + "\":[";
            else
                jsonString += ", \"" + key + "\":[";
            int j = 0;
            foreach (Object obj in dic[key])
            {
                if (j == 0)
                    jsonString += "\"" + obj + "\"";
                else
                    jsonString += ", \"" + obj + "\"";
                j++;
            }
            jsonString += "]";
        }

        jsonString += "}";
        return jsonString;
    }
    public static string getJSONFromDic(Dictionary<string, Object> dic)
    {
        string jsonString = "{";
        foreach (string key in dic.Keys)
        {
            if (jsonString == "{")
                jsonString += "\"" + key + "\": ";
            else
                jsonString += ", \"" + key + "\": ";

            jsonString += "\"" + dic[key] + "\"";
        }

        jsonString += "}";
        return jsonString;
    }
        


}