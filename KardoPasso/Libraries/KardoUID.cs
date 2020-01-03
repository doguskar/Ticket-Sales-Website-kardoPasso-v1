using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class KardoUID
{
    private string UID;
    public KardoUID() {
        runCreateUID();
    }
    private void runCreateUID()
    {
        string UID = "";
        string[] time = getNowDateTimeUTC().Split('-', ' ', ':'); // "yyyy'-'MM'-'dd' 'HH':'mm':'ss':'fff"
        Random random = new Random();
        for (int i = 0; i < 6; i++)
        {
            int rSelection = random.Next(3);
            switch (rSelection)
            {
                case 0: UID += random.Next(0, 10); break; //numbers
                case 1: UID += (char)random.Next(65, 91); break; // upper letters
                case 2: UID += (char)random.Next(97, 122); break; // lower letters
            }
        }
        int second = Convert.ToInt32(time[5]);
        int reminding = second % 10;
        UID += encryptSecond(second);
        UID += encryptChar(reminding, (char)((time[0])[0]));
        UID += encryptChar(reminding, (char)((time[0])[1]));
        UID += encryptChar(reminding, (char)((time[0])[2]));
        UID += encryptChar(reminding, (char)((time[0])[3]));
        UID += encryptChar(reminding, (char)((time[1])[0]));
        UID += encryptChar(reminding, (char)((time[1])[1]));
        UID += encryptChar(reminding, (char)((time[2])[0]));
        UID += encryptChar(reminding, (char)((time[2])[1]));
        UID += encryptChar(reminding, (char)((time[3])[0]));
        UID += encryptChar(reminding, (char)((time[3])[1]));
        UID += encryptChar(reminding, (char)((time[4])[0]));
        UID += encryptChar(reminding, (char)((time[4])[1]));
        UID += encryptChar(reminding, (char)((time[5])[0]));
        UID += encryptChar(reminding, (char)((time[5])[1]));
        UID += encryptChar(reminding, (char)((time[6])[0]));
        UID += encryptChar(reminding, (char)((time[6])[1]));
        UID += encryptChar(reminding, (char)((time[6])[2]));

        this.UID = UID;
    }
    public string getUID() {
        return this.UID;
    }
    public string getDateOfCreationOfUID()
    {
        return runGetDateOfCreationOfUID(this.UID);
    }
    private string runGetDateOfCreationOfUID(string submittedUID)
    {
        int second = decipherSecond(submittedUID[6].ToString() + submittedUID[7].ToString());
        int reminding = second % 10;
        string theYear = "";
        theYear += decipherChar(reminding, (submittedUID[8].ToString() + submittedUID[9]));
        theYear += decipherChar(reminding, (submittedUID[10].ToString() + submittedUID[11]));
        theYear += decipherChar(reminding, (submittedUID[12].ToString() + submittedUID[13]));
        theYear += decipherChar(reminding, (submittedUID[14].ToString() + submittedUID[15]));
        string theMonth = "";
        theMonth += decipherChar(reminding, (submittedUID[16].ToString() + submittedUID[17]));
        theMonth += decipherChar(reminding, (submittedUID[18].ToString() + submittedUID[19]));
        string theDay = "";
        theDay += decipherChar(reminding, (submittedUID[20].ToString() + submittedUID[21]));
        theDay += decipherChar(reminding, (submittedUID[22].ToString() + submittedUID[23]));
        string theHour = "";
        theHour += decipherChar(reminding, (submittedUID[24].ToString() + submittedUID[25]));
        theHour += decipherChar(reminding, (submittedUID[26].ToString() + submittedUID[27]));
        string theMinute = "";
        theMinute += decipherChar(reminding, (submittedUID[28].ToString() + submittedUID[29]));
        theMinute += decipherChar(reminding, (submittedUID[30].ToString() + submittedUID[31]));
        string theMilliSecond = "";
        theMilliSecond += decipherChar(reminding, (submittedUID[36].ToString() + submittedUID[37]));
        theMilliSecond += decipherChar(reminding, (submittedUID[38].ToString() + submittedUID[39]));
        theMilliSecond += decipherChar(reminding, (submittedUID[40].ToString() + submittedUID[41]));

        return theYear + "-" + theMonth + "-" + theDay + " " + theHour + ":" + theMinute + ":" + second.ToString() + ":" + theMilliSecond;
    }

    private string encryptSecond(int second)
    {
        switch (second)
        {
            case 0: return "DJ";
            case 1: return "X6";
            case 2: return "yu";
            case 3: return "u1";
            case 4: return "nc";
            case 5: return "Gv";
            case 6: return "bs";
            case 7: return "Uq";
            case 8: return "t1";
            case 9: return "Sp";
            case 10: return "1u";
            case 11: return "QD";
            case 12: return "Ng";
            case 13: return "9A";
            case 14: return "o8";
            case 15: return "jF";
            case 16: return "7P";
            case 17: return "8k";
            case 18: return "qh";
            case 19: return "tD";
            case 20: return "oh";
            case 21: return "41";
            case 22: return "sm";
            case 23: return "Dt";
            case 24: return "5m";
            case 25: return "bR";
            case 26: return "a7";
            case 27: return "K6";
            case 28: return "R2";
            case 29: return "T8";
            case 30: return "P1";
            case 31: return "g2";
            case 32: return "K7";
            case 33: return "IA";
            case 34: return "26";
            case 35: return "47";
            case 36: return "jH";
            case 37: return "Fm";
            case 38: return "YL";
            case 39: return "tp";
            case 40: return "Vw";
            case 41: return "7W";
            case 42: return "2a";
            case 43: return "m9";
            case 44: return "F8";
            case 45: return "rX";
            case 46: return "8M";
            case 47: return "ci";
            case 48: return "cN";
            case 49: return "Qn";
            case 50: return "JI";
            case 51: return "kn";
            case 52: return "Eu";
            case 53: return "L0";
            case 54: return "33";
            case 55: return "F1";
            case 56: return "Cg";
            case 57: return "KX";
            case 58: return "Vv";
            case 59: return "78";
            default: return "00";
        }
    }
    private int decipherSecond(string decipher)
    {
        switch (decipher)
        {
            case "DJ": return 0;
            case "X6": return 1;
            case "yu": return 2;
            case "u1": return 3;
            case "nc": return 4;
            case "Gv": return 5;
            case "bs": return 6;
            case "Uq": return 7;
            case "t1": return 8;
            case "Sp": return 9;
            case "1u": return 10;
            case "QD": return 11;
            case "Ng": return 12;
            case "9A": return 13;
            case "o8": return 14;
            case "jF": return 15;
            case "7P": return 16;
            case "8k": return 17;
            case "qh": return 18;
            case "tD": return 19;
            case "oh": return 20;
            case "41": return 21;
            case "sm": return 22;
            case "Dt": return 23;
            case "5m": return 24;
            case "bR": return 25;
            case "a7": return 26;
            case "K6": return 27;
            case "R2": return 28;
            case "T8": return 29;
            case "P1": return 30;
            case "g2": return 31;
            case "K7": return 32;
            case "IA": return 33;
            case "26": return 34;
            case "47": return 35;
            case "jH": return 36;
            case "Fm": return 37;
            case "YL": return 38;
            case "tp": return 39;
            case "Vw": return 40;
            case "7W": return 41;
            case "2a": return 42;
            case "m9": return 43;
            case "F8": return 44;
            case "rX": return 45;
            case "8M": return 46;
            case "ci": return 47;
            case "cN": return 48;
            case "Qn": return 49;
            case "JI": return 50;
            case "kn": return 51;
            case "Eu": return 52;
            case "L0": return 53;
            case "33": return 54;
            case "F1": return 55;
            case "Cg": return 56;
            case "KX": return 57;
            case "Vv": return 58;
            case "78": return 59;
            default: return -1;
        }
    }

    private static string encryptChar(int remaining, char submittedChar)
    {
        if (remaining == 0)
        {
            switch (submittedChar)
            {
                case '0': return "3u";
                case '1': return "0a";
                case '2': return "gK";
                case '3': return "Q3";
                case '4': return "0J";
                case '5': return "0P";
                case '6': return "O9";
                case '7': return "QV";
                case '8': return "eC";
                case '9': return "I6";
            }
        }
        if (remaining == 1)
        {
            switch (submittedChar)
            {
                case '0': return "Tn";
                case '1': return "56";
                case '2': return "ep";
                case '3': return "60";
                case '4': return "bh";
                case '5': return "xr";
                case '6': return "KM";
                case '7': return "3s";
                case '8': return "Sx";
                case '9': return "y8";
            }
        }
        if (remaining == 2)
        {
            switch (submittedChar)
            {
                case '0': return "95";
                case '1': return "02";
                case '2': return "Xn";
                case '3': return "9w";
                case '4': return "O1";
                case '5': return "03";
                case '6': return "wc";
                case '7': return "s6";
                case '8': return "0J";
                case '9': return "NB";
            }
        }
        if (remaining == 3)
        {
            switch (submittedChar)
            {
                case '0': return "eA";
                case '1': return "y9";
                case '2': return "f5";
                case '3': return "3m";
                case '4': return "wW";
                case '5': return "BM";
                case '6': return "ly";
                case '7': return "1p";
                case '8': return "p9";
                case '9': return "aw";
            }
        }
        if (remaining == 4)
        {
            switch (submittedChar)
            {
                case '0': return "NU";
                case '1': return "Dd";
                case '2': return "KT";
                case '3': return "aA";
                case '4': return "dl";
                case '5': return "IR";
                case '6': return "36";
                case '7': return "vo";
                case '8': return "fW";
                case '9': return "md";
            }
        }
        if (remaining == 5)
        {
            switch (submittedChar)
            {
                case '0': return "h0";
                case '1': return "90";
                case '2': return "09";
                case '3': return "2u";
                case '4': return "KE";
                case '5': return "Bu";
                case '6': return "05";
                case '7': return "Oj";
                case '8': return "sa";
                case '9': return "AJ";
            }
        }
        if (remaining == 6)
        {
            switch (submittedChar)
            {
                case '0': return "g7";
                case '1': return "5C";
                case '2': return "lA";
                case '3': return "WY";
                case '4': return "24";
                case '5': return "P0";
                case '6': return "YE";
                case '7': return "r9";
                case '8': return "uO";
                case '9': return "33";
            }
        }
        if (remaining == 7)
        {
            switch (submittedChar)
            {
                case '0': return "fG";
                case '1': return "uv";
                case '2': return "j4";
                case '3': return "69";
                case '4': return "Su";
                case '5': return "62";
                case '6': return "bb";
                case '7': return "Ur";
                case '8': return "PT";
                case '9': return "Y1";
            }
        }
        if (remaining == 8)
        {
            switch (submittedChar)
            {
                case '0': return "3T";
                case '1': return "sg";
                case '2': return "1n";
                case '3': return "w7";
                case '4': return "kE";
                case '5': return "oJ";
                case '6': return "ch";
                case '7': return "u4";
                case '8': return "9G";
                case '9': return "28";
            }
        }
        if (remaining == 9)
        {
            switch (submittedChar)
            {
                case '0': return "P9";
                case '1': return "8C";
                case '2': return "9i";
                case '3': return "3g";
                case '4': return "W7";
                case '5': return "44";
                case '6': return "n5";
                case '7': return "uT";
                case '8': return "cj";
                case '9': return "77";
            }
        }
        return "";
    }
    private static int decipherChar(int remaining, string uniquePass)
    {

        if (remaining == 0)
        {
            switch (uniquePass)
            {
                case "3u": return 0;
                case "0a": return 1;
                case "gK": return 2;
                case "Q3": return 3;
                case "0J": return 4;
                case "0P": return 5;
                case "O9": return 6;
                case "QV": return 7;
                case "eC": return 8;
                case "I6": return 9;
            }
        }
        if (remaining == 1)
        {
            switch (uniquePass)
            {
                case "Tn": return 0;
                case "56": return 1;
                case "ep": return 2;
                case "60": return 3;
                case "bh": return 4;
                case "xr": return 5;
                case "KM": return 6;
                case "3s": return 7;
                case "Sx": return 8;
                case "y8": return 9;
            }
        }
        if (remaining == 2)
        {
            switch (uniquePass)
            {
                case "95": return 0;
                case "02": return 1;
                case "Xn": return 2;
                case "9w": return 3;
                case "O1": return 4;
                case "03": return 5;
                case "wc": return 6;
                case "s6": return 7;
                case "0J": return 8;
                case "NB": return 9;
            }
        }
        if (remaining == 3)
        {
            switch (uniquePass)
            {
                case "eA": return 0;
                case "y9": return 1;
                case "f5": return 2;
                case "3m": return 3;
                case "wW": return 4;
                case "BM": return 5;
                case "ly": return 6;
                case "1p": return 7;
                case "p9": return 8;
                case "aw": return 9;
            }
        }
        if (remaining == 4)
        {
            switch (uniquePass)
            {
                case "NU": return 0;
                case "Dd": return 1;
                case "KT": return 2;
                case "aA": return 3;
                case "dl": return 4;
                case "IR": return 5;
                case "36": return 6;
                case "vo": return 7;
                case "fW": return 8;
                case "md": return 9;
            }
        }
        if (remaining == 5)
        {
            switch (uniquePass)
            {
                case "h0": return 0;
                case "90": return 1;
                case "09": return 2;
                case "2u": return 3;
                case "KE": return 4;
                case "Bu": return 5;
                case "05": return 6;
                case "Oj": return 7;
                case "sa": return 8;
                case "AJ": return 9;
            }
        }
        if (remaining == 6)
        {
            switch (uniquePass)
            {
                case "g7": return 0;
                case "5C": return 1;
                case "lA": return 2;
                case "WY": return 3;
                case "24": return 4;
                case "P0": return 5;
                case "YE": return 6;
                case "r9": return 7;
                case "uO": return 8;
                case "33": return 9;
            }
        }
        if (remaining == 7)
        {
            switch (uniquePass)
            {
                case "fG": return 0;
                case "uv": return 1;
                case "j4": return 2;
                case "69": return 3;
                case "Su": return 4;
                case "62": return 5;
                case "bb": return 6;
                case "Ur": return 7;
                case "PT": return 8;
                case "Y1": return 9;
            }
        }
        if (remaining == 8)
        {
            switch (uniquePass)
            {
                case "3T": return 0;
                case "sg": return 1;
                case "1n": return 2;
                case "w7": return 3;
                case "kE": return 4;
                case "oJ": return 5;
                case "ch": return 6;
                case "u4": return 7;
                case "9G": return 8;
                case "28": return 9;
            }
        }
        if (remaining == 9)
        {
            switch (uniquePass)
            {
                case "P9": return 0;
                case "8C": return 1;
                case "9i": return 2;
                case "3g": return 3;
                case "W7": return 4;
                case "44": return 5;
                case "n5": return 6;
                case "uT": return 7;
                case "cj": return 8;
                case "77": return 9;
            }
        }

        return -1;
    }
    private static string getNowDateTimeUTC()
    {
        string now = DateTime.UtcNow.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss':'fff");
        return now;
    }
}