using System;
using System.Collections;

public class KardoEncryptionASCII//:IEquatable<KardoEncryptionASCII>
{
    private string submittedString;
    private string encryptedString;
    private string decipheredString;
    private int minChar;
    public KardoEncryptionASCII(string submittedString) 
    {
        this.submittedString = submittedString;
    }
    public KardoEncryptionASCII(string submittedString, int minChar)
    {
        this.submittedString = submittedString;
        this.minChar = minChar;
    }
    public KardoEncryptionASCII(string submittedString, string operation)
    {
        this.submittedString = submittedString;
        if (operation == "encrypt"){
            this.decipheredString = submittedString;
            startEncryption();
        }
        else if (operation == "decipher"){
            this.encryptedString = submittedString;
            startDeciphering();
        }

    }
    public KardoEncryptionASCII(string submittedString, string operation, int minChar)
    {
        this.submittedString = submittedString;
        this.minChar = minChar;
        if (operation == "encrypt")
        {
            this.decipheredString = submittedString;
            startEncryption();
        }
        else if (operation == "decipher")
        {
            this.encryptedString = submittedString;
            startDeciphering();
        }
    }
    /*public override bool Equals(Object obj)
    {
        return false;
    }*/
    public string getEncryptedString()
    {
        if (!String.IsNullOrEmpty(this.encryptedString))
            return encryptedString;
        else if (!String.IsNullOrEmpty(this.submittedString) && String.IsNullOrEmpty(this.encryptedString) && String.IsNullOrEmpty(this.decipheredString)) {
            this.decipheredString = this.submittedString;
            startEncryption();
        }

        return "KardoEncryptionError";
    }
    public string getDecipherString()
    {
        if (!String.IsNullOrEmpty(this.decipheredString))
            return this.decipheredString;
        else if (!String.IsNullOrEmpty(this.submittedString) && String.IsNullOrEmpty(this.decipheredString) && String.IsNullOrEmpty(this.encryptedString))
        {
            this.encryptedString = this.submittedString;
            startDeciphering();
            return this.decipheredString;
        }

        return "KardoEncryptionError";
    }
    private void startEncryption() 
    {
        if (!String.IsNullOrEmpty(this.decipheredString))
        {
            int currentSecond = Convert.ToInt32(DateTime.UtcNow.ToString("ss"));

            //Gönderilen stringi karakterlere ayırır ve ASII kodları array'e atılır.
            string[] charASIIArr = new string[decipheredString.Length];
            for (int i = 0; i < decipheredString.Length; i++)
                charASIIArr[i] = ((int)decipheredString[i]).ToString();

            encryptString(currentSecond, charASIIArr);
        }
    }
    private void encryptString(int second, string[] charASIIArr)
    {
        string stringIsEncrypted = "";
        Random random = new Random();
        
        stringIsEncrypted += encryptSecond(second); //is added information of seconds. It is first two char.

        foreach(string asii in charASIIArr)
        {
            if(stringIsEncrypted.Length > 2)
                stringIsEncrypted += encryptNumber(second, random.Next(40, 44));  //This is used to detect code of ascii 

            for (int i = 0; i < asii.Length; i++) {
                int theNumber = (Convert.ToInt32((asii[i].ToString())))*4;
                stringIsEncrypted += encryptNumber(second, random.Next(theNumber,theNumber+4)); // code of ascii is separated as numbers and it is converted encrypted char.
            }
        }
        if(this.minChar != 0)
        {
            if(stringIsEncrypted.Length < minChar)
            {
                //If number of min char is not enough, this function add random chars to string.
                stringIsEncrypted += encryptNumber(second, random.Next(44,48)); //random chars is added after that 
                int firstLength = stringIsEncrypted.Length;
                for (int i = 0; i < (minChar - firstLength); i++)
                    stringIsEncrypted += encryptNumber(random.Next(0, 60), random.Next(0, 48));
            }
        }

        this.encryptedString = stringIsEncrypted;
    }
    private void startDeciphering()
    {
        if (!String.IsNullOrEmpty(this.encryptedString))
        {
            int second = decipherSecond(encryptedString[0].ToString() + encryptedString[1]);
            ArrayList asciiEncArr = new ArrayList();
            string asciiEncStr = "", decipheredStr = "";
            if(second != -1)
            {
                for (int i = 2; i < encryptedString.Length; i++)
                {
                    int theNumber = decipherNumber(second, encryptedString[i]);
                    if (theNumber < 40 && theNumber > -1)
                        asciiEncStr += (int)(theNumber/4);
                    else if (theNumber > 38 && theNumber < 44)
                    {
                        asciiEncArr.Add(asciiEncStr);
                        asciiEncStr = "";
                    }
                    else if (theNumber > 43 && theNumber < 48)
                    {
                        asciiEncArr.Add(asciiEncStr);
                        break;
                    }
                    else
                        break;
                    if(i == encryptedString.Length-1)
                        asciiEncArr.Add(asciiEncStr);
                }
            }

            foreach(string ascii in asciiEncArr)
            {
                char theChar = (char)Convert.ToInt32(ascii);
                decipheredStr += theChar;
            }

            this.decipheredString = decipheredStr;
        } 
    }
    private string encryptSecond(int casePar)
    {
        switch (casePar)
        {
            case 0: return "x0";
            case 1: return "XN";
            case 2: return "Qt";
            case 3: return "qA";
            case 4: return "0f";
            case 5: return "fX";
            case 6: return "8q";
            case 7: return "3f";
            case 8: return "kM";
            case 9: return "vH";
            case 10: return "q4";
            case 11: return "QH";
            case 12: return "IB";
            case 13: return "tB";
            case 14: return "m4";
            case 15: return "5l";
            case 16: return "C3";
            case 17: return "1X";
            case 18: return "na";
            case 19: return "4s";
            case 20: return "xs";
            case 21: return "4Q";
            case 22: return "2N";
            case 23: return "Q8";
            case 24: return "mP";
            case 25: return "TU";
            case 26: return "24";
            case 27: return "V6";
            case 28: return "Lu";
            case 29: return "H1";
            case 30: return "wx";
            case 31: return "a6";
            case 32: return "jp";
            case 33: return "u7";
            case 34: return "yG";
            case 35: return "BQ";
            case 36: return "o6";
            case 37: return "l9";
            case 38: return "99";
            case 39: return "Br";
            case 40: return "5k";
            case 41: return "e9";
            case 42: return "bb";
            case 43: return "g0";
            case 44: return "59";
            case 45: return "sq";
            case 46: return "Y1";
            case 47: return "no";
            case 48: return "fM";
            case 49: return "Xd";
            case 50: return "xv";
            case 51: return "xB";
            case 52: return "8o";
            case 53: return "8n";
            case 54: return "WA";
            case 55: return "g8";
            case 56: return "vb";
            case 57: return "8P";
            case 58: return "5R";
            case 59: return "0N";
            default: return "";
        }
    }
    private int decipherSecond(string casePar)
    {
        switch (casePar)
        {
            case "x0": return 0;
            case "XN": return 1;
            case "Qt": return 2;
            case "qA": return 3;
            case "0f": return 4;
            case "fX": return 5;
            case "8q": return 6;
            case "3f": return 7;
            case "kM": return 8;
            case "vH": return 9;
            case "q4": return 10;
            case "QH": return 11;
            case "IB": return 12;
            case "tB": return 13;
            case "m4": return 14;
            case "5l": return 15;
            case "C3": return 16;
            case "1X": return 17;
            case "na": return 18;
            case "4s": return 19;
            case "xs": return 20;
            case "4Q": return 21;
            case "2N": return 22;
            case "Q8": return 23;
            case "mP": return 24;
            case "TU": return 25;
            case "24": return 26;
            case "V6": return 27;
            case "Lu": return 28;
            case "H1": return 29;
            case "wx": return 30;
            case "a6": return 31;
            case "jp": return 32;
            case "u7": return 33;
            case "yG": return 34;
            case "BQ": return 35;
            case "o6": return 36;
            case "l9": return 37;
            case "99": return 38;
            case "Br": return 39;
            case "5k": return 40;
            case "e9": return 41;
            case "bb": return 42;
            case "g0": return 43;
            case "59": return 44;
            case "sq": return 45;
            case "Y1": return 46;
            case "no": return 47;
            case "fM": return 48;
            case "Xd": return 49;
            case "xv": return 50;
            case "xB": return 51;
            case "8o": return 52;
            case "8n": return 53;
            case "WA": return 54;
            case "g8": return 55;
            case "vb": return 56;
            case "8P": return 57;
            case "5R": return 58;
            case "0N": return 59;
            default: return -1;
        }
    }

    private char encryptNumber(int second, int casePar) 
    {
        if (second == 0)
        {
            switch (casePar)
            {
                case 0: return 'v';
                case 1: return 'K';
                case 2: return '0';
                case 3: return 'r';
                case 4: return 'P';
                case 5: return 'g';
                case 6: return 'o';
                case 7: return 'a';
                case 8: return '8';
                case 9: return '7';
                case 10: return '5';
                case 11: return 'p';
                case 12: return 'I';
                case 13: return '2';
                case 14: return '9';
                case 15: return 'T';
                case 16: return '6';
                case 17: return 'D';
                case 18: return 'L';
                case 19: return 'J';
                case 20: return '1';
                case 21: return 'F';
                case 22: return 'h';
                case 23: return 'b';
                case 24: return '3';
                case 25: return '4';
                case 26: return 'n';
                case 27: return 'X';
                case 28: return 'O';
                case 29: return 'V';
                case 30: return 't';
                case 31: return 's';
                case 32: return 'W';
                case 33: return 'Q';
                case 34: return 'i';
                case 35: return 'N';
                case 36: return 'w';
                case 37: return 'U';
                case 38: return 'm';
                case 39: return 'B';
                case 40: return 'x';
                case 41: return 'l';
                case 42: return 'c';
                case 43: return 'k';
                case 44: return 'q';
                case 45: return 'S';
                case 46: return 'M';
                case 47: return 'C';
            }
        }
        if (second == 1)
        {
            switch (casePar)
            {
                case 0: return '4';
                case 1: return 'G';
                case 2: return 'y';
                case 3: return 'q';
                case 4: return 'C';
                case 5: return '5';
                case 6: return '8';
                case 7: return 'F';
                case 8: return 'A';
                case 9: return 'I';
                case 10: return '6';
                case 11: return 'X';
                case 12: return 'R';
                case 13: return 'h';
                case 14: return 'w';
                case 15: return 'S';
                case 16: return 'V';
                case 17: return 'M';
                case 18: return '1';
                case 19: return 'm';
                case 20: return 'Q';
                case 21: return 'Y';
                case 22: return 'r';
                case 23: return '9';
                case 24: return '0';
                case 25: return 'O';
                case 26: return 'T';
                case 27: return '2';
                case 28: return 'p';
                case 29: return '7';
                case 30: return 'j';
                case 31: return 'b';
                case 32: return 'i';
                case 33: return '3';
                case 34: return 'l';
                case 35: return 'B';
                case 36: return 'n';
                case 37: return 'f';
                case 38: return 'c';
                case 39: return 'a';
                case 40: return 'L';
                case 41: return 'P';
                case 42: return 'e';
                case 43: return 'g';
                case 44: return 'U';
                case 45: return 'H';
                case 46: return 'J';
                case 47: return 'K';
            }
        }
        if (second == 2)
        {
            switch (casePar)
            {
                case 0: return 'T';
                case 1: return 'B';
                case 2: return 'A';
                case 3: return '7';
                case 4: return 't';
                case 5: return 'u';
                case 6: return 'C';
                case 7: return 'm';
                case 8: return '9';
                case 9: return '5';
                case 10: return 'G';
                case 11: return '1';
                case 12: return 'W';
                case 13: return 'c';
                case 14: return 'Y';
                case 15: return 'P';
                case 16: return 'x';
                case 17: return 'K';
                case 18: return 'H';
                case 19: return 'w';
                case 20: return 'r';
                case 21: return 'R';
                case 22: return 'F';
                case 23: return 'd';
                case 24: return 'D';
                case 25: return '6';
                case 26: return 'V';
                case 27: return 'f';
                case 28: return 'S';
                case 29: return '2';
                case 30: return '3';
                case 31: return 'j';
                case 32: return 'y';
                case 33: return 's';
                case 34: return 'J';
                case 35: return '8';
                case 36: return 'g';
                case 37: return 'q';
                case 38: return 'n';
                case 39: return 'I';
                case 40: return 'E';
                case 41: return 'h';
                case 42: return 'o';
                case 43: return 'e';
                case 44: return 'v';
                case 45: return 'l';
                case 46: return 'U';
                case 47: return 'L';
            }
        }
        if (second == 3)
        {
            switch (casePar)
            {
                case 0: return 'S';
                case 1: return 'V';
                case 2: return '2';
                case 3: return 'n';
                case 4: return '8';
                case 5: return 'q';
                case 6: return 'v';
                case 7: return 'U';
                case 8: return 'R';
                case 9: return 'p';
                case 10: return 'E';
                case 11: return 'L';
                case 12: return 'o';
                case 13: return '0';
                case 14: return 'N';
                case 15: return 'K';
                case 16: return '4';
                case 17: return 'f';
                case 18: return 'X';
                case 19: return 'j';
                case 20: return 'J';
                case 21: return 'h';
                case 22: return 'P';
                case 23: return 'r';
                case 24: return 'i';
                case 25: return 'I';
                case 26: return 'F';
                case 27: return 'a';
                case 28: return '9';
                case 29: return 'u';
                case 30: return '5';
                case 31: return 'H';
                case 32: return 'k';
                case 33: return '3';
                case 34: return '7';
                case 35: return 'e';
                case 36: return 'D';
                case 37: return 'g';
                case 38: return 'M';
                case 39: return 'W';
                case 40: return 'l';
                case 41: return 'Y';
                case 42: return 'd';
                case 43: return '6';
                case 44: return '1';
                case 45: return 'w';
                case 46: return 'b';
                case 47: return 'x';
            }
        }
        if (second == 4)
        {
            switch (casePar)
            {
                case 0: return '9';
                case 1: return 'h';
                case 2: return 'c';
                case 3: return '7';
                case 4: return 'g';
                case 5: return 'k';
                case 6: return '2';
                case 7: return 'T';
                case 8: return 'F';
                case 9: return '3';
                case 10: return '6';
                case 11: return '4';
                case 12: return 'E';
                case 13: return 'o';
                case 14: return 'j';
                case 15: return 'f';
                case 16: return 'l';
                case 17: return 'b';
                case 18: return 'r';
                case 19: return 'p';
                case 20: return 'B';
                case 21: return 'd';
                case 22: return 'm';
                case 23: return 'V';
                case 24: return 'W';
                case 25: return 'Y';
                case 26: return 'G';
                case 27: return 'D';
                case 28: return 'P';
                case 29: return 'u';
                case 30: return '1';
                case 31: return 'K';
                case 32: return 'S';
                case 33: return 'X';
                case 34: return 'M';
                case 35: return 'O';
                case 36: return 'R';
                case 37: return 'i';
                case 38: return 't';
                case 39: return 'I';
                case 40: return 'e';
                case 41: return 'A';
                case 42: return 'n';
                case 43: return '5';
                case 44: return '0';
                case 45: return 'Q';
                case 46: return 'N';
                case 47: return 'v';
            }
        }
        if (second == 5)
        {
            switch (casePar)
            {
                case 0: return 'g';
                case 1: return '0';
                case 2: return 'p';
                case 3: return 'c';
                case 4: return 'd';
                case 5: return '6';
                case 6: return 'X';
                case 7: return 'Q';
                case 8: return '2';
                case 9: return 'L';
                case 10: return 'S';
                case 11: return 't';
                case 12: return 'G';
                case 13: return 's';
                case 14: return 'j';
                case 15: return '5';
                case 16: return 'a';
                case 17: return 'k';
                case 18: return 'U';
                case 19: return '9';
                case 20: return '7';
                case 21: return '8';
                case 22: return '4';
                case 23: return 'O';
                case 24: return 'P';
                case 25: return 'u';
                case 26: return 'o';
                case 27: return 'A';
                case 28: return 'R';
                case 29: return 'b';
                case 30: return 'f';
                case 31: return 'e';
                case 32: return 'D';
                case 33: return 'K';
                case 34: return 'Y';
                case 35: return 'I';
                case 36: return '3';
                case 37: return '1';
                case 38: return 'q';
                case 39: return 'w';
                case 40: return 'V';
                case 41: return 'i';
                case 42: return 'C';
                case 43: return 'n';
                case 44: return 'F';
                case 45: return 'J';
                case 46: return 'H';
                case 47: return 'B';
            }
        }
        if (second == 6)
        {
            switch (casePar)
            {
                case 0: return '6';
                case 1: return 'J';
                case 2: return 's';
                case 3: return '3';
                case 4: return 'f';
                case 5: return 'W';
                case 6: return '4';
                case 7: return '0';
                case 8: return 'y';
                case 9: return 'j';
                case 10: return 'G';
                case 11: return '5';
                case 12: return 'w';
                case 13: return 'D';
                case 14: return 'g';
                case 15: return 'S';
                case 16: return 'F';
                case 17: return 'L';
                case 18: return '7';
                case 19: return 'P';
                case 20: return 't';
                case 21: return 'H';
                case 22: return '8';
                case 23: return 'o';
                case 24: return 'N';
                case 25: return 'V';
                case 26: return 'R';
                case 27: return '1';
                case 28: return 'c';
                case 29: return 'Q';
                case 30: return 'A';
                case 31: return 'q';
                case 32: return 'Y';
                case 33: return 'i';
                case 34: return 'h';
                case 35: return 'r';
                case 36: return 'I';
                case 37: return '2';
                case 38: return 'n';
                case 39: return 'C';
                case 40: return 'O';
                case 41: return 'a';
                case 42: return 'B';
                case 43: return 'E';
                case 44: return 'M';
                case 45: return 'e';
                case 46: return 'u';
                case 47: return 'p';
            }
        }
        if (second == 7)
        {
            switch (casePar)
            {
                case 0: return '5';
                case 1: return 'c';
                case 2: return 'Y';
                case 3: return 'K';
                case 4: return 'N';
                case 5: return 'R';
                case 6: return 'S';
                case 7: return '1';
                case 8: return 'y';
                case 9: return '6';
                case 10: return 'h';
                case 11: return 'A';
                case 12: return '9';
                case 13: return 'F';
                case 14: return 'm';
                case 15: return 't';
                case 16: return 'L';
                case 17: return 'V';
                case 18: return 'r';
                case 19: return '4';
                case 20: return 'H';
                case 21: return 'd';
                case 22: return 'G';
                case 23: return 'W';
                case 24: return 'w';
                case 25: return 'U';
                case 26: return 'j';
                case 27: return 'E';
                case 28: return '3';
                case 29: return 'i';
                case 30: return 'X';
                case 31: return '2';
                case 32: return '7';
                case 33: return '8';
                case 34: return 'v';
                case 35: return 'B';
                case 36: return 'a';
                case 37: return 'I';
                case 38: return '0';
                case 39: return 'Q';
                case 40: return 'f';
                case 41: return 'b';
                case 42: return 'u';
                case 43: return 'l';
                case 44: return 'P';
                case 45: return 'D';
                case 46: return 'o';
                case 47: return 'O';
            }
        }
        if (second == 8)
        {
            switch (casePar)
            {
                case 0: return 'N';
                case 1: return 'y';
                case 2: return '6';
                case 3: return 'x';
                case 4: return 'r';
                case 5: return '1';
                case 6: return 'g';
                case 7: return '5';
                case 8: return 'M';
                case 9: return '8';
                case 10: return 'j';
                case 11: return '3';
                case 12: return 't';
                case 13: return 'a';
                case 14: return 'T';
                case 15: return 'E';
                case 16: return 'u';
                case 17: return 'o';
                case 18: return '7';
                case 19: return 'D';
                case 20: return 'R';
                case 21: return '4';
                case 22: return 'H';
                case 23: return 's';
                case 24: return 'm';
                case 25: return 'W';
                case 26: return 'L';
                case 27: return 'U';
                case 28: return '0';
                case 29: return 'V';
                case 30: return 'n';
                case 31: return 'v';
                case 32: return 'p';
                case 33: return 'q';
                case 34: return 'f';
                case 35: return 'Y';
                case 36: return 'X';
                case 37: return 'd';
                case 38: return 'K';
                case 39: return 'P';
                case 40: return 'I';
                case 41: return 'A';
                case 42: return 'l';
                case 43: return 'G';
                case 44: return '2';
                case 45: return 'w';
                case 46: return 'b';
                case 47: return 'J';
            }
        }
        if (second == 9)
        {
            switch (casePar)
            {
                case 0: return 'l';
                case 1: return 'd';
                case 2: return '0';
                case 3: return 'R';
                case 4: return 'B';
                case 5: return 'k';
                case 6: return 'a';
                case 7: return '7';
                case 8: return '4';
                case 9: return 'v';
                case 10: return '5';
                case 11: return 'q';
                case 12: return '2';
                case 13: return 'r';
                case 14: return '8';
                case 15: return 'c';
                case 16: return 'E';
                case 17: return 'J';
                case 18: return 's';
                case 19: return 'w';
                case 20: return 'h';
                case 21: return 'j';
                case 22: return '6';
                case 23: return 'S';
                case 24: return 'f';
                case 25: return 'X';
                case 26: return 'L';
                case 27: return 'W';
                case 28: return 'O';
                case 29: return 'K';
                case 30: return 'b';
                case 31: return 'Q';
                case 32: return 'm';
                case 33: return 'p';
                case 34: return 'V';
                case 35: return '3';
                case 36: return 'T';
                case 37: return 'D';
                case 38: return '1';
                case 39: return '9';
                case 40: return 't';
                case 41: return 'M';
                case 42: return 'H';
                case 43: return 'P';
                case 44: return 'N';
                case 45: return 'n';
                case 46: return 'Y';
                case 47: return 'g';
            }
        }
        if (second == 10)
        {
            switch (casePar)
            {
                case 0: return 'u';
                case 1: return '2';
                case 2: return 'l';
                case 3: return '8';
                case 4: return 'q';
                case 5: return 'e';
                case 6: return 'O';
                case 7: return '3';
                case 8: return 'T';
                case 9: return 'V';
                case 10: return 'F';
                case 11: return '7';
                case 12: return 'n';
                case 13: return '9';
                case 14: return 'w';
                case 15: return '0';
                case 16: return 'b';
                case 17: return 'm';
                case 18: return 's';
                case 19: return 'A';
                case 20: return '1';
                case 21: return 'U';
                case 22: return 'R';
                case 23: return 'I';
                case 24: return '5';
                case 25: return 'v';
                case 26: return 'Q';
                case 27: return 'W';
                case 28: return 'X';
                case 29: return 'C';
                case 30: return 'h';
                case 31: return '6';
                case 32: return 'H';
                case 33: return 'r';
                case 34: return 'Y';
                case 35: return 'M';
                case 36: return 'P';
                case 37: return 'g';
                case 38: return 'y';
                case 39: return '4';
                case 40: return 'E';
                case 41: return 'L';
                case 42: return 'S';
                case 43: return 'K';
                case 44: return 'f';
                case 45: return 'B';
                case 46: return 'J';
                case 47: return 'G';
            }
        }
        if (second == 11)
        {
            switch (casePar)
            {
                case 0: return '7';
                case 1: return '2';
                case 2: return 'p';
                case 3: return 'A';
                case 4: return 'r';
                case 5: return '4';
                case 6: return '0';
                case 7: return '8';
                case 8: return 'I';
                case 9: return 'd';
                case 10: return '3';
                case 11: return 'f';
                case 12: return 'F';
                case 13: return 'n';
                case 14: return 'Q';
                case 15: return 'x';
                case 16: return 'U';
                case 17: return 'v';
                case 18: return '9';
                case 19: return 'O';
                case 20: return 'c';
                case 21: return '6';
                case 22: return 'Y';
                case 23: return 'R';
                case 24: return 'o';
                case 25: return 'B';
                case 26: return 'E';
                case 27: return 'W';
                case 28: return '1';
                case 29: return 'S';
                case 30: return 'k';
                case 31: return 'D';
                case 32: return 'l';
                case 33: return 'q';
                case 34: return 'm';
                case 35: return 'i';
                case 36: return 'X';
                case 37: return 'T';
                case 38: return 'y';
                case 39: return 'g';
                case 40: return 'b';
                case 41: return 'P';
                case 42: return 'u';
                case 43: return 'j';
                case 44: return '5';
                case 45: return 'G';
                case 46: return 'K';
                case 47: return 'C';
            }
        }
        if (second == 12)
        {
            switch (casePar)
            {
                case 0: return 'N';
                case 1: return '1';
                case 2: return 'i';
                case 3: return 'K';
                case 4: return 'o';
                case 5: return '2';
                case 6: return 'r';
                case 7: return '6';
                case 8: return 'l';
                case 9: return 'd';
                case 10: return '3';
                case 11: return '8';
                case 12: return 'M';
                case 13: return 'w';
                case 14: return 'T';
                case 15: return '0';
                case 16: return '4';
                case 17: return 't';
                case 18: return '5';
                case 19: return 'f';
                case 20: return 'q';
                case 21: return '9';
                case 22: return 'n';
                case 23: return 'u';
                case 24: return 'y';
                case 25: return 'L';
                case 26: return 'b';
                case 27: return 'c';
                case 28: return 'G';
                case 29: return 'I';
                case 30: return 'H';
                case 31: return 'U';
                case 32: return '7';
                case 33: return 'J';
                case 34: return 'R';
                case 35: return 'v';
                case 36: return 'B';
                case 37: return 'a';
                case 38: return 'D';
                case 39: return 'x';
                case 40: return 'V';
                case 41: return 's';
                case 42: return 'F';
                case 43: return 'p';
                case 44: return 'm';
                case 45: return 'A';
                case 46: return 'j';
                case 47: return 'Y';
            }
        }
        if (second == 13)
        {
            switch (casePar)
            {
                case 0: return 'J';
                case 1: return 't';
                case 2: return 'V';
                case 3: return '9';
                case 4: return '5';
                case 5: return 'M';
                case 6: return '8';
                case 7: return 'Y';
                case 8: return 'B';
                case 9: return 'W';
                case 10: return 'U';
                case 11: return 'y';
                case 12: return '0';
                case 13: return 'e';
                case 14: return '6';
                case 15: return 'D';
                case 16: return 'x';
                case 17: return 'v';
                case 18: return '3';
                case 19: return 'K';
                case 20: return 'E';
                case 21: return 'p';
                case 22: return 'S';
                case 23: return 'w';
                case 24: return '4';
                case 25: return 'q';
                case 26: return 'N';
                case 27: return 'Q';
                case 28: return '7';
                case 29: return '2';
                case 30: return 'P';
                case 31: return 'C';
                case 32: return 'H';
                case 33: return 'k';
                case 34: return 'T';
                case 35: return 'j';
                case 36: return 'I';
                case 37: return 'a';
                case 38: return 'h';
                case 39: return 'G';
                case 40: return 'b';
                case 41: return 's';
                case 42: return 'o';
                case 43: return 'A';
                case 44: return 'n';
                case 45: return 'X';
                case 46: return '1';
                case 47: return 'F';
            }
        }
        if (second == 14)
        {
            switch (casePar)
            {
                case 0: return '4';
                case 1: return 'E';
                case 2: return '9';
                case 3: return 'G';
                case 4: return '0';
                case 5: return 'l';
                case 6: return 'D';
                case 7: return 'k';
                case 8: return 'Y';
                case 9: return 'o';
                case 10: return 'u';
                case 11: return 'p';
                case 12: return '8';
                case 13: return '1';
                case 14: return 'H';
                case 15: return '7';
                case 16: return '5';
                case 17: return 'T';
                case 18: return 'c';
                case 19: return '6';
                case 20: return 'q';
                case 21: return 'm';
                case 22: return '2';
                case 23: return 'h';
                case 24: return 'y';
                case 25: return 'J';
                case 26: return 'C';
                case 27: return 'S';
                case 28: return 'e';
                case 29: return 'W';
                case 30: return 'd';
                case 31: return 'v';
                case 32: return 'r';
                case 33: return 'L';
                case 34: return 'V';
                case 35: return 'b';
                case 36: return 'g';
                case 37: return 'K';
                case 38: return 'Q';
                case 39: return 'w';
                case 40: return 'j';
                case 41: return 'F';
                case 42: return 'B';
                case 43: return 'N';
                case 44: return 't';
                case 45: return 'X';
                case 46: return 'M';
                case 47: return 'U';
            }
        }
        if (second == 15)
        {
            switch (casePar)
            {
                case 0: return 'o';
                case 1: return 'R';
                case 2: return 'q';
                case 3: return 'g';
                case 4: return '7';
                case 5: return '8';
                case 6: return 'n';
                case 7: return 'd';
                case 8: return 'x';
                case 9: return 'A';
                case 10: return 's';
                case 11: return '4';
                case 12: return '6';
                case 13: return '9';
                case 14: return '5';
                case 15: return 'S';
                case 16: return '1';
                case 17: return 'H';
                case 18: return '0';
                case 19: return 'X';
                case 20: return 'u';
                case 21: return 'O';
                case 22: return 'Y';
                case 23: return 'e';
                case 24: return 'I';
                case 25: return 'E';
                case 26: return '3';
                case 27: return 'K';
                case 28: return 'L';
                case 29: return 'T';
                case 30: return 'l';
                case 31: return 'y';
                case 32: return '2';
                case 33: return 'Q';
                case 34: return 'm';
                case 35: return 'v';
                case 36: return 'b';
                case 37: return 'i';
                case 38: return 'F';
                case 39: return 'N';
                case 40: return 'k';
                case 41: return 't';
                case 42: return 'h';
                case 43: return 'j';
                case 44: return 'a';
                case 45: return 'J';
                case 46: return 'P';
                case 47: return 'B';
            }
        }
        if (second == 16)
        {
            switch (casePar)
            {
                case 0: return '5';
                case 1: return 'V';
                case 2: return 'j';
                case 3: return 'o';
                case 4: return '8';
                case 5: return '3';
                case 6: return 'P';
                case 7: return 'G';
                case 8: return '4';
                case 9: return 'A';
                case 10: return 'Q';
                case 11: return 'r';
                case 12: return '0';
                case 13: return '7';
                case 14: return 'I';
                case 15: return 'e';
                case 16: return 'c';
                case 17: return 'F';
                case 18: return 'J';
                case 19: return 'y';
                case 20: return 'X';
                case 21: return 'O';
                case 22: return 'L';
                case 23: return 'a';
                case 24: return 'g';
                case 25: return '1';
                case 26: return 'q';
                case 27: return 'R';
                case 28: return 'K';
                case 29: return 'C';
                case 30: return 'v';
                case 31: return 'E';
                case 32: return 'k';
                case 33: return 'U';
                case 34: return 't';
                case 35: return 's';
                case 36: return 'p';
                case 37: return '2';
                case 38: return 'T';
                case 39: return 'Y';
                case 40: return 'N';
                case 41: return 'u';
                case 42: return 'S';
                case 43: return 'h';
                case 44: return 'i';
                case 45: return 'b';
                case 46: return 'H';
                case 47: return '9';
            }
        }
        if (second == 17)
        {
            switch (casePar)
            {
                case 0: return 'j';
                case 1: return 'n';
                case 2: return '7';
                case 3: return 'B';
                case 4: return '6';
                case 5: return '1';
                case 6: return '4';
                case 7: return 'Q';
                case 8: return 'L';
                case 9: return 'E';
                case 10: return '5';
                case 11: return 'p';
                case 12: return 'U';
                case 13: return 'M';
                case 14: return 'I';
                case 15: return 'P';
                case 16: return '3';
                case 17: return '0';
                case 18: return '9';
                case 19: return 'H';
                case 20: return 's';
                case 21: return 'h';
                case 22: return 'v';
                case 23: return 'o';
                case 24: return 'x';
                case 25: return 'O';
                case 26: return 'k';
                case 27: return 'q';
                case 28: return '8';
                case 29: return 'y';
                case 30: return '2';
                case 31: return 'A';
                case 32: return 'r';
                case 33: return 'f';
                case 34: return 'K';
                case 35: return 'S';
                case 36: return 'N';
                case 37: return 'g';
                case 38: return 'd';
                case 39: return 'a';
                case 40: return 'c';
                case 41: return 'e';
                case 42: return 'l';
                case 43: return 'T';
                case 44: return 'F';
                case 45: return 'D';
                case 46: return 'R';
                case 47: return 't';
            }
        }
        if (second == 18)
        {
            switch (casePar)
            {
                case 0: return 'W';
                case 1: return 'c';
                case 2: return '2';
                case 3: return 'f';
                case 4: return '3';
                case 5: return 'm';
                case 6: return 'x';
                case 7: return 'A';
                case 8: return '9';
                case 9: return '7';
                case 10: return 'g';
                case 11: return 'Q';
                case 12: return 'l';
                case 13: return 'Y';
                case 14: return 'L';
                case 15: return 'P';
                case 16: return '8';
                case 17: return 'K';
                case 18: return 'V';
                case 19: return 'N';
                case 20: return 'M';
                case 21: return '6';
                case 22: return 'u';
                case 23: return 'q';
                case 24: return 'D';
                case 25: return '5';
                case 26: return 't';
                case 27: return 'e';
                case 28: return 'o';
                case 29: return 'w';
                case 30: return '4';
                case 31: return 'h';
                case 32: return 'E';
                case 33: return 'v';
                case 34: return 'k';
                case 35: return 'i';
                case 36: return 'U';
                case 37: return '0';
                case 38: return '1';
                case 39: return 'O';
                case 40: return 'n';
                case 41: return 'I';
                case 42: return 'p';
                case 43: return 'C';
                case 44: return 'b';
                case 45: return 's';
                case 46: return 'j';
                case 47: return 'r';
            }
        }
        if (second == 19)
        {
            switch (casePar)
            {
                case 0: return '2';
                case 1: return 'o';
                case 2: return '0';
                case 3: return '5';
                case 4: return 'a';
                case 5: return 'K';
                case 6: return '9';
                case 7: return 'b';
                case 8: return 'G';
                case 9: return 'I';
                case 10: return 'r';
                case 11: return 'H';
                case 12: return 'Q';
                case 13: return 'L';
                case 14: return 's';
                case 15: return 'P';
                case 16: return 'F';
                case 17: return 'M';
                case 18: return 'e';
                case 19: return 'B';
                case 20: return 'U';
                case 21: return '6';
                case 22: return 'u';
                case 23: return '4';
                case 24: return '8';
                case 25: return 'w';
                case 26: return 'N';
                case 27: return 'p';
                case 28: return 'v';
                case 29: return 'R';
                case 30: return 'O';
                case 31: return 't';
                case 32: return 'D';
                case 33: return 'k';
                case 34: return '3';
                case 35: return 'T';
                case 36: return 'Y';
                case 37: return 'c';
                case 38: return '1';
                case 39: return 'V';
                case 40: return 'g';
                case 41: return 'n';
                case 42: return '7';
                case 43: return 'W';
                case 44: return 'm';
                case 45: return 'J';
                case 46: return 'E';
                case 47: return 'd';
            }
        }
        if (second == 20)
        {
            switch (casePar)
            {
                case 0: return '3';
                case 1: return 'A';
                case 2: return 'C';
                case 3: return 'H';
                case 4: return 'v';
                case 5: return 'F';
                case 6: return 'T';
                case 7: return 'a';
                case 8: return 'w';
                case 9: return 'P';
                case 10: return 'y';
                case 11: return 'V';
                case 12: return 'D';
                case 13: return 'I';
                case 14: return 'J';
                case 15: return 'B';
                case 16: return '5';
                case 17: return '7';
                case 18: return 'L';
                case 19: return 'g';
                case 20: return '2';
                case 21: return 'S';
                case 22: return 'i';
                case 23: return 'o';
                case 24: return '9';
                case 25: return 'm';
                case 26: return 'j';
                case 27: return 'R';
                case 28: return 't';
                case 29: return '4';
                case 30: return '0';
                case 31: return 'l';
                case 32: return 'k';
                case 33: return 'G';
                case 34: return 'M';
                case 35: return '8';
                case 36: return 'b';
                case 37: return 'Y';
                case 38: return 'u';
                case 39: return 'f';
                case 40: return '1';
                case 41: return 'O';
                case 42: return 'c';
                case 43: return '6';
                case 44: return 'q';
                case 45: return 'x';
                case 46: return 'r';
                case 47: return 'h';
            }
        }
        if (second == 21)
        {
            switch (casePar)
            {
                case 0: return '2';
                case 1: return '9';
                case 2: return 'd';
                case 3: return 'T';
                case 4: return 'H';
                case 5: return 'j';
                case 6: return 'm';
                case 7: return 'x';
                case 8: return 'W';
                case 9: return 'B';
                case 10: return 'M';
                case 11: return 'f';
                case 12: return 'o';
                case 13: return 'c';
                case 14: return 'Q';
                case 15: return 'w';
                case 16: return 'I';
                case 17: return '6';
                case 18: return 'r';
                case 19: return 'N';
                case 20: return 'J';
                case 21: return 'C';
                case 22: return 'i';
                case 23: return '4';
                case 24: return '3';
                case 25: return '7';
                case 26: return 'U';
                case 27: return 'R';
                case 28: return 'O';
                case 29: return 'D';
                case 30: return '0';
                case 31: return '1';
                case 32: return 'Y';
                case 33: return 'L';
                case 34: return 'y';
                case 35: return 'E';
                case 36: return 'g';
                case 37: return 'S';
                case 38: return 't';
                case 39: return 'F';
                case 40: return '5';
                case 41: return 'u';
                case 42: return 'A';
                case 43: return '8';
                case 44: return 's';
                case 45: return 'V';
                case 46: return 'v';
                case 47: return 'k';
            }
        }
        if (second == 22)
        {
            switch (casePar)
            {
                case 0: return '6';
                case 1: return 'P';
                case 2: return 'I';
                case 3: return '7';
                case 4: return 'F';
                case 5: return 't';
                case 6: return 'u';
                case 7: return 'o';
                case 8: return 'n';
                case 9: return '1';
                case 10: return '9';
                case 11: return 'r';
                case 12: return 'k';
                case 13: return '0';
                case 14: return 'p';
                case 15: return 'l';
                case 16: return 'y';
                case 17: return 'R';
                case 18: return 'j';
                case 19: return '5';
                case 20: return '3';
                case 21: return 'b';
                case 22: return 'q';
                case 23: return 'i';
                case 24: return '8';
                case 25: return 'C';
                case 26: return '4';
                case 27: return 'E';
                case 28: return 'G';
                case 29: return 'X';
                case 30: return 'H';
                case 31: return 'Y';
                case 32: return 'S';
                case 33: return 'w';
                case 34: return '2';
                case 35: return 'Q';
                case 36: return 'd';
                case 37: return 'W';
                case 38: return 'a';
                case 39: return 'B';
                case 40: return 'g';
                case 41: return 'N';
                case 42: return 'h';
                case 43: return 'J';
                case 44: return 'U';
                case 45: return 'v';
                case 46: return 'f';
                case 47: return 'T';
            }
        }
        if (second == 23)
        {
            switch (casePar)
            {
                case 0: return '9';
                case 1: return 'k';
                case 2: return 'U';
                case 3: return 'i';
                case 4: return 'G';
                case 5: return 'l';
                case 6: return 'p';
                case 7: return 'y';
                case 8: return '5';
                case 9: return '6';
                case 10: return 'b';
                case 11: return 'q';
                case 12: return 't';
                case 13: return '4';
                case 14: return '7';
                case 15: return '2';
                case 16: return 'm';
                case 17: return 'g';
                case 18: return 'Q';
                case 19: return 'I';
                case 20: return 'h';
                case 21: return 'R';
                case 22: return 'M';
                case 23: return 'L';
                case 24: return 'S';
                case 25: return 'n';
                case 26: return 'X';
                case 27: return 'u';
                case 28: return 'T';
                case 29: return 'K';
                case 30: return 'x';
                case 31: return 'H';
                case 32: return 'E';
                case 33: return 'o';
                case 34: return '8';
                case 35: return 'Y';
                case 36: return 'V';
                case 37: return '0';
                case 38: return 'P';
                case 39: return 'B';
                case 40: return 'e';
                case 41: return 'O';
                case 42: return 'D';
                case 43: return '1';
                case 44: return 'f';
                case 45: return 'r';
                case 46: return 'W';
                case 47: return 'J';
            }
        }
        if (second == 24)
        {
            switch (casePar)
            {
                case 0: return 'p';
                case 1: return 'd';
                case 2: return 'r';
                case 3: return 'A';
                case 4: return '2';
                case 5: return '0';
                case 6: return 'C';
                case 7: return 's';
                case 8: return 'R';
                case 9: return 'D';
                case 10: return 'E';
                case 11: return 'x';
                case 12: return 'Y';
                case 13: return 'K';
                case 14: return 'b';
                case 15: return 'f';
                case 16: return '3';
                case 17: return '9';
                case 18: return 'Q';
                case 19: return 'X';
                case 20: return 'n';
                case 21: return 'l';
                case 22: return 'k';
                case 23: return 'g';
                case 24: return '4';
                case 25: return '1';
                case 26: return '5';
                case 27: return 'L';
                case 28: return 'w';
                case 29: return 'T';
                case 30: return 'v';
                case 31: return 'y';
                case 32: return 'c';
                case 33: return 'i';
                case 34: return 'P';
                case 35: return '6';
                case 36: return 'O';
                case 37: return 'u';
                case 38: return 'F';
                case 39: return '8';
                case 40: return 't';
                case 41: return 'h';
                case 42: return 'G';
                case 43: return 'U';
                case 44: return 'M';
                case 45: return 'a';
                case 46: return 'o';
                case 47: return '7';
            }
        }
        if (second == 25)
        {
            switch (casePar)
            {
                case 0: return 'n';
                case 1: return 'u';
                case 2: return 'S';
                case 3: return '5';
                case 4: return '4';
                case 5: return '3';
                case 6: return '1';
                case 7: return 'a';
                case 8: return 'm';
                case 9: return 'M';
                case 10: return '8';
                case 11: return 'R';
                case 12: return 'v';
                case 13: return '7';
                case 14: return 'L';
                case 15: return 'b';
                case 16: return 'P';
                case 17: return 'w';
                case 18: return 'J';
                case 19: return 'Y';
                case 20: return 'D';
                case 21: return '6';
                case 22: return 'x';
                case 23: return 'e';
                case 24: return 'r';
                case 25: return 'j';
                case 26: return 't';
                case 27: return 'G';
                case 28: return 'N';
                case 29: return 'A';
                case 30: return '9';
                case 31: return 'F';
                case 32: return '2';
                case 33: return 'E';
                case 34: return 'k';
                case 35: return 'X';
                case 36: return 'y';
                case 37: return 'K';
                case 38: return 'f';
                case 39: return 'U';
                case 40: return 'O';
                case 41: return 'H';
                case 42: return 'p';
                case 43: return 'V';
                case 44: return '0';
                case 45: return 'I';
                case 46: return 'W';
                case 47: return 'h';
            }
        }
        if (second == 26)
        {
            switch (casePar)
            {
                case 0: return 'C';
                case 1: return 'R';
                case 2: return 'u';
                case 3: return 'F';
                case 4: return '0';
                case 5: return 'c';
                case 6: return 'K';
                case 7: return 'M';
                case 8: return 'n';
                case 9: return 't';
                case 10: return 's';
                case 11: return 'H';
                case 12: return 'i';
                case 13: return 'h';
                case 14: return '2';
                case 15: return '1';
                case 16: return 'p';
                case 17: return '8';
                case 18: return 'a';
                case 19: return '3';
                case 20: return 'g';
                case 21: return 'r';
                case 22: return 'W';
                case 23: return '7';
                case 24: return 'q';
                case 25: return 'V';
                case 26: return 'x';
                case 27: return '6';
                case 28: return 'L';
                case 29: return 'Y';
                case 30: return '9';
                case 31: return 'B';
                case 32: return 'f';
                case 33: return 'Q';
                case 34: return 'y';
                case 35: return '4';
                case 36: return 'I';
                case 37: return 'G';
                case 38: return 'e';
                case 39: return '5';
                case 40: return 'N';
                case 41: return 'U';
                case 42: return 'k';
                case 43: return 'S';
                case 44: return 'D';
                case 45: return 'P';
                case 46: return 'O';
                case 47: return 'v';
            }
        }
        if (second == 27)
        {
            switch (casePar)
            {
                case 0: return 'O';
                case 1: return '6';
                case 2: return 'C';
                case 3: return 'k';
                case 4: return 'W';
                case 5: return 'j';
                case 6: return 'v';
                case 7: return '7';
                case 8: return 'T';
                case 9: return 'm';
                case 10: return 'b';
                case 11: return 'e';
                case 12: return 'h';
                case 13: return '3';
                case 14: return '8';
                case 15: return '0';
                case 16: return '4';
                case 17: return 'P';
                case 18: return 'f';
                case 19: return 'V';
                case 20: return 'x';
                case 21: return '1';
                case 22: return '5';
                case 23: return '9';
                case 24: return 'n';
                case 25: return 'F';
                case 26: return 'w';
                case 27: return 'd';
                case 28: return 'a';
                case 29: return 'A';
                case 30: return 'G';
                case 31: return 's';
                case 32: return 'E';
                case 33: return 'M';
                case 34: return 'N';
                case 35: return 'u';
                case 36: return 'r';
                case 37: return 'c';
                case 38: return 'J';
                case 39: return 'D';
                case 40: return '2';
                case 41: return 'q';
                case 42: return 'i';
                case 43: return 'U';
                case 44: return 'R';
                case 45: return 'g';
                case 46: return 'H';
                case 47: return 'K';
            }
        }
        if (second == 28)
        {
            switch (casePar)
            {
                case 0: return '4';
                case 1: return 'S';
                case 2: return 'n';
                case 3: return '7';
                case 4: return 'O';
                case 5: return 'U';
                case 6: return '3';
                case 7: return '2';
                case 8: return 'y';
                case 9: return 'Q';
                case 10: return 'e';
                case 11: return 'X';
                case 12: return 'L';
                case 13: return 'D';
                case 14: return 'V';
                case 15: return 'g';
                case 16: return 'T';
                case 17: return 'r';
                case 18: return 'h';
                case 19: return '6';
                case 20: return 'M';
                case 21: return 'K';
                case 22: return '5';
                case 23: return 'F';
                case 24: return '8';
                case 25: return '1';
                case 26: return 'j';
                case 27: return 'd';
                case 28: return 'x';
                case 29: return 'R';
                case 30: return 'H';
                case 31: return 'J';
                case 32: return 'q';
                case 33: return 'w';
                case 34: return 'v';
                case 35: return 'C';
                case 36: return '0';
                case 37: return 'm';
                case 38: return 'A';
                case 39: return 'l';
                case 40: return 'u';
                case 41: return 'o';
                case 42: return 'E';
                case 43: return '9';
                case 44: return 'a';
                case 45: return 'k';
                case 46: return 'Y';
                case 47: return 'b';
            }
        }
        if (second == 29)
        {
            switch (casePar)
            {
                case 0: return 'l';
                case 1: return 'L';
                case 2: return 'U';
                case 3: return 'p';
                case 4: return 'G';
                case 5: return 'g';
                case 6: return '9';
                case 7: return 'r';
                case 8: return 'W';
                case 9: return 'H';
                case 10: return 'N';
                case 11: return 'a';
                case 12: return 'R';
                case 13: return 'I';
                case 14: return '5';
                case 15: return 'h';
                case 16: return '8';
                case 17: return 'D';
                case 18: return 'j';
                case 19: return 'd';
                case 20: return 'J';
                case 21: return 'v';
                case 22: return 'u';
                case 23: return 'i';
                case 24: return '0';
                case 25: return 'Y';
                case 26: return 'F';
                case 27: return '3';
                case 28: return 'O';
                case 29: return 'n';
                case 30: return '4';
                case 31: return '1';
                case 32: return '7';
                case 33: return 'A';
                case 34: return 'y';
                case 35: return 'c';
                case 36: return 'V';
                case 37: return 'x';
                case 38: return 'X';
                case 39: return 'Q';
                case 40: return '2';
                case 41: return 'K';
                case 42: return 'M';
                case 43: return 't';
                case 44: return 'm';
                case 45: return 'C';
                case 46: return 'E';
                case 47: return '6';
            }
        }
        if (second == 30)
        {
            switch (casePar)
            {
                case 0: return 'f';
                case 1: return '3';
                case 2: return 'G';
                case 3: return 'i';
                case 4: return '7';
                case 5: return 'x';
                case 6: return 'n';
                case 7: return 'J';
                case 8: return '0';
                case 9: return 'O';
                case 10: return 'Q';
                case 11: return 'b';
                case 12: return '2';
                case 13: return 'v';
                case 14: return 'W';
                case 15: return 'C';
                case 16: return '6';
                case 17: return '1';
                case 18: return 'k';
                case 19: return 'U';
                case 20: return 'Y';
                case 21: return 'P';
                case 22: return 'V';
                case 23: return 'g';
                case 24: return 'c';
                case 25: return 'H';
                case 26: return '5';
                case 27: return '9';
                case 28: return '4';
                case 29: return 'd';
                case 30: return 'j';
                case 31: return 'e';
                case 32: return 'u';
                case 33: return 'w';
                case 34: return 'D';
                case 35: return 'A';
                case 36: return 'm';
                case 37: return 'p';
                case 38: return 'r';
                case 39: return '8';
                case 40: return 'L';
                case 41: return 'B';
                case 42: return 'X';
                case 43: return 'o';
                case 44: return 'l';
                case 45: return 'y';
                case 46: return 'R';
                case 47: return 'K';
            }
        }
        if (second == 31)
        {
            switch (casePar)
            {
                case 0: return '4';
                case 1: return '1';
                case 2: return '0';
                case 3: return 'o';
                case 4: return 'O';
                case 5: return 'D';
                case 6: return 'g';
                case 7: return 'f';
                case 8: return 'h';
                case 9: return 'R';
                case 10: return 'n';
                case 11: return 'p';
                case 12: return 'S';
                case 13: return 'a';
                case 14: return 'F';
                case 15: return 'J';
                case 16: return 'e';
                case 17: return 'l';
                case 18: return 'Q';
                case 19: return 'u';
                case 20: return 'j';
                case 21: return '8';
                case 22: return 'Y';
                case 23: return '7';
                case 24: return 'M';
                case 25: return 'A';
                case 26: return 's';
                case 27: return '6';
                case 28: return 'C';
                case 29: return 'I';
                case 30: return 'X';
                case 31: return '5';
                case 32: return 'b';
                case 33: return 'q';
                case 34: return 'y';
                case 35: return '3';
                case 36: return '9';
                case 37: return 'x';
                case 38: return 'T';
                case 39: return 'v';
                case 40: return 'H';
                case 41: return 'r';
                case 42: return 'm';
                case 43: return 'N';
                case 44: return 't';
                case 45: return 'K';
                case 46: return '2';
                case 47: return 'V';
            }
        }
        if (second == 32)
        {
            switch (casePar)
            {
                case 0: return 'g';
                case 1: return '9';
                case 2: return 'S';
                case 3: return 'M';
                case 4: return 'V';
                case 5: return '3';
                case 6: return 'n';
                case 7: return 'q';
                case 8: return 'X';
                case 9: return 'a';
                case 10: return '5';
                case 11: return 'v';
                case 12: return 'Y';
                case 13: return 'G';
                case 14: return '0';
                case 15: return 'e';
                case 16: return '1';
                case 17: return 'c';
                case 18: return 'f';
                case 19: return '7';
                case 20: return '8';
                case 21: return '2';
                case 22: return 'm';
                case 23: return '4';
                case 24: return 'p';
                case 25: return 'D';
                case 26: return 'O';
                case 27: return '6';
                case 28: return 'H';
                case 29: return 'Q';
                case 30: return 'x';
                case 31: return 'd';
                case 32: return 'h';
                case 33: return 'K';
                case 34: return 'L';
                case 35: return 'C';
                case 36: return 'I';
                case 37: return 'b';
                case 38: return 'J';
                case 39: return 'N';
                case 40: return 'i';
                case 41: return 'F';
                case 42: return 'T';
                case 43: return 'E';
                case 44: return 's';
                case 45: return 'U';
                case 46: return 'A';
                case 47: return 'k';
            }
        }
        if (second == 33)
        {
            switch (casePar)
            {
                case 0: return '8';
                case 1: return 'r';
                case 2: return 'p';
                case 3: return 'c';
                case 4: return 'B';
                case 5: return 'U';
                case 6: return 'x';
                case 7: return 'm';
                case 8: return 'k';
                case 9: return '6';
                case 10: return 'j';
                case 11: return 'A';
                case 12: return 'K';
                case 13: return '0';
                case 14: return '2';
                case 15: return 'J';
                case 16: return 's';
                case 17: return 'F';
                case 18: return 'h';
                case 19: return 'w';
                case 20: return 'l';
                case 21: return 'H';
                case 22: return 'N';
                case 23: return 'G';
                case 24: return 'I';
                case 25: return 'L';
                case 26: return '7';
                case 27: return 'f';
                case 28: return 'a';
                case 29: return 'R';
                case 30: return '5';
                case 31: return 'V';
                case 32: return 'b';
                case 33: return 'S';
                case 34: return 'Y';
                case 35: return 'e';
                case 36: return '1';
                case 37: return 'u';
                case 38: return 'n';
                case 39: return 'P';
                case 40: return 'g';
                case 41: return '4';
                case 42: return '3';
                case 43: return 'O';
                case 44: return 'q';
                case 45: return 'o';
                case 46: return '9';
                case 47: return 'W';
            }
        }
        if (second == 34)
        {
            switch (casePar)
            {
                case 0: return '4';
                case 1: return '8';
                case 2: return 'k';
                case 3: return 'N';
                case 4: return '1';
                case 5: return 'i';
                case 6: return '5';
                case 7: return 'u';
                case 8: return 's';
                case 9: return '7';
                case 10: return '6';
                case 11: return 'R';
                case 12: return 'h';
                case 13: return 'W';
                case 14: return 'G';
                case 15: return 't';
                case 16: return 'r';
                case 17: return 'j';
                case 18: return 'n';
                case 19: return 'v';
                case 20: return 'X';
                case 21: return 'O';
                case 22: return 'x';
                case 23: return '0';
                case 24: return 'H';
                case 25: return 'S';
                case 26: return 'p';
                case 27: return '3';
                case 28: return 'w';
                case 29: return '9';
                case 30: return 'B';
                case 31: return 'V';
                case 32: return 'L';
                case 33: return 'M';
                case 34: return 'd';
                case 35: return 'P';
                case 36: return 'Q';
                case 37: return 'f';
                case 38: return 'Y';
                case 39: return 'y';
                case 40: return 'I';
                case 41: return 'J';
                case 42: return 'A';
                case 43: return 'T';
                case 44: return 'l';
                case 45: return 'C';
                case 46: return 'o';
                case 47: return 'E';
            }
        }
        if (second == 35)
        {
            switch (casePar)
            {
                case 0: return 'P';
                case 1: return 's';
                case 2: return 'l';
                case 3: return '7';
                case 4: return 'u';
                case 5: return 'K';
                case 6: return '0';
                case 7: return '4';
                case 8: return '3';
                case 9: return 'E';
                case 10: return 'a';
                case 11: return 'S';
                case 12: return '2';
                case 13: return 'q';
                case 14: return 'v';
                case 15: return 'w';
                case 16: return 'X';
                case 17: return '6';
                case 18: return 'h';
                case 19: return 'y';
                case 20: return 'g';
                case 21: return 'n';
                case 22: return 'A';
                case 23: return '8';
                case 24: return 'p';
                case 25: return 'C';
                case 26: return 'N';
                case 27: return 'b';
                case 28: return 'H';
                case 29: return 'o';
                case 30: return '9';
                case 31: return 'I';
                case 32: return '1';
                case 33: return 'W';
                case 34: return 'D';
                case 35: return 'T';
                case 36: return 'B';
                case 37: return '5';
                case 38: return 'M';
                case 39: return 'd';
                case 40: return 'L';
                case 41: return 'c';
                case 42: return 'F';
                case 43: return 'i';
                case 44: return 'Q';
                case 45: return 'J';
                case 46: return 'k';
                case 47: return 'G';
            }
        }
        if (second == 36)
        {
            switch (casePar)
            {
                case 0: return '5';
                case 1: return 'Q';
                case 2: return '2';
                case 3: return 'J';
                case 4: return 'm';
                case 5: return '7';
                case 6: return 'G';
                case 7: return '6';
                case 8: return '9';
                case 9: return 'I';
                case 10: return 'w';
                case 11: return 'o';
                case 12: return 'n';
                case 13: return 'F';
                case 14: return 'd';
                case 15: return '4';
                case 16: return 'b';
                case 17: return '8';
                case 18: return '1';
                case 19: return 'P';
                case 20: return 'u';
                case 21: return 'l';
                case 22: return 'R';
                case 23: return 'D';
                case 24: return '3';
                case 25: return 'L';
                case 26: return 'N';
                case 27: return '0';
                case 28: return 'T';
                case 29: return 'f';
                case 30: return 'Y';
                case 31: return 'K';
                case 32: return 'v';
                case 33: return 'V';
                case 34: return 'y';
                case 35: return 'c';
                case 36: return 'O';
                case 37: return 'q';
                case 38: return 'H';
                case 39: return 'S';
                case 40: return 'B';
                case 41: return 'j';
                case 42: return 'i';
                case 43: return 'e';
                case 44: return 'x';
                case 45: return 'M';
                case 46: return 'r';
                case 47: return 'h';
            }
        }
        if (second == 37)
        {
            switch (casePar)
            {
                case 0: return 'a';
                case 1: return '8';
                case 2: return '9';
                case 3: return 'C';
                case 4: return 'h';
                case 5: return '7';
                case 6: return 'q';
                case 7: return 'B';
                case 8: return 'e';
                case 9: return 't';
                case 10: return 'N';
                case 11: return 's';
                case 12: return 'n';
                case 13: return 'r';
                case 14: return 'k';
                case 15: return 'i';
                case 16: return 'O';
                case 17: return '1';
                case 18: return 'x';
                case 19: return 'Y';
                case 20: return 'I';
                case 21: return 'v';
                case 22: return 'E';
                case 23: return 'y';
                case 24: return '4';
                case 25: return 'X';
                case 26: return 'V';
                case 27: return 'L';
                case 28: return '5';
                case 29: return 'p';
                case 30: return 'g';
                case 31: return 'l';
                case 32: return 'P';
                case 33: return 'G';
                case 34: return 'W';
                case 35: return '0';
                case 36: return 'o';
                case 37: return 'T';
                case 38: return 'J';
                case 39: return 'M';
                case 40: return '6';
                case 41: return 'S';
                case 42: return 'K';
                case 43: return 'm';
                case 44: return 'u';
                case 45: return '2';
                case 46: return 'w';
                case 47: return 'c';
            }
        }
        if (second == 38)
        {
            switch (casePar)
            {
                case 0: return 'W';
                case 1: return 'y';
                case 2: return 'L';
                case 3: return 'M';
                case 4: return 'x';
                case 5: return 'f';
                case 6: return 'k';
                case 7: return '6';
                case 8: return 'p';
                case 9: return 'v';
                case 10: return 's';
                case 11: return 'F';
                case 12: return 'S';
                case 13: return 'P';
                case 14: return 'm';
                case 15: return 'K';
                case 16: return 'Y';
                case 17: return '5';
                case 18: return 'J';
                case 19: return 'E';
                case 20: return 'I';
                case 21: return '2';
                case 22: return 'D';
                case 23: return '3';
                case 24: return '7';
                case 25: return 'O';
                case 26: return 'A';
                case 27: return '4';
                case 28: return 'G';
                case 29: return '9';
                case 30: return 'w';
                case 31: return 'R';
                case 32: return '0';
                case 33: return 'B';
                case 34: return 'r';
                case 35: return 'g';
                case 36: return '8';
                case 37: return 'U';
                case 38: return 'c';
                case 39: return 'o';
                case 40: return 'e';
                case 41: return 'C';
                case 42: return 'd';
                case 43: return 'N';
                case 44: return '1';
                case 45: return 'j';
                case 46: return 'u';
                case 47: return 'q';
            }
        }
        if (second == 39)
        {
            switch (casePar)
            {
                case 0: return 'R';
                case 1: return 'P';
                case 2: return 'O';
                case 3: return '2';
                case 4: return '0';
                case 5: return 'L';
                case 6: return 'S';
                case 7: return 'F';
                case 8: return 'G';
                case 9: return '8';
                case 10: return 'E';
                case 11: return 'o';
                case 12: return '1';
                case 13: return '7';
                case 14: return 'l';
                case 15: return '3';
                case 16: return 'c';
                case 17: return '9';
                case 18: return 'b';
                case 19: return 'f';
                case 20: return 'i';
                case 21: return '4';
                case 22: return '6';
                case 23: return 'u';
                case 24: return '5';
                case 25: return 'w';
                case 26: return 'y';
                case 27: return 's';
                case 28: return 'd';
                case 29: return 'W';
                case 30: return 'n';
                case 31: return 'h';
                case 32: return 'a';
                case 33: return 'C';
                case 34: return 't';
                case 35: return 'M';
                case 36: return 'N';
                case 37: return 'D';
                case 38: return 'I';
                case 39: return 'x';
                case 40: return 'A';
                case 41: return 'Y';
                case 42: return 'X';
                case 43: return 'k';
                case 44: return 'm';
                case 45: return 'q';
                case 46: return 'T';
                case 47: return 'e';
            }
        }
        if (second == 40)
        {
            switch (casePar)
            {
                case 0: return 'k';
                case 1: return '7';
                case 2: return '1';
                case 3: return 'I';
                case 4: return 'E';
                case 5: return '0';
                case 6: return 'Q';
                case 7: return 'j';
                case 8: return '9';
                case 9: return '5';
                case 10: return '4';
                case 11: return 'q';
                case 12: return 'r';
                case 13: return 'c';
                case 14: return 'C';
                case 15: return '6';
                case 16: return 'u';
                case 17: return 'O';
                case 18: return '3';
                case 19: return 'l';
                case 20: return 'T';
                case 21: return 'L';
                case 22: return '8';
                case 23: return 'p';
                case 24: return 'M';
                case 25: return 'h';
                case 26: return 'P';
                case 27: return 'A';
                case 28: return 'm';
                case 29: return 'd';
                case 30: return 'v';
                case 31: return 'H';
                case 32: return 's';
                case 33: return 'f';
                case 34: return 't';
                case 35: return 'K';
                case 36: return 'o';
                case 37: return 'X';
                case 38: return 'V';
                case 39: return 'x';
                case 40: return 'a';
                case 41: return 'B';
                case 42: return 'G';
                case 43: return 'J';
                case 44: return '2';
                case 45: return 'w';
                case 46: return 'U';
                case 47: return 'y';
            }
        }
        if (second == 41)
        {
            switch (casePar)
            {
                case 0: return '8';
                case 1: return 'T';
                case 2: return '9';
                case 3: return '7';
                case 4: return '1';
                case 5: return 'f';
                case 6: return 'O';
                case 7: return 'K';
                case 8: return 'U';
                case 9: return '2';
                case 10: return '6';
                case 11: return 'N';
                case 12: return '4';
                case 13: return 'w';
                case 14: return 'q';
                case 15: return '5';
                case 16: return 'u';
                case 17: return 'I';
                case 18: return 'd';
                case 19: return 'M';
                case 20: return 'Y';
                case 21: return 'D';
                case 22: return 'b';
                case 23: return '3';
                case 24: return 'i';
                case 25: return 'r';
                case 26: return 'v';
                case 27: return 'F';
                case 28: return 'E';
                case 29: return 'R';
                case 30: return 'P';
                case 31: return 'V';
                case 32: return 'e';
                case 33: return 'c';
                case 34: return 'X';
                case 35: return 'h';
                case 36: return 'S';
                case 37: return 'm';
                case 38: return 'L';
                case 39: return 'J';
                case 40: return 'G';
                case 41: return 'k';
                case 42: return 'x';
                case 43: return 'C';
                case 44: return 'p';
                case 45: return 't';
                case 46: return 'B';
                case 47: return 'W';
            }
        }
        if (second == 42)
        {
            switch (casePar)
            {
                case 0: return 'P';
                case 1: return '6';
                case 2: return '5';
                case 3: return 'i';
                case 4: return 'C';
                case 5: return 'e';
                case 6: return 'g';
                case 7: return 'y';
                case 8: return 'S';
                case 9: return '1';
                case 10: return 'w';
                case 11: return '9';
                case 12: return '3';
                case 13: return 'l';
                case 14: return '8';
                case 15: return 'f';
                case 16: return 'Y';
                case 17: return 'H';
                case 18: return '2';
                case 19: return 'M';
                case 20: return 'o';
                case 21: return 's';
                case 22: return 'n';
                case 23: return 'T';
                case 24: return 'b';
                case 25: return 'U';
                case 26: return '0';
                case 27: return 'c';
                case 28: return '7';
                case 29: return 'J';
                case 30: return 'R';
                case 31: return 'G';
                case 32: return 'W';
                case 33: return 'r';
                case 34: return 'E';
                case 35: return '4';
                case 36: return 'L';
                case 37: return 'D';
                case 38: return 'X';
                case 39: return 'N';
                case 40: return 'a';
                case 41: return 'h';
                case 42: return 'v';
                case 43: return 'A';
                case 44: return 'q';
                case 45: return 'O';
                case 46: return 'V';
                case 47: return 'm';
            }
        }
        if (second == 43)
        {
            switch (casePar)
            {
                case 0: return 'y';
                case 1: return 'r';
                case 2: return '8';
                case 3: return '4';
                case 4: return 'o';
                case 5: return 'x';
                case 6: return 'a';
                case 7: return '7';
                case 8: return 'J';
                case 9: return 'h';
                case 10: return 'L';
                case 11: return 'O';
                case 12: return '2';
                case 13: return 'X';
                case 14: return 'w';
                case 15: return 'q';
                case 16: return 'p';
                case 17: return '1';
                case 18: return '5';
                case 19: return '9';
                case 20: return 'Y';
                case 21: return 'c';
                case 22: return 's';
                case 23: return '6';
                case 24: return 'f';
                case 25: return 'j';
                case 26: return 'R';
                case 27: return 'i';
                case 28: return 'U';
                case 29: return '3';
                case 30: return 'P';
                case 31: return 'm';
                case 32: return 'D';
                case 33: return '0';
                case 34: return 'E';
                case 35: return 'K';
                case 36: return 'C';
                case 37: return 'e';
                case 38: return 'M';
                case 39: return 'v';
                case 40: return 'k';
                case 41: return 'l';
                case 42: return 'I';
                case 43: return 'u';
                case 44: return 'V';
                case 45: return 'S';
                case 46: return 'F';
                case 47: return 'W';
            }
        }
        if (second == 44)
        {
            switch (casePar)
            {
                case 0: return 'y';
                case 1: return 'J';
                case 2: return '3';
                case 3: return '9';
                case 4: return 'a';
                case 5: return 'I';
                case 6: return '8';
                case 7: return 'W';
                case 8: return 'p';
                case 9: return 'g';
                case 10: return 'q';
                case 11: return 'L';
                case 12: return 's';
                case 13: return 'w';
                case 14: return 'E';
                case 15: return 'o';
                case 16: return '1';
                case 17: return 'N';
                case 18: return 'B';
                case 19: return 'V';
                case 20: return 'A';
                case 21: return 'l';
                case 22: return 'D';
                case 23: return 'f';
                case 24: return 'X';
                case 25: return '0';
                case 26: return '6';
                case 27: return 'd';
                case 28: return 'k';
                case 29: return 'i';
                case 30: return 'G';
                case 31: return 'x';
                case 32: return 'Y';
                case 33: return '4';
                case 34: return 'H';
                case 35: return 'O';
                case 36: return 'e';
                case 37: return 't';
                case 38: return '5';
                case 39: return '7';
                case 40: return 'T';
                case 41: return 'C';
                case 42: return 'K';
                case 43: return 'Q';
                case 44: return 'n';
                case 45: return 'R';
                case 46: return 'j';
                case 47: return 'F';
            }
        }
        if (second == 45)
        {
            switch (casePar)
            {
                case 0: return '1';
                case 1: return 'I';
                case 2: return 'W';
                case 3: return 'm';
                case 4: return 'g';
                case 5: return '9';
                case 6: return 'u';
                case 7: return 'H';
                case 8: return 'U';
                case 9: return 'k';
                case 10: return 'R';
                case 11: return '3';
                case 12: return 'w';
                case 13: return 'N';
                case 14: return '8';
                case 15: return 'i';
                case 16: return 'O';
                case 17: return 'v';
                case 18: return '6';
                case 19: return 'P';
                case 20: return '4';
                case 21: return '5';
                case 22: return 'y';
                case 23: return 'o';
                case 24: return 'f';
                case 25: return 'S';
                case 26: return 'K';
                case 27: return 'Y';
                case 28: return 'A';
                case 29: return 't';
                case 30: return 'j';
                case 31: return 's';
                case 32: return 'C';
                case 33: return 'q';
                case 34: return 'J';
                case 35: return 'r';
                case 36: return '0';
                case 37: return 'E';
                case 38: return 'l';
                case 39: return 'p';
                case 40: return 'X';
                case 41: return 'V';
                case 42: return 'd';
                case 43: return 'L';
                case 44: return 'G';
                case 45: return '2';
                case 46: return 'D';
                case 47: return 'Q';
            }
        }
        if (second == 46)
        {
            switch (casePar)
            {
                case 0: return 'F';
                case 1: return 'N';
                case 2: return 's';
                case 3: return 'J';
                case 4: return 'a';
                case 5: return 'D';
                case 6: return 'u';
                case 7: return 'b';
                case 8: return '0';
                case 9: return '4';
                case 10: return 'U';
                case 11: return '5';
                case 12: return '7';
                case 13: return 'Q';
                case 14: return 'L';
                case 15: return 'G';
                case 16: return 'K';
                case 17: return 'h';
                case 18: return 'v';
                case 19: return '8';
                case 20: return 'n';
                case 21: return 't';
                case 22: return '1';
                case 23: return 'x';
                case 24: return 'C';
                case 25: return 'M';
                case 26: return 'w';
                case 27: return 'S';
                case 28: return 'i';
                case 29: return 'r';
                case 30: return 'E';
                case 31: return 'f';
                case 32: return '6';
                case 33: return 'I';
                case 34: return '3';
                case 35: return 'O';
                case 36: return 'A';
                case 37: return 'V';
                case 38: return 'B';
                case 39: return 'm';
                case 40: return 'j';
                case 41: return 'l';
                case 42: return '2';
                case 43: return 'y';
                case 44: return 'p';
                case 45: return '9';
                case 46: return 'P';
                case 47: return 'Y';
            }
        }
        if (second == 47)
        {
            switch (casePar)
            {
                case 0: return '3';
                case 1: return 'J';
                case 2: return 'y';
                case 3: return 'N';
                case 4: return 'L';
                case 5: return 'U';
                case 6: return '0';
                case 7: return 't';
                case 8: return '1';
                case 9: return 'F';
                case 10: return 'P';
                case 11: return '6';
                case 12: return 'h';
                case 13: return 'k';
                case 14: return '5';
                case 15: return 'l';
                case 16: return '7';
                case 17: return 'G';
                case 18: return 'd';
                case 19: return 'j';
                case 20: return 'o';
                case 21: return 'V';
                case 22: return 'f';
                case 23: return 'R';
                case 24: return 'T';
                case 25: return 'v';
                case 26: return 'A';
                case 27: return 'r';
                case 28: return '4';
                case 29: return 'S';
                case 30: return 'O';
                case 31: return 'w';
                case 32: return 'K';
                case 33: return 'x';
                case 34: return 'D';
                case 35: return 'm';
                case 36: return 'p';
                case 37: return 'W';
                case 38: return 'I';
                case 39: return 'E';
                case 40: return 'q';
                case 41: return 'C';
                case 42: return '2';
                case 43: return '8';
                case 44: return 'a';
                case 45: return '9';
                case 46: return 'i';
                case 47: return 'n';
            }
        }
        if (second == 48)
        {
            switch (casePar)
            {
                case 0: return 'T';
                case 1: return '2';
                case 2: return 'W';
                case 3: return '8';
                case 4: return 'F';
                case 5: return 'x';
                case 6: return '5';
                case 7: return '7';
                case 8: return '9';
                case 9: return 'f';
                case 10: return '4';
                case 11: return 'B';
                case 12: return 'V';
                case 13: return 'X';
                case 14: return 'E';
                case 15: return 'Q';
                case 16: return 'L';
                case 17: return 'C';
                case 18: return 'y';
                case 19: return 'o';
                case 20: return '0';
                case 21: return '3';
                case 22: return 'p';
                case 23: return 'u';
                case 24: return '6';
                case 25: return 'g';
                case 26: return 'k';
                case 27: return 'r';
                case 28: return 'A';
                case 29: return 'H';
                case 30: return 'P';
                case 31: return 'j';
                case 32: return 'O';
                case 33: return 'e';
                case 34: return 'S';
                case 35: return 'v';
                case 36: return 'J';
                case 37: return 'G';
                case 38: return 's';
                case 39: return 'N';
                case 40: return 'Y';
                case 41: return 'q';
                case 42: return 'M';
                case 43: return 'w';
                case 44: return 'b';
                case 45: return 'K';
                case 46: return 'i';
                case 47: return 'n';
            }
        }
        if (second == 49)
        {
            switch (casePar)
            {
                case 0: return 'j';
                case 1: return 'S';
                case 2: return 'l';
                case 3: return 'u';
                case 4: return '4';
                case 5: return 's';
                case 6: return 'o';
                case 7: return 'G';
                case 8: return 'C';
                case 9: return 'L';
                case 10: return 'h';
                case 11: return '0';
                case 12: return 'e';
                case 13: return 'M';
                case 14: return 'y';
                case 15: return 'P';
                case 16: return 'Y';
                case 17: return '1';
                case 18: return 'E';
                case 19: return '9';
                case 20: return 'f';
                case 21: return 'U';
                case 22: return 'W';
                case 23: return 'v';
                case 24: return 'N';
                case 25: return 'b';
                case 26: return '5';
                case 27: return '6';
                case 28: return '2';
                case 29: return '3';
                case 30: return '8';
                case 31: return 'F';
                case 32: return 'i';
                case 33: return 'g';
                case 34: return 'Q';
                case 35: return 't';
                case 36: return 'q';
                case 37: return 'c';
                case 38: return '7';
                case 39: return 'a';
                case 40: return 'w';
                case 41: return 'p';
                case 42: return 'R';
                case 43: return 'T';
                case 44: return 'n';
                case 45: return 'A';
                case 46: return 'x';
                case 47: return 'V';
            }
        }
        if (second == 50)
        {
            switch (casePar)
            {
                case 0: return '2';
                case 1: return '1';
                case 2: return 'l';
                case 3: return 'K';
                case 4: return 'y';
                case 5: return 'T';
                case 6: return '7';
                case 7: return 'H';
                case 8: return 'R';
                case 9: return '6';
                case 10: return 'w';
                case 11: return 't';
                case 12: return 'd';
                case 13: return '8';
                case 14: return 'h';
                case 15: return 'o';
                case 16: return 'p';
                case 17: return 'U';
                case 18: return 'J';
                case 19: return 'r';
                case 20: return 'S';
                case 21: return 'Y';
                case 22: return '3';
                case 23: return 'm';
                case 24: return '0';
                case 25: return 'b';
                case 26: return 'u';
                case 27: return 'v';
                case 28: return '5';
                case 29: return '9';
                case 30: return 'D';
                case 31: return 'A';
                case 32: return 'k';
                case 33: return 'P';
                case 34: return 'O';
                case 35: return 'n';
                case 36: return 'X';
                case 37: return 'e';
                case 38: return '4';
                case 39: return 'F';
                case 40: return 'W';
                case 41: return 'M';
                case 42: return 'q';
                case 43: return 'N';
                case 44: return 'f';
                case 45: return 'i';
                case 46: return 'E';
                case 47: return 'g';
            }
        }
        if (second == 51)
        {
            switch (casePar)
            {
                case 0: return 'y';
                case 1: return 'M';
                case 2: return '0';
                case 3: return '8';
                case 4: return 'w';
                case 5: return 'O';
                case 6: return '3';
                case 7: return 'e';
                case 8: return '9';
                case 9: return '5';
                case 10: return 'L';
                case 11: return 'K';
                case 12: return 'F';
                case 13: return 'a';
                case 14: return 'V';
                case 15: return 'C';
                case 16: return '4';
                case 17: return 'R';
                case 18: return 'q';
                case 19: return '2';
                case 20: return 'I';
                case 21: return 'G';
                case 22: return 'v';
                case 23: return '1';
                case 24: return 'D';
                case 25: return 'j';
                case 26: return 'o';
                case 27: return 'g';
                case 28: return 's';
                case 29: return 'h';
                case 30: return 'k';
                case 31: return '7';
                case 32: return 'E';
                case 33: return 'u';
                case 34: return 'b';
                case 35: return 'W';
                case 36: return 'x';
                case 37: return 'S';
                case 38: return 'T';
                case 39: return 'Y';
                case 40: return 'l';
                case 41: return 'i';
                case 42: return 'f';
                case 43: return 'J';
                case 44: return 'r';
                case 45: return 'U';
                case 46: return 'B';
                case 47: return 'Q';
            }
        }
        if (second == 52)
        {
            switch (casePar)
            {
                case 0: return '3';
                case 1: return 'B';
                case 2: return 'A';
                case 3: return 'K';
                case 4: return 'T';
                case 5: return 'o';
                case 6: return 'w';
                case 7: return 'Y';
                case 8: return 'd';
                case 9: return 'p';
                case 10: return '9';
                case 11: return 'h';
                case 12: return '6';
                case 13: return 'X';
                case 14: return 'j';
                case 15: return 'Q';
                case 16: return 'u';
                case 17: return 'r';
                case 18: return 'U';
                case 19: return 'L';
                case 20: return 'J';
                case 21: return '7';
                case 22: return 'k';
                case 23: return 'b';
                case 24: return '1';
                case 25: return 'F';
                case 26: return 'g';
                case 27: return 'E';
                case 28: return 'q';
                case 29: return 'e';
                case 30: return 'y';
                case 31: return '8';
                case 32: return 'C';
                case 33: return 'M';
                case 34: return '2';
                case 35: return '0';
                case 36: return 't';
                case 37: return 'f';
                case 38: return 'N';
                case 39: return 'O';
                case 40: return '5';
                case 41: return '4';
                case 42: return 'l';
                case 43: return 'G';
                case 44: return 'H';
                case 45: return 'n';
                case 46: return 'D';
                case 47: return 's';
            }
        }
        if (second == 53)
        {
            switch (casePar)
            {
                case 0: return 'N';
                case 1: return 'A';
                case 2: return '0';
                case 3: return 'i';
                case 4: return '4';
                case 5: return 't';
                case 6: return 'e';
                case 7: return 'T';
                case 8: return 'y';
                case 9: return 's';
                case 10: return '6';
                case 11: return 'B';
                case 12: return 'P';
                case 13: return 'V';
                case 14: return 'b';
                case 15: return 'a';
                case 16: return 'r';
                case 17: return 'W';
                case 18: return 'F';
                case 19: return 'j';
                case 20: return '2';
                case 21: return 'M';
                case 22: return '9';
                case 23: return 'q';
                case 24: return 'f';
                case 25: return 'O';
                case 26: return 'U';
                case 27: return 'm';
                case 28: return '8';
                case 29: return 'v';
                case 30: return 'D';
                case 31: return 'E';
                case 32: return '3';
                case 33: return 'R';
                case 34: return 'd';
                case 35: return 'g';
                case 36: return 'I';
                case 37: return 'c';
                case 38: return 'J';
                case 39: return 'u';
                case 40: return 'G';
                case 41: return 'w';
                case 42: return 'k';
                case 43: return 'S';
                case 44: return 'C';
                case 45: return '7';
                case 46: return 'p';
                case 47: return 'L';
            }
        }
        if (second == 54)
        {
            switch (casePar)
            {
                case 0: return 'u';
                case 1: return '3';
                case 2: return 'k';
                case 3: return 'V';
                case 4: return '0';
                case 5: return 'W';
                case 6: return 'l';
                case 7: return '8';
                case 8: return 'L';
                case 9: return '1';
                case 10: return 'R';
                case 11: return '2';
                case 12: return 'h';
                case 13: return 'Y';
                case 14: return 'D';
                case 15: return '6';
                case 16: return 'N';
                case 17: return 't';
                case 18: return 'A';
                case 19: return 'K';
                case 20: return 'p';
                case 21: return '7';
                case 22: return 'd';
                case 23: return 'M';
                case 24: return '5';
                case 25: return 'E';
                case 26: return 'J';
                case 27: return '9';
                case 28: return '4';
                case 29: return 'm';
                case 30: return 'n';
                case 31: return 'Q';
                case 32: return 'X';
                case 33: return 'i';
                case 34: return 'e';
                case 35: return 'S';
                case 36: return 'B';
                case 37: return 'H';
                case 38: return 'C';
                case 39: return 'a';
                case 40: return 'P';
                case 41: return 'j';
                case 42: return 'v';
                case 43: return 'c';
                case 44: return 'q';
                case 45: return 'G';
                case 46: return 's';
                case 47: return 'I';
            }
        }
        if (second == 55)
        {
            switch (casePar)
            {
                case 0: return 'A';
                case 1: return 'S';
                case 2: return '5';
                case 3: return 'i';
                case 4: return 'b';
                case 5: return 'f';
                case 6: return 'r';
                case 7: return 'o';
                case 8: return 'y';
                case 9: return 'C';
                case 10: return 'x';
                case 11: return '9';
                case 12: return '6';
                case 13: return 'l';
                case 14: return 'D';
                case 15: return '0';
                case 16: return '7';
                case 17: return 'v';
                case 18: return 'p';
                case 19: return 'O';
                case 20: return 'Y';
                case 21: return '1';
                case 22: return '4';
                case 23: return 'V';
                case 24: return 'q';
                case 25: return '8';
                case 26: return 'u';
                case 27: return '2';
                case 28: return 'j';
                case 29: return 'n';
                case 30: return 'm';
                case 31: return 's';
                case 32: return 'a';
                case 33: return 'T';
                case 34: return '3';
                case 35: return 'E';
                case 36: return 'U';
                case 37: return 'w';
                case 38: return 'F';
                case 39: return 'W';
                case 40: return 'H';
                case 41: return 'X';
                case 42: return 'B';
                case 43: return 'k';
                case 44: return 'c';
                case 45: return 'L';
                case 46: return 'M';
                case 47: return 'h';
            }
        }
        if (second == 56)
        {
            switch (casePar)
            {
                case 0: return '5';
                case 1: return '8';
                case 2: return 'S';
                case 3: return 'N';
                case 4: return '2';
                case 5: return '1';
                case 6: return 'q';
                case 7: return 'r';
                case 8: return 'B';
                case 9: return 'd';
                case 10: return 'X';
                case 11: return 'U';
                case 12: return '4';
                case 13: return 'g';
                case 14: return 'O';
                case 15: return 'Y';
                case 16: return '9';
                case 17: return '7';
                case 18: return 'h';
                case 19: return 'c';
                case 20: return 'T';
                case 21: return '6';
                case 22: return 'R';
                case 23: return 'a';
                case 24: return 'I';
                case 25: return 'n';
                case 26: return '3';
                case 27: return '0';
                case 28: return 'k';
                case 29: return 'o';
                case 30: return 'E';
                case 31: return 'F';
                case 32: return 't';
                case 33: return 'H';
                case 34: return 'l';
                case 35: return 'J';
                case 36: return 'u';
                case 37: return 'L';
                case 38: return 'M';
                case 39: return 'Q';
                case 40: return 'A';
                case 41: return 'K';
                case 42: return 'm';
                case 43: return 'p';
                case 44: return 'D';
                case 45: return 'f';
                case 46: return 'G';
                case 47: return 'P';
            }
        }
        if (second == 57)
        {
            switch (casePar)
            {
                case 0: return 'X';
                case 1: return 'T';
                case 2: return 'd';
                case 3: return 'm';
                case 4: return '6';
                case 5: return '7';
                case 6: return '3';
                case 7: return 'n';
                case 8: return 'O';
                case 9: return 'V';
                case 10: return 's';
                case 11: return 'e';
                case 12: return 'E';
                case 13: return 'H';
                case 14: return 'Y';
                case 15: return '8';
                case 16: return 'F';
                case 17: return 'l';
                case 18: return '9';
                case 19: return 'J';
                case 20: return 'G';
                case 21: return 'f';
                case 22: return '2';
                case 23: return 'K';
                case 24: return 'C';
                case 25: return '1';
                case 26: return 'g';
                case 27: return '0';
                case 28: return 'A';
                case 29: return 'M';
                case 30: return 't';
                case 31: return '5';
                case 32: return 'c';
                case 33: return 'Q';
                case 34: return 'h';
                case 35: return 'R';
                case 36: return 'r';
                case 37: return '4';
                case 38: return 'N';
                case 39: return 'W';
                case 40: return 'b';
                case 41: return 'o';
                case 42: return 'v';
                case 43: return 'j';
                case 44: return 'L';
                case 45: return 'u';
                case 46: return 'B';
                case 47: return 'S';
            }
        }
        if (second == 58)
        {
            switch (casePar)
            {
                case 0: return 'U';
                case 1: return 'f';
                case 2: return '5';
                case 3: return 'T';
                case 4: return 'c';
                case 5: return '7';
                case 6: return '0';
                case 7: return 'i';
                case 8: return 'I';
                case 9: return 'F';
                case 10: return 'j';
                case 11: return 'V';
                case 12: return 'w';
                case 13: return '3';
                case 14: return 'D';
                case 15: return 'u';
                case 16: return 'Q';
                case 17: return '6';
                case 18: return 'N';
                case 19: return '2';
                case 20: return '9';
                case 21: return 'P';
                case 22: return 'G';
                case 23: return 'b';
                case 24: return 'C';
                case 25: return '8';
                case 26: return '4';
                case 27: return '1';
                case 28: return 'o';
                case 29: return 'K';
                case 30: return 'A';
                case 31: return 'y';
                case 32: return 'R';
                case 33: return 'q';
                case 34: return 'X';
                case 35: return 'p';
                case 36: return 'h';
                case 37: return 'H';
                case 38: return 'M';
                case 39: return 'l';
                case 40: return 'm';
                case 41: return 'L';
                case 42: return 'x';
                case 43: return 'e';
                case 44: return 'k';
                case 45: return 'n';
                case 46: return 'Y';
                case 47: return 'E';
            }
        }
        if (second == 59)
        {
            switch (casePar)
            {
                case 0: return 'j';
                case 1: return 'W';
                case 2: return '9';
                case 3: return '0';
                case 4: return 'a';
                case 5: return '5';
                case 6: return 'w';
                case 7: return '7';
                case 8: return '1';
                case 9: return 'R';
                case 10: return '4';
                case 11: return 'B';
                case 12: return 'K';
                case 13: return '6';
                case 14: return '2';
                case 15: return 'I';
                case 16: return '3';
                case 17: return 'f';
                case 18: return 'l';
                case 19: return 'b';
                case 20: return 'F';
                case 21: return 'n';
                case 22: return 'J';
                case 23: return 'X';
                case 24: return 'D';
                case 25: return 'P';
                case 26: return 'G';
                case 27: return '8';
                case 28: return 'e';
                case 29: return 'x';
                case 30: return 'H';
                case 31: return 'Q';
                case 32: return 'N';
                case 33: return 'V';
                case 34: return 'h';
                case 35: return 'm';
                case 36: return 'O';
                case 37: return 'y';
                case 38: return 'L';
                case 39: return 'T';
                case 40: return 'Y';
                case 41: return 'S';
                case 42: return 'g';
                case 43: return 'i';
                case 44: return 'c';
                case 45: return 'p';
                case 46: return 'o';
                case 47: return 'q';
            }
        }

        return ' ';
    }
    private int decipherNumber(int second, char casePar)
    {
        if (second == 0)
        {
            switch (casePar)
            {
                case 'v': return 0;
                case 'K': return 1;
                case '0': return 2;
                case 'r': return 3;
                case 'P': return 4;
                case 'g': return 5;
                case 'o': return 6;
                case 'a': return 7;
                case '8': return 8;
                case '7': return 9;
                case '5': return 10;
                case 'p': return 11;
                case 'I': return 12;
                case '2': return 13;
                case '9': return 14;
                case 'T': return 15;
                case '6': return 16;
                case 'D': return 17;
                case 'L': return 18;
                case 'J': return 19;
                case '1': return 20;
                case 'F': return 21;
                case 'h': return 22;
                case 'b': return 23;
                case '3': return 24;
                case '4': return 25;
                case 'n': return 26;
                case 'X': return 27;
                case 'O': return 28;
                case 'V': return 29;
                case 't': return 30;
                case 's': return 31;
                case 'W': return 32;
                case 'Q': return 33;
                case 'i': return 34;
                case 'N': return 35;
                case 'w': return 36;
                case 'U': return 37;
                case 'm': return 38;
                case 'B': return 39;
                case 'x': return 40;
                case 'l': return 41;
                case 'c': return 42;
                case 'k': return 43;
                case 'q': return 44;
                case 'S': return 45;
                case 'M': return 46;
                case 'C': return 47;
            }
        }
        if (second == 1)
        {
            switch (casePar)
            {
                case '4': return 0;
                case 'G': return 1;
                case 'y': return 2;
                case 'q': return 3;
                case 'C': return 4;
                case '5': return 5;
                case '8': return 6;
                case 'F': return 7;
                case 'A': return 8;
                case 'I': return 9;
                case '6': return 10;
                case 'X': return 11;
                case 'R': return 12;
                case 'h': return 13;
                case 'w': return 14;
                case 'S': return 15;
                case 'V': return 16;
                case 'M': return 17;
                case '1': return 18;
                case 'm': return 19;
                case 'Q': return 20;
                case 'Y': return 21;
                case 'r': return 22;
                case '9': return 23;
                case '0': return 24;
                case 'O': return 25;
                case 'T': return 26;
                case '2': return 27;
                case 'p': return 28;
                case '7': return 29;
                case 'j': return 30;
                case 'b': return 31;
                case 'i': return 32;
                case '3': return 33;
                case 'l': return 34;
                case 'B': return 35;
                case 'n': return 36;
                case 'f': return 37;
                case 'c': return 38;
                case 'a': return 39;
                case 'L': return 40;
                case 'P': return 41;
                case 'e': return 42;
                case 'g': return 43;
                case 'U': return 44;
                case 'H': return 45;
                case 'J': return 46;
                case 'K': return 47;
            }
        }
        if (second == 2)
        {
            switch (casePar)
            {
                case 'T': return 0;
                case 'B': return 1;
                case 'A': return 2;
                case '7': return 3;
                case 't': return 4;
                case 'u': return 5;
                case 'C': return 6;
                case 'm': return 7;
                case '9': return 8;
                case '5': return 9;
                case 'G': return 10;
                case '1': return 11;
                case 'W': return 12;
                case 'c': return 13;
                case 'Y': return 14;
                case 'P': return 15;
                case 'x': return 16;
                case 'K': return 17;
                case 'H': return 18;
                case 'w': return 19;
                case 'r': return 20;
                case 'R': return 21;
                case 'F': return 22;
                case 'd': return 23;
                case 'D': return 24;
                case '6': return 25;
                case 'V': return 26;
                case 'f': return 27;
                case 'S': return 28;
                case '2': return 29;
                case '3': return 30;
                case 'j': return 31;
                case 'y': return 32;
                case 's': return 33;
                case 'J': return 34;
                case '8': return 35;
                case 'g': return 36;
                case 'q': return 37;
                case 'n': return 38;
                case 'I': return 39;
                case 'E': return 40;
                case 'h': return 41;
                case 'o': return 42;
                case 'e': return 43;
                case 'v': return 44;
                case 'l': return 45;
                case 'U': return 46;
                case 'L': return 47;
            }
        }
        if (second == 3)
        {
            switch (casePar)
            {
                case 'S': return 0;
                case 'V': return 1;
                case '2': return 2;
                case 'n': return 3;
                case '8': return 4;
                case 'q': return 5;
                case 'v': return 6;
                case 'U': return 7;
                case 'R': return 8;
                case 'p': return 9;
                case 'E': return 10;
                case 'L': return 11;
                case 'o': return 12;
                case '0': return 13;
                case 'N': return 14;
                case 'K': return 15;
                case '4': return 16;
                case 'f': return 17;
                case 'X': return 18;
                case 'j': return 19;
                case 'J': return 20;
                case 'h': return 21;
                case 'P': return 22;
                case 'r': return 23;
                case 'i': return 24;
                case 'I': return 25;
                case 'F': return 26;
                case 'a': return 27;
                case '9': return 28;
                case 'u': return 29;
                case '5': return 30;
                case 'H': return 31;
                case 'k': return 32;
                case '3': return 33;
                case '7': return 34;
                case 'e': return 35;
                case 'D': return 36;
                case 'g': return 37;
                case 'M': return 38;
                case 'W': return 39;
                case 'l': return 40;
                case 'Y': return 41;
                case 'd': return 42;
                case '6': return 43;
                case '1': return 44;
                case 'w': return 45;
                case 'b': return 46;
                case 'x': return 47;
            }
        }
        if (second == 4)
        {
            switch (casePar)
            {
                case '9': return 0;
                case 'h': return 1;
                case 'c': return 2;
                case '7': return 3;
                case 'g': return 4;
                case 'k': return 5;
                case '2': return 6;
                case 'T': return 7;
                case 'F': return 8;
                case '3': return 9;
                case '6': return 10;
                case '4': return 11;
                case 'E': return 12;
                case 'o': return 13;
                case 'j': return 14;
                case 'f': return 15;
                case 'l': return 16;
                case 'b': return 17;
                case 'r': return 18;
                case 'p': return 19;
                case 'B': return 20;
                case 'd': return 21;
                case 'm': return 22;
                case 'V': return 23;
                case 'W': return 24;
                case 'Y': return 25;
                case 'G': return 26;
                case 'D': return 27;
                case 'P': return 28;
                case 'u': return 29;
                case '1': return 30;
                case 'K': return 31;
                case 'S': return 32;
                case 'X': return 33;
                case 'M': return 34;
                case 'O': return 35;
                case 'R': return 36;
                case 'i': return 37;
                case 't': return 38;
                case 'I': return 39;
                case 'e': return 40;
                case 'A': return 41;
                case 'n': return 42;
                case '5': return 43;
                case '0': return 44;
                case 'Q': return 45;
                case 'N': return 46;
                case 'v': return 47;
            }
        }
        if (second == 5)
        {
            switch (casePar)
            {
                case 'g': return 0;
                case '0': return 1;
                case 'p': return 2;
                case 'c': return 3;
                case 'd': return 4;
                case '6': return 5;
                case 'X': return 6;
                case 'Q': return 7;
                case '2': return 8;
                case 'L': return 9;
                case 'S': return 10;
                case 't': return 11;
                case 'G': return 12;
                case 's': return 13;
                case 'j': return 14;
                case '5': return 15;
                case 'a': return 16;
                case 'k': return 17;
                case 'U': return 18;
                case '9': return 19;
                case '7': return 20;
                case '8': return 21;
                case '4': return 22;
                case 'O': return 23;
                case 'P': return 24;
                case 'u': return 25;
                case 'o': return 26;
                case 'A': return 27;
                case 'R': return 28;
                case 'b': return 29;
                case 'f': return 30;
                case 'e': return 31;
                case 'D': return 32;
                case 'K': return 33;
                case 'Y': return 34;
                case 'I': return 35;
                case '3': return 36;
                case '1': return 37;
                case 'q': return 38;
                case 'w': return 39;
                case 'V': return 40;
                case 'i': return 41;
                case 'C': return 42;
                case 'n': return 43;
                case 'F': return 44;
                case 'J': return 45;
                case 'H': return 46;
                case 'B': return 47;
            }
        }
        if (second == 6)
        {
            switch (casePar)
            {
                case '6': return 0;
                case 'J': return 1;
                case 's': return 2;
                case '3': return 3;
                case 'f': return 4;
                case 'W': return 5;
                case '4': return 6;
                case '0': return 7;
                case 'y': return 8;
                case 'j': return 9;
                case 'G': return 10;
                case '5': return 11;
                case 'w': return 12;
                case 'D': return 13;
                case 'g': return 14;
                case 'S': return 15;
                case 'F': return 16;
                case 'L': return 17;
                case '7': return 18;
                case 'P': return 19;
                case 't': return 20;
                case 'H': return 21;
                case '8': return 22;
                case 'o': return 23;
                case 'N': return 24;
                case 'V': return 25;
                case 'R': return 26;
                case '1': return 27;
                case 'c': return 28;
                case 'Q': return 29;
                case 'A': return 30;
                case 'q': return 31;
                case 'Y': return 32;
                case 'i': return 33;
                case 'h': return 34;
                case 'r': return 35;
                case 'I': return 36;
                case '2': return 37;
                case 'n': return 38;
                case 'C': return 39;
                case 'O': return 40;
                case 'a': return 41;
                case 'B': return 42;
                case 'E': return 43;
                case 'M': return 44;
                case 'e': return 45;
                case 'u': return 46;
                case 'p': return 47;
            }
        }
        if (second == 7)
        {
            switch (casePar)
            {
                case '5': return 0;
                case 'c': return 1;
                case 'Y': return 2;
                case 'K': return 3;
                case 'N': return 4;
                case 'R': return 5;
                case 'S': return 6;
                case '1': return 7;
                case 'y': return 8;
                case '6': return 9;
                case 'h': return 10;
                case 'A': return 11;
                case '9': return 12;
                case 'F': return 13;
                case 'm': return 14;
                case 't': return 15;
                case 'L': return 16;
                case 'V': return 17;
                case 'r': return 18;
                case '4': return 19;
                case 'H': return 20;
                case 'd': return 21;
                case 'G': return 22;
                case 'W': return 23;
                case 'w': return 24;
                case 'U': return 25;
                case 'j': return 26;
                case 'E': return 27;
                case '3': return 28;
                case 'i': return 29;
                case 'X': return 30;
                case '2': return 31;
                case '7': return 32;
                case '8': return 33;
                case 'v': return 34;
                case 'B': return 35;
                case 'a': return 36;
                case 'I': return 37;
                case '0': return 38;
                case 'Q': return 39;
                case 'f': return 40;
                case 'b': return 41;
                case 'u': return 42;
                case 'l': return 43;
                case 'P': return 44;
                case 'D': return 45;
                case 'o': return 46;
                case 'O': return 47;
            }
        }
        if (second == 8)
        {
            switch (casePar)
            {
                case 'N': return 0;
                case 'y': return 1;
                case '6': return 2;
                case 'x': return 3;
                case 'r': return 4;
                case '1': return 5;
                case 'g': return 6;
                case '5': return 7;
                case 'M': return 8;
                case '8': return 9;
                case 'j': return 10;
                case '3': return 11;
                case 't': return 12;
                case 'a': return 13;
                case 'T': return 14;
                case 'E': return 15;
                case 'u': return 16;
                case 'o': return 17;
                case '7': return 18;
                case 'D': return 19;
                case 'R': return 20;
                case '4': return 21;
                case 'H': return 22;
                case 's': return 23;
                case 'm': return 24;
                case 'W': return 25;
                case 'L': return 26;
                case 'U': return 27;
                case '0': return 28;
                case 'V': return 29;
                case 'n': return 30;
                case 'v': return 31;
                case 'p': return 32;
                case 'q': return 33;
                case 'f': return 34;
                case 'Y': return 35;
                case 'X': return 36;
                case 'd': return 37;
                case 'K': return 38;
                case 'P': return 39;
                case 'I': return 40;
                case 'A': return 41;
                case 'l': return 42;
                case 'G': return 43;
                case '2': return 44;
                case 'w': return 45;
                case 'b': return 46;
                case 'J': return 47;
            }
        }
        if (second == 9)
        {
            switch (casePar)
            {
                case 'l': return 0;
                case 'd': return 1;
                case '0': return 2;
                case 'R': return 3;
                case 'B': return 4;
                case 'k': return 5;
                case 'a': return 6;
                case '7': return 7;
                case '4': return 8;
                case 'v': return 9;
                case '5': return 10;
                case 'q': return 11;
                case '2': return 12;
                case 'r': return 13;
                case '8': return 14;
                case 'c': return 15;
                case 'E': return 16;
                case 'J': return 17;
                case 's': return 18;
                case 'w': return 19;
                case 'h': return 20;
                case 'j': return 21;
                case '6': return 22;
                case 'S': return 23;
                case 'f': return 24;
                case 'X': return 25;
                case 'L': return 26;
                case 'W': return 27;
                case 'O': return 28;
                case 'K': return 29;
                case 'b': return 30;
                case 'Q': return 31;
                case 'm': return 32;
                case 'p': return 33;
                case 'V': return 34;
                case '3': return 35;
                case 'T': return 36;
                case 'D': return 37;
                case '1': return 38;
                case '9': return 39;
                case 't': return 40;
                case 'M': return 41;
                case 'H': return 42;
                case 'P': return 43;
                case 'N': return 44;
                case 'n': return 45;
                case 'Y': return 46;
                case 'g': return 47;
            }
        }
        if (second == 10)
        {
            switch (casePar)
            {
                case 'u': return 0;
                case '2': return 1;
                case 'l': return 2;
                case '8': return 3;
                case 'q': return 4;
                case 'e': return 5;
                case 'O': return 6;
                case '3': return 7;
                case 'T': return 8;
                case 'V': return 9;
                case 'F': return 10;
                case '7': return 11;
                case 'n': return 12;
                case '9': return 13;
                case 'w': return 14;
                case '0': return 15;
                case 'b': return 16;
                case 'm': return 17;
                case 's': return 18;
                case 'A': return 19;
                case '1': return 20;
                case 'U': return 21;
                case 'R': return 22;
                case 'I': return 23;
                case '5': return 24;
                case 'v': return 25;
                case 'Q': return 26;
                case 'W': return 27;
                case 'X': return 28;
                case 'C': return 29;
                case 'h': return 30;
                case '6': return 31;
                case 'H': return 32;
                case 'r': return 33;
                case 'Y': return 34;
                case 'M': return 35;
                case 'P': return 36;
                case 'g': return 37;
                case 'y': return 38;
                case '4': return 39;
                case 'E': return 40;
                case 'L': return 41;
                case 'S': return 42;
                case 'K': return 43;
                case 'f': return 44;
                case 'B': return 45;
                case 'J': return 46;
                case 'G': return 47;
            }
        }
        if (second == 11)
        {
            switch (casePar)
            {
                case '7': return 0;
                case '2': return 1;
                case 'p': return 2;
                case 'A': return 3;
                case 'r': return 4;
                case '4': return 5;
                case '0': return 6;
                case '8': return 7;
                case 'I': return 8;
                case 'd': return 9;
                case '3': return 10;
                case 'f': return 11;
                case 'F': return 12;
                case 'n': return 13;
                case 'Q': return 14;
                case 'x': return 15;
                case 'U': return 16;
                case 'v': return 17;
                case '9': return 18;
                case 'O': return 19;
                case 'c': return 20;
                case '6': return 21;
                case 'Y': return 22;
                case 'R': return 23;
                case 'o': return 24;
                case 'B': return 25;
                case 'E': return 26;
                case 'W': return 27;
                case '1': return 28;
                case 'S': return 29;
                case 'k': return 30;
                case 'D': return 31;
                case 'l': return 32;
                case 'q': return 33;
                case 'm': return 34;
                case 'i': return 35;
                case 'X': return 36;
                case 'T': return 37;
                case 'y': return 38;
                case 'g': return 39;
                case 'b': return 40;
                case 'P': return 41;
                case 'u': return 42;
                case 'j': return 43;
                case '5': return 44;
                case 'G': return 45;
                case 'K': return 46;
                case 'C': return 47;
            }
        }
        if (second == 12)
        {
            switch (casePar)
            {
                case 'N': return 0;
                case '1': return 1;
                case 'i': return 2;
                case 'K': return 3;
                case 'o': return 4;
                case '2': return 5;
                case 'r': return 6;
                case '6': return 7;
                case 'l': return 8;
                case 'd': return 9;
                case '3': return 10;
                case '8': return 11;
                case 'M': return 12;
                case 'w': return 13;
                case 'T': return 14;
                case '0': return 15;
                case '4': return 16;
                case 't': return 17;
                case '5': return 18;
                case 'f': return 19;
                case 'q': return 20;
                case '9': return 21;
                case 'n': return 22;
                case 'u': return 23;
                case 'y': return 24;
                case 'L': return 25;
                case 'b': return 26;
                case 'c': return 27;
                case 'G': return 28;
                case 'I': return 29;
                case 'H': return 30;
                case 'U': return 31;
                case '7': return 32;
                case 'J': return 33;
                case 'R': return 34;
                case 'v': return 35;
                case 'B': return 36;
                case 'a': return 37;
                case 'D': return 38;
                case 'x': return 39;
                case 'V': return 40;
                case 's': return 41;
                case 'F': return 42;
                case 'p': return 43;
                case 'm': return 44;
                case 'A': return 45;
                case 'j': return 46;
                case 'Y': return 47;
            }
        }
        if (second == 13)
        {
            switch (casePar)
            {
                case 'J': return 0;
                case 't': return 1;
                case 'V': return 2;
                case '9': return 3;
                case '5': return 4;
                case 'M': return 5;
                case '8': return 6;
                case 'Y': return 7;
                case 'B': return 8;
                case 'W': return 9;
                case 'U': return 10;
                case 'y': return 11;
                case '0': return 12;
                case 'e': return 13;
                case '6': return 14;
                case 'D': return 15;
                case 'x': return 16;
                case 'v': return 17;
                case '3': return 18;
                case 'K': return 19;
                case 'E': return 20;
                case 'p': return 21;
                case 'S': return 22;
                case 'w': return 23;
                case '4': return 24;
                case 'q': return 25;
                case 'N': return 26;
                case 'Q': return 27;
                case '7': return 28;
                case '2': return 29;
                case 'P': return 30;
                case 'C': return 31;
                case 'H': return 32;
                case 'k': return 33;
                case 'T': return 34;
                case 'j': return 35;
                case 'I': return 36;
                case 'a': return 37;
                case 'h': return 38;
                case 'G': return 39;
                case 'b': return 40;
                case 's': return 41;
                case 'o': return 42;
                case 'A': return 43;
                case 'n': return 44;
                case 'X': return 45;
                case '1': return 46;
                case 'F': return 47;
            }
        }
        if (second == 14)
        {
            switch (casePar)
            {
                case '4': return 0;
                case 'E': return 1;
                case '9': return 2;
                case 'G': return 3;
                case '0': return 4;
                case 'l': return 5;
                case 'D': return 6;
                case 'k': return 7;
                case 'Y': return 8;
                case 'o': return 9;
                case 'u': return 10;
                case 'p': return 11;
                case '8': return 12;
                case '1': return 13;
                case 'H': return 14;
                case '7': return 15;
                case '5': return 16;
                case 'T': return 17;
                case 'c': return 18;
                case '6': return 19;
                case 'q': return 20;
                case 'm': return 21;
                case '2': return 22;
                case 'h': return 23;
                case 'y': return 24;
                case 'J': return 25;
                case 'C': return 26;
                case 'S': return 27;
                case 'e': return 28;
                case 'W': return 29;
                case 'd': return 30;
                case 'v': return 31;
                case 'r': return 32;
                case 'L': return 33;
                case 'V': return 34;
                case 'b': return 35;
                case 'g': return 36;
                case 'K': return 37;
                case 'Q': return 38;
                case 'w': return 39;
                case 'j': return 40;
                case 'F': return 41;
                case 'B': return 42;
                case 'N': return 43;
                case 't': return 44;
                case 'X': return 45;
                case 'M': return 46;
                case 'U': return 47;
            }
        }
        if (second == 15)
        {
            switch (casePar)
            {
                case 'o': return 0;
                case 'R': return 1;
                case 'q': return 2;
                case 'g': return 3;
                case '7': return 4;
                case '8': return 5;
                case 'n': return 6;
                case 'd': return 7;
                case 'x': return 8;
                case 'A': return 9;
                case 's': return 10;
                case '4': return 11;
                case '6': return 12;
                case '9': return 13;
                case '5': return 14;
                case 'S': return 15;
                case '1': return 16;
                case 'H': return 17;
                case '0': return 18;
                case 'X': return 19;
                case 'u': return 20;
                case 'O': return 21;
                case 'Y': return 22;
                case 'e': return 23;
                case 'I': return 24;
                case 'E': return 25;
                case '3': return 26;
                case 'K': return 27;
                case 'L': return 28;
                case 'T': return 29;
                case 'l': return 30;
                case 'y': return 31;
                case '2': return 32;
                case 'Q': return 33;
                case 'm': return 34;
                case 'v': return 35;
                case 'b': return 36;
                case 'i': return 37;
                case 'F': return 38;
                case 'N': return 39;
                case 'k': return 40;
                case 't': return 41;
                case 'h': return 42;
                case 'j': return 43;
                case 'a': return 44;
                case 'J': return 45;
                case 'P': return 46;
                case 'B': return 47;
            }
        }
        if (second == 16)
        {
            switch (casePar)
            {
                case '5': return 0;
                case 'V': return 1;
                case 'j': return 2;
                case 'o': return 3;
                case '8': return 4;
                case '3': return 5;
                case 'P': return 6;
                case 'G': return 7;
                case '4': return 8;
                case 'A': return 9;
                case 'Q': return 10;
                case 'r': return 11;
                case '0': return 12;
                case '7': return 13;
                case 'I': return 14;
                case 'e': return 15;
                case 'c': return 16;
                case 'F': return 17;
                case 'J': return 18;
                case 'y': return 19;
                case 'X': return 20;
                case 'O': return 21;
                case 'L': return 22;
                case 'a': return 23;
                case 'g': return 24;
                case '1': return 25;
                case 'q': return 26;
                case 'R': return 27;
                case 'K': return 28;
                case 'C': return 29;
                case 'v': return 30;
                case 'E': return 31;
                case 'k': return 32;
                case 'U': return 33;
                case 't': return 34;
                case 's': return 35;
                case 'p': return 36;
                case '2': return 37;
                case 'T': return 38;
                case 'Y': return 39;
                case 'N': return 40;
                case 'u': return 41;
                case 'S': return 42;
                case 'h': return 43;
                case 'i': return 44;
                case 'b': return 45;
                case 'H': return 46;
                case '9': return 47;
            }
        }
        if (second == 17)
        {
            switch (casePar)
            {
                case 'j': return 0;
                case 'n': return 1;
                case '7': return 2;
                case 'B': return 3;
                case '6': return 4;
                case '1': return 5;
                case '4': return 6;
                case 'Q': return 7;
                case 'L': return 8;
                case 'E': return 9;
                case '5': return 10;
                case 'p': return 11;
                case 'U': return 12;
                case 'M': return 13;
                case 'I': return 14;
                case 'P': return 15;
                case '3': return 16;
                case '0': return 17;
                case '9': return 18;
                case 'H': return 19;
                case 's': return 20;
                case 'h': return 21;
                case 'v': return 22;
                case 'o': return 23;
                case 'x': return 24;
                case 'O': return 25;
                case 'k': return 26;
                case 'q': return 27;
                case '8': return 28;
                case 'y': return 29;
                case '2': return 30;
                case 'A': return 31;
                case 'r': return 32;
                case 'f': return 33;
                case 'K': return 34;
                case 'S': return 35;
                case 'N': return 36;
                case 'g': return 37;
                case 'd': return 38;
                case 'a': return 39;
                case 'c': return 40;
                case 'e': return 41;
                case 'l': return 42;
                case 'T': return 43;
                case 'F': return 44;
                case 'D': return 45;
                case 'R': return 46;
                case 't': return 47;
            }
        }
        if (second == 18)
        {
            switch (casePar)
            {
                case 'W': return 0;
                case 'c': return 1;
                case '2': return 2;
                case 'f': return 3;
                case '3': return 4;
                case 'm': return 5;
                case 'x': return 6;
                case 'A': return 7;
                case '9': return 8;
                case '7': return 9;
                case 'g': return 10;
                case 'Q': return 11;
                case 'l': return 12;
                case 'Y': return 13;
                case 'L': return 14;
                case 'P': return 15;
                case '8': return 16;
                case 'K': return 17;
                case 'V': return 18;
                case 'N': return 19;
                case 'M': return 20;
                case '6': return 21;
                case 'u': return 22;
                case 'q': return 23;
                case 'D': return 24;
                case '5': return 25;
                case 't': return 26;
                case 'e': return 27;
                case 'o': return 28;
                case 'w': return 29;
                case '4': return 30;
                case 'h': return 31;
                case 'E': return 32;
                case 'v': return 33;
                case 'k': return 34;
                case 'i': return 35;
                case 'U': return 36;
                case '0': return 37;
                case '1': return 38;
                case 'O': return 39;
                case 'n': return 40;
                case 'I': return 41;
                case 'p': return 42;
                case 'C': return 43;
                case 'b': return 44;
                case 's': return 45;
                case 'j': return 46;
                case 'r': return 47;
            }
        }
        if (second == 19)
        {
            switch (casePar)
            {
                case '2': return 0;
                case 'o': return 1;
                case '0': return 2;
                case '5': return 3;
                case 'a': return 4;
                case 'K': return 5;
                case '9': return 6;
                case 'b': return 7;
                case 'G': return 8;
                case 'I': return 9;
                case 'r': return 10;
                case 'H': return 11;
                case 'Q': return 12;
                case 'L': return 13;
                case 's': return 14;
                case 'P': return 15;
                case 'F': return 16;
                case 'M': return 17;
                case 'e': return 18;
                case 'B': return 19;
                case 'U': return 20;
                case '6': return 21;
                case 'u': return 22;
                case '4': return 23;
                case '8': return 24;
                case 'w': return 25;
                case 'N': return 26;
                case 'p': return 27;
                case 'v': return 28;
                case 'R': return 29;
                case 'O': return 30;
                case 't': return 31;
                case 'D': return 32;
                case 'k': return 33;
                case '3': return 34;
                case 'T': return 35;
                case 'Y': return 36;
                case 'c': return 37;
                case '1': return 38;
                case 'V': return 39;
                case 'g': return 40;
                case 'n': return 41;
                case '7': return 42;
                case 'W': return 43;
                case 'm': return 44;
                case 'J': return 45;
                case 'E': return 46;
                case 'd': return 47;
            }
        }
        if (second == 20)
        {
            switch (casePar)
            {
                case '3': return 0;
                case 'A': return 1;
                case 'C': return 2;
                case 'H': return 3;
                case 'v': return 4;
                case 'F': return 5;
                case 'T': return 6;
                case 'a': return 7;
                case 'w': return 8;
                case 'P': return 9;
                case 'y': return 10;
                case 'V': return 11;
                case 'D': return 12;
                case 'I': return 13;
                case 'J': return 14;
                case 'B': return 15;
                case '5': return 16;
                case '7': return 17;
                case 'L': return 18;
                case 'g': return 19;
                case '2': return 20;
                case 'S': return 21;
                case 'i': return 22;
                case 'o': return 23;
                case '9': return 24;
                case 'm': return 25;
                case 'j': return 26;
                case 'R': return 27;
                case 't': return 28;
                case '4': return 29;
                case '0': return 30;
                case 'l': return 31;
                case 'k': return 32;
                case 'G': return 33;
                case 'M': return 34;
                case '8': return 35;
                case 'b': return 36;
                case 'Y': return 37;
                case 'u': return 38;
                case 'f': return 39;
                case '1': return 40;
                case 'O': return 41;
                case 'c': return 42;
                case '6': return 43;
                case 'q': return 44;
                case 'x': return 45;
                case 'r': return 46;
                case 'h': return 47;
            }
        }
        if (second == 21)
        {
            switch (casePar)
            {
                case '2': return 0;
                case '9': return 1;
                case 'd': return 2;
                case 'T': return 3;
                case 'H': return 4;
                case 'j': return 5;
                case 'm': return 6;
                case 'x': return 7;
                case 'W': return 8;
                case 'B': return 9;
                case 'M': return 10;
                case 'f': return 11;
                case 'o': return 12;
                case 'c': return 13;
                case 'Q': return 14;
                case 'w': return 15;
                case 'I': return 16;
                case '6': return 17;
                case 'r': return 18;
                case 'N': return 19;
                case 'J': return 20;
                case 'C': return 21;
                case 'i': return 22;
                case '4': return 23;
                case '3': return 24;
                case '7': return 25;
                case 'U': return 26;
                case 'R': return 27;
                case 'O': return 28;
                case 'D': return 29;
                case '0': return 30;
                case '1': return 31;
                case 'Y': return 32;
                case 'L': return 33;
                case 'y': return 34;
                case 'E': return 35;
                case 'g': return 36;
                case 'S': return 37;
                case 't': return 38;
                case 'F': return 39;
                case '5': return 40;
                case 'u': return 41;
                case 'A': return 42;
                case '8': return 43;
                case 's': return 44;
                case 'V': return 45;
                case 'v': return 46;
                case 'k': return 47;
            }
        }
        if (second == 22)
        {
            switch (casePar)
            {
                case '6': return 0;
                case 'P': return 1;
                case 'I': return 2;
                case '7': return 3;
                case 'F': return 4;
                case 't': return 5;
                case 'u': return 6;
                case 'o': return 7;
                case 'n': return 8;
                case '1': return 9;
                case '9': return 10;
                case 'r': return 11;
                case 'k': return 12;
                case '0': return 13;
                case 'p': return 14;
                case 'l': return 15;
                case 'y': return 16;
                case 'R': return 17;
                case 'j': return 18;
                case '5': return 19;
                case '3': return 20;
                case 'b': return 21;
                case 'q': return 22;
                case 'i': return 23;
                case '8': return 24;
                case 'C': return 25;
                case '4': return 26;
                case 'E': return 27;
                case 'G': return 28;
                case 'X': return 29;
                case 'H': return 30;
                case 'Y': return 31;
                case 'S': return 32;
                case 'w': return 33;
                case '2': return 34;
                case 'Q': return 35;
                case 'd': return 36;
                case 'W': return 37;
                case 'a': return 38;
                case 'B': return 39;
                case 'g': return 40;
                case 'N': return 41;
                case 'h': return 42;
                case 'J': return 43;
                case 'U': return 44;
                case 'v': return 45;
                case 'f': return 46;
                case 'T': return 47;
            }
        }
        if (second == 23)
        {
            switch (casePar)
            {
                case '9': return 0;
                case 'k': return 1;
                case 'U': return 2;
                case 'i': return 3;
                case 'G': return 4;
                case 'l': return 5;
                case 'p': return 6;
                case 'y': return 7;
                case '5': return 8;
                case '6': return 9;
                case 'b': return 10;
                case 'q': return 11;
                case 't': return 12;
                case '4': return 13;
                case '7': return 14;
                case '2': return 15;
                case 'm': return 16;
                case 'g': return 17;
                case 'Q': return 18;
                case 'I': return 19;
                case 'h': return 20;
                case 'R': return 21;
                case 'M': return 22;
                case 'L': return 23;
                case 'S': return 24;
                case 'n': return 25;
                case 'X': return 26;
                case 'u': return 27;
                case 'T': return 28;
                case 'K': return 29;
                case 'x': return 30;
                case 'H': return 31;
                case 'E': return 32;
                case 'o': return 33;
                case '8': return 34;
                case 'Y': return 35;
                case 'V': return 36;
                case '0': return 37;
                case 'P': return 38;
                case 'B': return 39;
                case 'e': return 40;
                case 'O': return 41;
                case 'D': return 42;
                case '1': return 43;
                case 'f': return 44;
                case 'r': return 45;
                case 'W': return 46;
                case 'J': return 47;
            }
        }
        if (second == 24)
        {
            switch (casePar)
            {
                case 'p': return 0;
                case 'd': return 1;
                case 'r': return 2;
                case 'A': return 3;
                case '2': return 4;
                case '0': return 5;
                case 'C': return 6;
                case 's': return 7;
                case 'R': return 8;
                case 'D': return 9;
                case 'E': return 10;
                case 'x': return 11;
                case 'Y': return 12;
                case 'K': return 13;
                case 'b': return 14;
                case 'f': return 15;
                case '3': return 16;
                case '9': return 17;
                case 'Q': return 18;
                case 'X': return 19;
                case 'n': return 20;
                case 'l': return 21;
                case 'k': return 22;
                case 'g': return 23;
                case '4': return 24;
                case '1': return 25;
                case '5': return 26;
                case 'L': return 27;
                case 'w': return 28;
                case 'T': return 29;
                case 'v': return 30;
                case 'y': return 31;
                case 'c': return 32;
                case 'i': return 33;
                case 'P': return 34;
                case '6': return 35;
                case 'O': return 36;
                case 'u': return 37;
                case 'F': return 38;
                case '8': return 39;
                case 't': return 40;
                case 'h': return 41;
                case 'G': return 42;
                case 'U': return 43;
                case 'M': return 44;
                case 'a': return 45;
                case 'o': return 46;
                case '7': return 47;
            }
        }
        if (second == 25)
        {
            switch (casePar)
            {
                case 'n': return 0;
                case 'u': return 1;
                case 'S': return 2;
                case '5': return 3;
                case '4': return 4;
                case '3': return 5;
                case '1': return 6;
                case 'a': return 7;
                case 'm': return 8;
                case 'M': return 9;
                case '8': return 10;
                case 'R': return 11;
                case 'v': return 12;
                case '7': return 13;
                case 'L': return 14;
                case 'b': return 15;
                case 'P': return 16;
                case 'w': return 17;
                case 'J': return 18;
                case 'Y': return 19;
                case 'D': return 20;
                case '6': return 21;
                case 'x': return 22;
                case 'e': return 23;
                case 'r': return 24;
                case 'j': return 25;
                case 't': return 26;
                case 'G': return 27;
                case 'N': return 28;
                case 'A': return 29;
                case '9': return 30;
                case 'F': return 31;
                case '2': return 32;
                case 'E': return 33;
                case 'k': return 34;
                case 'X': return 35;
                case 'y': return 36;
                case 'K': return 37;
                case 'f': return 38;
                case 'U': return 39;
                case 'O': return 40;
                case 'H': return 41;
                case 'p': return 42;
                case 'V': return 43;
                case '0': return 44;
                case 'I': return 45;
                case 'W': return 46;
                case 'h': return 47;
            }
        }
        if (second == 26)
        {
            switch (casePar)
            {
                case 'C': return 0;
                case 'R': return 1;
                case 'u': return 2;
                case 'F': return 3;
                case '0': return 4;
                case 'c': return 5;
                case 'K': return 6;
                case 'M': return 7;
                case 'n': return 8;
                case 't': return 9;
                case 's': return 10;
                case 'H': return 11;
                case 'i': return 12;
                case 'h': return 13;
                case '2': return 14;
                case '1': return 15;
                case 'p': return 16;
                case '8': return 17;
                case 'a': return 18;
                case '3': return 19;
                case 'g': return 20;
                case 'r': return 21;
                case 'W': return 22;
                case '7': return 23;
                case 'q': return 24;
                case 'V': return 25;
                case 'x': return 26;
                case '6': return 27;
                case 'L': return 28;
                case 'Y': return 29;
                case '9': return 30;
                case 'B': return 31;
                case 'f': return 32;
                case 'Q': return 33;
                case 'y': return 34;
                case '4': return 35;
                case 'I': return 36;
                case 'G': return 37;
                case 'e': return 38;
                case '5': return 39;
                case 'N': return 40;
                case 'U': return 41;
                case 'k': return 42;
                case 'S': return 43;
                case 'D': return 44;
                case 'P': return 45;
                case 'O': return 46;
                case 'v': return 47;
            }
        }
        if (second == 27)
        {
            switch (casePar)
            {
                case 'O': return 0;
                case '6': return 1;
                case 'C': return 2;
                case 'k': return 3;
                case 'W': return 4;
                case 'j': return 5;
                case 'v': return 6;
                case '7': return 7;
                case 'T': return 8;
                case 'm': return 9;
                case 'b': return 10;
                case 'e': return 11;
                case 'h': return 12;
                case '3': return 13;
                case '8': return 14;
                case '0': return 15;
                case '4': return 16;
                case 'P': return 17;
                case 'f': return 18;
                case 'V': return 19;
                case 'x': return 20;
                case '1': return 21;
                case '5': return 22;
                case '9': return 23;
                case 'n': return 24;
                case 'F': return 25;
                case 'w': return 26;
                case 'd': return 27;
                case 'a': return 28;
                case 'A': return 29;
                case 'G': return 30;
                case 's': return 31;
                case 'E': return 32;
                case 'M': return 33;
                case 'N': return 34;
                case 'u': return 35;
                case 'r': return 36;
                case 'c': return 37;
                case 'J': return 38;
                case 'D': return 39;
                case '2': return 40;
                case 'q': return 41;
                case 'i': return 42;
                case 'U': return 43;
                case 'R': return 44;
                case 'g': return 45;
                case 'H': return 46;
                case 'K': return 47;
            }
        }
        if (second == 28)
        {
            switch (casePar)
            {
                case '4': return 0;
                case 'S': return 1;
                case 'n': return 2;
                case '7': return 3;
                case 'O': return 4;
                case 'U': return 5;
                case '3': return 6;
                case '2': return 7;
                case 'y': return 8;
                case 'Q': return 9;
                case 'e': return 10;
                case 'X': return 11;
                case 'L': return 12;
                case 'D': return 13;
                case 'V': return 14;
                case 'g': return 15;
                case 'T': return 16;
                case 'r': return 17;
                case 'h': return 18;
                case '6': return 19;
                case 'M': return 20;
                case 'K': return 21;
                case '5': return 22;
                case 'F': return 23;
                case '8': return 24;
                case '1': return 25;
                case 'j': return 26;
                case 'd': return 27;
                case 'x': return 28;
                case 'R': return 29;
                case 'H': return 30;
                case 'J': return 31;
                case 'q': return 32;
                case 'w': return 33;
                case 'v': return 34;
                case 'C': return 35;
                case '0': return 36;
                case 'm': return 37;
                case 'A': return 38;
                case 'l': return 39;
                case 'u': return 40;
                case 'o': return 41;
                case 'E': return 42;
                case '9': return 43;
                case 'a': return 44;
                case 'k': return 45;
                case 'Y': return 46;
                case 'b': return 47;
            }
        }
        if (second == 29)
        {
            switch (casePar)
            {
                case 'l': return 0;
                case 'L': return 1;
                case 'U': return 2;
                case 'p': return 3;
                case 'G': return 4;
                case 'g': return 5;
                case '9': return 6;
                case 'r': return 7;
                case 'W': return 8;
                case 'H': return 9;
                case 'N': return 10;
                case 'a': return 11;
                case 'R': return 12;
                case 'I': return 13;
                case '5': return 14;
                case 'h': return 15;
                case '8': return 16;
                case 'D': return 17;
                case 'j': return 18;
                case 'd': return 19;
                case 'J': return 20;
                case 'v': return 21;
                case 'u': return 22;
                case 'i': return 23;
                case '0': return 24;
                case 'Y': return 25;
                case 'F': return 26;
                case '3': return 27;
                case 'O': return 28;
                case 'n': return 29;
                case '4': return 30;
                case '1': return 31;
                case '7': return 32;
                case 'A': return 33;
                case 'y': return 34;
                case 'c': return 35;
                case 'V': return 36;
                case 'x': return 37;
                case 'X': return 38;
                case 'Q': return 39;
                case '2': return 40;
                case 'K': return 41;
                case 'M': return 42;
                case 't': return 43;
                case 'm': return 44;
                case 'C': return 45;
                case 'E': return 46;
                case '6': return 47;
            }
        }
        if (second == 30)
        {
            switch (casePar)
            {
                case 'f': return 0;
                case '3': return 1;
                case 'G': return 2;
                case 'i': return 3;
                case '7': return 4;
                case 'x': return 5;
                case 'n': return 6;
                case 'J': return 7;
                case '0': return 8;
                case 'O': return 9;
                case 'Q': return 10;
                case 'b': return 11;
                case '2': return 12;
                case 'v': return 13;
                case 'W': return 14;
                case 'C': return 15;
                case '6': return 16;
                case '1': return 17;
                case 'k': return 18;
                case 'U': return 19;
                case 'Y': return 20;
                case 'P': return 21;
                case 'V': return 22;
                case 'g': return 23;
                case 'c': return 24;
                case 'H': return 25;
                case '5': return 26;
                case '9': return 27;
                case '4': return 28;
                case 'd': return 29;
                case 'j': return 30;
                case 'e': return 31;
                case 'u': return 32;
                case 'w': return 33;
                case 'D': return 34;
                case 'A': return 35;
                case 'm': return 36;
                case 'p': return 37;
                case 'r': return 38;
                case '8': return 39;
                case 'L': return 40;
                case 'B': return 41;
                case 'X': return 42;
                case 'o': return 43;
                case 'l': return 44;
                case 'y': return 45;
                case 'R': return 46;
                case 'K': return 47;
            }
        }
        if (second == 31)
        {
            switch (casePar)
            {
                case '4': return 0;
                case '1': return 1;
                case '0': return 2;
                case 'o': return 3;
                case 'O': return 4;
                case 'D': return 5;
                case 'g': return 6;
                case 'f': return 7;
                case 'h': return 8;
                case 'R': return 9;
                case 'n': return 10;
                case 'p': return 11;
                case 'S': return 12;
                case 'a': return 13;
                case 'F': return 14;
                case 'J': return 15;
                case 'e': return 16;
                case 'l': return 17;
                case 'Q': return 18;
                case 'u': return 19;
                case 'j': return 20;
                case '8': return 21;
                case 'Y': return 22;
                case '7': return 23;
                case 'M': return 24;
                case 'A': return 25;
                case 's': return 26;
                case '6': return 27;
                case 'C': return 28;
                case 'I': return 29;
                case 'X': return 30;
                case '5': return 31;
                case 'b': return 32;
                case 'q': return 33;
                case 'y': return 34;
                case '3': return 35;
                case '9': return 36;
                case 'x': return 37;
                case 'T': return 38;
                case 'v': return 39;
                case 'H': return 40;
                case 'r': return 41;
                case 'm': return 42;
                case 'N': return 43;
                case 't': return 44;
                case 'K': return 45;
                case '2': return 46;
                case 'V': return 47;
            }
        }
        if (second == 32)
        {
            switch (casePar)
            {
                case 'g': return 0;
                case '9': return 1;
                case 'S': return 2;
                case 'M': return 3;
                case 'V': return 4;
                case '3': return 5;
                case 'n': return 6;
                case 'q': return 7;
                case 'X': return 8;
                case 'a': return 9;
                case '5': return 10;
                case 'v': return 11;
                case 'Y': return 12;
                case 'G': return 13;
                case '0': return 14;
                case 'e': return 15;
                case '1': return 16;
                case 'c': return 17;
                case 'f': return 18;
                case '7': return 19;
                case '8': return 20;
                case '2': return 21;
                case 'm': return 22;
                case '4': return 23;
                case 'p': return 24;
                case 'D': return 25;
                case 'O': return 26;
                case '6': return 27;
                case 'H': return 28;
                case 'Q': return 29;
                case 'x': return 30;
                case 'd': return 31;
                case 'h': return 32;
                case 'K': return 33;
                case 'L': return 34;
                case 'C': return 35;
                case 'I': return 36;
                case 'b': return 37;
                case 'J': return 38;
                case 'N': return 39;
                case 'i': return 40;
                case 'F': return 41;
                case 'T': return 42;
                case 'E': return 43;
                case 's': return 44;
                case 'U': return 45;
                case 'A': return 46;
                case 'k': return 47;
            }
        }
        if (second == 33)
        {
            switch (casePar)
            {
                case '8': return 0;
                case 'r': return 1;
                case 'p': return 2;
                case 'c': return 3;
                case 'B': return 4;
                case 'U': return 5;
                case 'x': return 6;
                case 'm': return 7;
                case 'k': return 8;
                case '6': return 9;
                case 'j': return 10;
                case 'A': return 11;
                case 'K': return 12;
                case '0': return 13;
                case '2': return 14;
                case 'J': return 15;
                case 's': return 16;
                case 'F': return 17;
                case 'h': return 18;
                case 'w': return 19;
                case 'l': return 20;
                case 'H': return 21;
                case 'N': return 22;
                case 'G': return 23;
                case 'I': return 24;
                case 'L': return 25;
                case '7': return 26;
                case 'f': return 27;
                case 'a': return 28;
                case 'R': return 29;
                case '5': return 30;
                case 'V': return 31;
                case 'b': return 32;
                case 'S': return 33;
                case 'Y': return 34;
                case 'e': return 35;
                case '1': return 36;
                case 'u': return 37;
                case 'n': return 38;
                case 'P': return 39;
                case 'g': return 40;
                case '4': return 41;
                case '3': return 42;
                case 'O': return 43;
                case 'q': return 44;
                case 'o': return 45;
                case '9': return 46;
                case 'W': return 47;
            }
        }
        if (second == 34)
        {
            switch (casePar)
            {
                case '4': return 0;
                case '8': return 1;
                case 'k': return 2;
                case 'N': return 3;
                case '1': return 4;
                case 'i': return 5;
                case '5': return 6;
                case 'u': return 7;
                case 's': return 8;
                case '7': return 9;
                case '6': return 10;
                case 'R': return 11;
                case 'h': return 12;
                case 'W': return 13;
                case 'G': return 14;
                case 't': return 15;
                case 'r': return 16;
                case 'j': return 17;
                case 'n': return 18;
                case 'v': return 19;
                case 'X': return 20;
                case 'O': return 21;
                case 'x': return 22;
                case '0': return 23;
                case 'H': return 24;
                case 'S': return 25;
                case 'p': return 26;
                case '3': return 27;
                case 'w': return 28;
                case '9': return 29;
                case 'B': return 30;
                case 'V': return 31;
                case 'L': return 32;
                case 'M': return 33;
                case 'd': return 34;
                case 'P': return 35;
                case 'Q': return 36;
                case 'f': return 37;
                case 'Y': return 38;
                case 'y': return 39;
                case 'I': return 40;
                case 'J': return 41;
                case 'A': return 42;
                case 'T': return 43;
                case 'l': return 44;
                case 'C': return 45;
                case 'o': return 46;
                case 'E': return 47;
            }
        }
        if (second == 35)
        {
            switch (casePar)
            {
                case 'P': return 0;
                case 's': return 1;
                case 'l': return 2;
                case '7': return 3;
                case 'u': return 4;
                case 'K': return 5;
                case '0': return 6;
                case '4': return 7;
                case '3': return 8;
                case 'E': return 9;
                case 'a': return 10;
                case 'S': return 11;
                case '2': return 12;
                case 'q': return 13;
                case 'v': return 14;
                case 'w': return 15;
                case 'X': return 16;
                case '6': return 17;
                case 'h': return 18;
                case 'y': return 19;
                case 'g': return 20;
                case 'n': return 21;
                case 'A': return 22;
                case '8': return 23;
                case 'p': return 24;
                case 'C': return 25;
                case 'N': return 26;
                case 'b': return 27;
                case 'H': return 28;
                case 'o': return 29;
                case '9': return 30;
                case 'I': return 31;
                case '1': return 32;
                case 'W': return 33;
                case 'D': return 34;
                case 'T': return 35;
                case 'B': return 36;
                case '5': return 37;
                case 'M': return 38;
                case 'd': return 39;
                case 'L': return 40;
                case 'c': return 41;
                case 'F': return 42;
                case 'i': return 43;
                case 'Q': return 44;
                case 'J': return 45;
                case 'k': return 46;
                case 'G': return 47;
            }
        }
        if (second == 36)
        {
            switch (casePar)
            {
                case '5': return 0;
                case 'Q': return 1;
                case '2': return 2;
                case 'J': return 3;
                case 'm': return 4;
                case '7': return 5;
                case 'G': return 6;
                case '6': return 7;
                case '9': return 8;
                case 'I': return 9;
                case 'w': return 10;
                case 'o': return 11;
                case 'n': return 12;
                case 'F': return 13;
                case 'd': return 14;
                case '4': return 15;
                case 'b': return 16;
                case '8': return 17;
                case '1': return 18;
                case 'P': return 19;
                case 'u': return 20;
                case 'l': return 21;
                case 'R': return 22;
                case 'D': return 23;
                case '3': return 24;
                case 'L': return 25;
                case 'N': return 26;
                case '0': return 27;
                case 'T': return 28;
                case 'f': return 29;
                case 'Y': return 30;
                case 'K': return 31;
                case 'v': return 32;
                case 'V': return 33;
                case 'y': return 34;
                case 'c': return 35;
                case 'O': return 36;
                case 'q': return 37;
                case 'H': return 38;
                case 'S': return 39;
                case 'B': return 40;
                case 'j': return 41;
                case 'i': return 42;
                case 'e': return 43;
                case 'x': return 44;
                case 'M': return 45;
                case 'r': return 46;
                case 'h': return 47;
            }
        }
        if (second == 37)
        {
            switch (casePar)
            {
                case 'a': return 0;
                case '8': return 1;
                case '9': return 2;
                case 'C': return 3;
                case 'h': return 4;
                case '7': return 5;
                case 'q': return 6;
                case 'B': return 7;
                case 'e': return 8;
                case 't': return 9;
                case 'N': return 10;
                case 's': return 11;
                case 'n': return 12;
                case 'r': return 13;
                case 'k': return 14;
                case 'i': return 15;
                case 'O': return 16;
                case '1': return 17;
                case 'x': return 18;
                case 'Y': return 19;
                case 'I': return 20;
                case 'v': return 21;
                case 'E': return 22;
                case 'y': return 23;
                case '4': return 24;
                case 'X': return 25;
                case 'V': return 26;
                case 'L': return 27;
                case '5': return 28;
                case 'p': return 29;
                case 'g': return 30;
                case 'l': return 31;
                case 'P': return 32;
                case 'G': return 33;
                case 'W': return 34;
                case '0': return 35;
                case 'o': return 36;
                case 'T': return 37;
                case 'J': return 38;
                case 'M': return 39;
                case '6': return 40;
                case 'S': return 41;
                case 'K': return 42;
                case 'm': return 43;
                case 'u': return 44;
                case '2': return 45;
                case 'w': return 46;
                case 'c': return 47;
            }
        }
        if (second == 38)
        {
            switch (casePar)
            {
                case 'W': return 0;
                case 'y': return 1;
                case 'L': return 2;
                case 'M': return 3;
                case 'x': return 4;
                case 'f': return 5;
                case 'k': return 6;
                case '6': return 7;
                case 'p': return 8;
                case 'v': return 9;
                case 's': return 10;
                case 'F': return 11;
                case 'S': return 12;
                case 'P': return 13;
                case 'm': return 14;
                case 'K': return 15;
                case 'Y': return 16;
                case '5': return 17;
                case 'J': return 18;
                case 'E': return 19;
                case 'I': return 20;
                case '2': return 21;
                case 'D': return 22;
                case '3': return 23;
                case '7': return 24;
                case 'O': return 25;
                case 'A': return 26;
                case '4': return 27;
                case 'G': return 28;
                case '9': return 29;
                case 'w': return 30;
                case 'R': return 31;
                case '0': return 32;
                case 'B': return 33;
                case 'r': return 34;
                case 'g': return 35;
                case '8': return 36;
                case 'U': return 37;
                case 'c': return 38;
                case 'o': return 39;
                case 'e': return 40;
                case 'C': return 41;
                case 'd': return 42;
                case 'N': return 43;
                case '1': return 44;
                case 'j': return 45;
                case 'u': return 46;
                case 'q': return 47;
            }
        }
        if (second == 39)
        {
            switch (casePar)
            {
                case 'R': return 0;
                case 'P': return 1;
                case 'O': return 2;
                case '2': return 3;
                case '0': return 4;
                case 'L': return 5;
                case 'S': return 6;
                case 'F': return 7;
                case 'G': return 8;
                case '8': return 9;
                case 'E': return 10;
                case 'o': return 11;
                case '1': return 12;
                case '7': return 13;
                case 'l': return 14;
                case '3': return 15;
                case 'c': return 16;
                case '9': return 17;
                case 'b': return 18;
                case 'f': return 19;
                case 'i': return 20;
                case '4': return 21;
                case '6': return 22;
                case 'u': return 23;
                case '5': return 24;
                case 'w': return 25;
                case 'y': return 26;
                case 's': return 27;
                case 'd': return 28;
                case 'W': return 29;
                case 'n': return 30;
                case 'h': return 31;
                case 'a': return 32;
                case 'C': return 33;
                case 't': return 34;
                case 'M': return 35;
                case 'N': return 36;
                case 'D': return 37;
                case 'I': return 38;
                case 'x': return 39;
                case 'A': return 40;
                case 'Y': return 41;
                case 'X': return 42;
                case 'k': return 43;
                case 'm': return 44;
                case 'q': return 45;
                case 'T': return 46;
                case 'e': return 47;
            }
        }
        if (second == 40)
        {
            switch (casePar)
            {
                case 'k': return 0;
                case '7': return 1;
                case '1': return 2;
                case 'I': return 3;
                case 'E': return 4;
                case '0': return 5;
                case 'Q': return 6;
                case 'j': return 7;
                case '9': return 8;
                case '5': return 9;
                case '4': return 10;
                case 'q': return 11;
                case 'r': return 12;
                case 'c': return 13;
                case 'C': return 14;
                case '6': return 15;
                case 'u': return 16;
                case 'O': return 17;
                case '3': return 18;
                case 'l': return 19;
                case 'T': return 20;
                case 'L': return 21;
                case '8': return 22;
                case 'p': return 23;
                case 'M': return 24;
                case 'h': return 25;
                case 'P': return 26;
                case 'A': return 27;
                case 'm': return 28;
                case 'd': return 29;
                case 'v': return 30;
                case 'H': return 31;
                case 's': return 32;
                case 'f': return 33;
                case 't': return 34;
                case 'K': return 35;
                case 'o': return 36;
                case 'X': return 37;
                case 'V': return 38;
                case 'x': return 39;
                case 'a': return 40;
                case 'B': return 41;
                case 'G': return 42;
                case 'J': return 43;
                case '2': return 44;
                case 'w': return 45;
                case 'U': return 46;
                case 'y': return 47;
            }
        }
        if (second == 41)
        {
            switch (casePar)
            {
                case '8': return 0;
                case 'T': return 1;
                case '9': return 2;
                case '7': return 3;
                case '1': return 4;
                case 'f': return 5;
                case 'O': return 6;
                case 'K': return 7;
                case 'U': return 8;
                case '2': return 9;
                case '6': return 10;
                case 'N': return 11;
                case '4': return 12;
                case 'w': return 13;
                case 'q': return 14;
                case '5': return 15;
                case 'u': return 16;
                case 'I': return 17;
                case 'd': return 18;
                case 'M': return 19;
                case 'Y': return 20;
                case 'D': return 21;
                case 'b': return 22;
                case '3': return 23;
                case 'i': return 24;
                case 'r': return 25;
                case 'v': return 26;
                case 'F': return 27;
                case 'E': return 28;
                case 'R': return 29;
                case 'P': return 30;
                case 'V': return 31;
                case 'e': return 32;
                case 'c': return 33;
                case 'X': return 34;
                case 'h': return 35;
                case 'S': return 36;
                case 'm': return 37;
                case 'L': return 38;
                case 'J': return 39;
                case 'G': return 40;
                case 'k': return 41;
                case 'x': return 42;
                case 'C': return 43;
                case 'p': return 44;
                case 't': return 45;
                case 'B': return 46;
                case 'W': return 47;
            }
        }
        if (second == 42)
        {
            switch (casePar)
            {
                case 'P': return 0;
                case '6': return 1;
                case '5': return 2;
                case 'i': return 3;
                case 'C': return 4;
                case 'e': return 5;
                case 'g': return 6;
                case 'y': return 7;
                case 'S': return 8;
                case '1': return 9;
                case 'w': return 10;
                case '9': return 11;
                case '3': return 12;
                case 'l': return 13;
                case '8': return 14;
                case 'f': return 15;
                case 'Y': return 16;
                case 'H': return 17;
                case '2': return 18;
                case 'M': return 19;
                case 'o': return 20;
                case 's': return 21;
                case 'n': return 22;
                case 'T': return 23;
                case 'b': return 24;
                case 'U': return 25;
                case '0': return 26;
                case 'c': return 27;
                case '7': return 28;
                case 'J': return 29;
                case 'R': return 30;
                case 'G': return 31;
                case 'W': return 32;
                case 'r': return 33;
                case 'E': return 34;
                case '4': return 35;
                case 'L': return 36;
                case 'D': return 37;
                case 'X': return 38;
                case 'N': return 39;
                case 'a': return 40;
                case 'h': return 41;
                case 'v': return 42;
                case 'A': return 43;
                case 'q': return 44;
                case 'O': return 45;
                case 'V': return 46;
                case 'm': return 47;
            }
        }
        if (second == 43)
        {
            switch (casePar)
            {
                case 'y': return 0;
                case 'r': return 1;
                case '8': return 2;
                case '4': return 3;
                case 'o': return 4;
                case 'x': return 5;
                case 'a': return 6;
                case '7': return 7;
                case 'J': return 8;
                case 'h': return 9;
                case 'L': return 10;
                case 'O': return 11;
                case '2': return 12;
                case 'X': return 13;
                case 'w': return 14;
                case 'q': return 15;
                case 'p': return 16;
                case '1': return 17;
                case '5': return 18;
                case '9': return 19;
                case 'Y': return 20;
                case 'c': return 21;
                case 's': return 22;
                case '6': return 23;
                case 'f': return 24;
                case 'j': return 25;
                case 'R': return 26;
                case 'i': return 27;
                case 'U': return 28;
                case '3': return 29;
                case 'P': return 30;
                case 'm': return 31;
                case 'D': return 32;
                case '0': return 33;
                case 'E': return 34;
                case 'K': return 35;
                case 'C': return 36;
                case 'e': return 37;
                case 'M': return 38;
                case 'v': return 39;
                case 'k': return 40;
                case 'l': return 41;
                case 'I': return 42;
                case 'u': return 43;
                case 'V': return 44;
                case 'S': return 45;
                case 'F': return 46;
                case 'W': return 47;
            }
        }
        if (second == 44)
        {
            switch (casePar)
            {
                case 'y': return 0;
                case 'J': return 1;
                case '3': return 2;
                case '9': return 3;
                case 'a': return 4;
                case 'I': return 5;
                case '8': return 6;
                case 'W': return 7;
                case 'p': return 8;
                case 'g': return 9;
                case 'q': return 10;
                case 'L': return 11;
                case 's': return 12;
                case 'w': return 13;
                case 'E': return 14;
                case 'o': return 15;
                case '1': return 16;
                case 'N': return 17;
                case 'B': return 18;
                case 'V': return 19;
                case 'A': return 20;
                case 'l': return 21;
                case 'D': return 22;
                case 'f': return 23;
                case 'X': return 24;
                case '0': return 25;
                case '6': return 26;
                case 'd': return 27;
                case 'k': return 28;
                case 'i': return 29;
                case 'G': return 30;
                case 'x': return 31;
                case 'Y': return 32;
                case '4': return 33;
                case 'H': return 34;
                case 'O': return 35;
                case 'e': return 36;
                case 't': return 37;
                case '5': return 38;
                case '7': return 39;
                case 'T': return 40;
                case 'C': return 41;
                case 'K': return 42;
                case 'Q': return 43;
                case 'n': return 44;
                case 'R': return 45;
                case 'j': return 46;
                case 'F': return 47;
            }
        }
        if (second == 45)
        {
            switch (casePar)
            {
                case '1': return 0;
                case 'I': return 1;
                case 'W': return 2;
                case 'm': return 3;
                case 'g': return 4;
                case '9': return 5;
                case 'u': return 6;
                case 'H': return 7;
                case 'U': return 8;
                case 'k': return 9;
                case 'R': return 10;
                case '3': return 11;
                case 'w': return 12;
                case 'N': return 13;
                case '8': return 14;
                case 'i': return 15;
                case 'O': return 16;
                case 'v': return 17;
                case '6': return 18;
                case 'P': return 19;
                case '4': return 20;
                case '5': return 21;
                case 'y': return 22;
                case 'o': return 23;
                case 'f': return 24;
                case 'S': return 25;
                case 'K': return 26;
                case 'Y': return 27;
                case 'A': return 28;
                case 't': return 29;
                case 'j': return 30;
                case 's': return 31;
                case 'C': return 32;
                case 'q': return 33;
                case 'J': return 34;
                case 'r': return 35;
                case '0': return 36;
                case 'E': return 37;
                case 'l': return 38;
                case 'p': return 39;
                case 'X': return 40;
                case 'V': return 41;
                case 'd': return 42;
                case 'L': return 43;
                case 'G': return 44;
                case '2': return 45;
                case 'D': return 46;
                case 'Q': return 47;
            }
        }
        if (second == 46)
        {
            switch (casePar)
            {
                case 'F': return 0;
                case 'N': return 1;
                case 's': return 2;
                case 'J': return 3;
                case 'a': return 4;
                case 'D': return 5;
                case 'u': return 6;
                case 'b': return 7;
                case '0': return 8;
                case '4': return 9;
                case 'U': return 10;
                case '5': return 11;
                case '7': return 12;
                case 'Q': return 13;
                case 'L': return 14;
                case 'G': return 15;
                case 'K': return 16;
                case 'h': return 17;
                case 'v': return 18;
                case '8': return 19;
                case 'n': return 20;
                case 't': return 21;
                case '1': return 22;
                case 'x': return 23;
                case 'C': return 24;
                case 'M': return 25;
                case 'w': return 26;
                case 'S': return 27;
                case 'i': return 28;
                case 'r': return 29;
                case 'E': return 30;
                case 'f': return 31;
                case '6': return 32;
                case 'I': return 33;
                case '3': return 34;
                case 'O': return 35;
                case 'A': return 36;
                case 'V': return 37;
                case 'B': return 38;
                case 'm': return 39;
                case 'j': return 40;
                case 'l': return 41;
                case '2': return 42;
                case 'y': return 43;
                case 'p': return 44;
                case '9': return 45;
                case 'P': return 46;
                case 'Y': return 47;
            }
        }
        if (second == 47)
        {
            switch (casePar)
            {
                case '3': return 0;
                case 'J': return 1;
                case 'y': return 2;
                case 'N': return 3;
                case 'L': return 4;
                case 'U': return 5;
                case '0': return 6;
                case 't': return 7;
                case '1': return 8;
                case 'F': return 9;
                case 'P': return 10;
                case '6': return 11;
                case 'h': return 12;
                case 'k': return 13;
                case '5': return 14;
                case 'l': return 15;
                case '7': return 16;
                case 'G': return 17;
                case 'd': return 18;
                case 'j': return 19;
                case 'o': return 20;
                case 'V': return 21;
                case 'f': return 22;
                case 'R': return 23;
                case 'T': return 24;
                case 'v': return 25;
                case 'A': return 26;
                case 'r': return 27;
                case '4': return 28;
                case 'S': return 29;
                case 'O': return 30;
                case 'w': return 31;
                case 'K': return 32;
                case 'x': return 33;
                case 'D': return 34;
                case 'm': return 35;
                case 'p': return 36;
                case 'W': return 37;
                case 'I': return 38;
                case 'E': return 39;
                case 'q': return 40;
                case 'C': return 41;
                case '2': return 42;
                case '8': return 43;
                case 'a': return 44;
                case '9': return 45;
                case 'i': return 46;
                case 'n': return 47;
            }
        }
        if (second == 48)
        {
            switch (casePar)
            {
                case 'T': return 0;
                case '2': return 1;
                case 'W': return 2;
                case '8': return 3;
                case 'F': return 4;
                case 'x': return 5;
                case '5': return 6;
                case '7': return 7;
                case '9': return 8;
                case 'f': return 9;
                case '4': return 10;
                case 'B': return 11;
                case 'V': return 12;
                case 'X': return 13;
                case 'E': return 14;
                case 'Q': return 15;
                case 'L': return 16;
                case 'C': return 17;
                case 'y': return 18;
                case 'o': return 19;
                case '0': return 20;
                case '3': return 21;
                case 'p': return 22;
                case 'u': return 23;
                case '6': return 24;
                case 'g': return 25;
                case 'k': return 26;
                case 'r': return 27;
                case 'A': return 28;
                case 'H': return 29;
                case 'P': return 30;
                case 'j': return 31;
                case 'O': return 32;
                case 'e': return 33;
                case 'S': return 34;
                case 'v': return 35;
                case 'J': return 36;
                case 'G': return 37;
                case 's': return 38;
                case 'N': return 39;
                case 'Y': return 40;
                case 'q': return 41;
                case 'M': return 42;
                case 'w': return 43;
                case 'b': return 44;
                case 'K': return 45;
                case 'i': return 46;
                case 'n': return 47;
            }
        }
        if (second == 49)
        {
            switch (casePar)
            {
                case 'j': return 0;
                case 'S': return 1;
                case 'l': return 2;
                case 'u': return 3;
                case '4': return 4;
                case 's': return 5;
                case 'o': return 6;
                case 'G': return 7;
                case 'C': return 8;
                case 'L': return 9;
                case 'h': return 10;
                case '0': return 11;
                case 'e': return 12;
                case 'M': return 13;
                case 'y': return 14;
                case 'P': return 15;
                case 'Y': return 16;
                case '1': return 17;
                case 'E': return 18;
                case '9': return 19;
                case 'f': return 20;
                case 'U': return 21;
                case 'W': return 22;
                case 'v': return 23;
                case 'N': return 24;
                case 'b': return 25;
                case '5': return 26;
                case '6': return 27;
                case '2': return 28;
                case '3': return 29;
                case '8': return 30;
                case 'F': return 31;
                case 'i': return 32;
                case 'g': return 33;
                case 'Q': return 34;
                case 't': return 35;
                case 'q': return 36;
                case 'c': return 37;
                case '7': return 38;
                case 'a': return 39;
                case 'w': return 40;
                case 'p': return 41;
                case 'R': return 42;
                case 'T': return 43;
                case 'n': return 44;
                case 'A': return 45;
                case 'x': return 46;
                case 'V': return 47;
            }
        }
        if (second == 50)
        {
            switch (casePar)
            {
                case '2': return 0;
                case '1': return 1;
                case 'l': return 2;
                case 'K': return 3;
                case 'y': return 4;
                case 'T': return 5;
                case '7': return 6;
                case 'H': return 7;
                case 'R': return 8;
                case '6': return 9;
                case 'w': return 10;
                case 't': return 11;
                case 'd': return 12;
                case '8': return 13;
                case 'h': return 14;
                case 'o': return 15;
                case 'p': return 16;
                case 'U': return 17;
                case 'J': return 18;
                case 'r': return 19;
                case 'S': return 20;
                case 'Y': return 21;
                case '3': return 22;
                case 'm': return 23;
                case '0': return 24;
                case 'b': return 25;
                case 'u': return 26;
                case 'v': return 27;
                case '5': return 28;
                case '9': return 29;
                case 'D': return 30;
                case 'A': return 31;
                case 'k': return 32;
                case 'P': return 33;
                case 'O': return 34;
                case 'n': return 35;
                case 'X': return 36;
                case 'e': return 37;
                case '4': return 38;
                case 'F': return 39;
                case 'W': return 40;
                case 'M': return 41;
                case 'q': return 42;
                case 'N': return 43;
                case 'f': return 44;
                case 'i': return 45;
                case 'E': return 46;
                case 'g': return 47;
            }
        }
        if (second == 51)
        {
            switch (casePar)
            {
                case 'y': return 0;
                case 'M': return 1;
                case '0': return 2;
                case '8': return 3;
                case 'w': return 4;
                case 'O': return 5;
                case '3': return 6;
                case 'e': return 7;
                case '9': return 8;
                case '5': return 9;
                case 'L': return 10;
                case 'K': return 11;
                case 'F': return 12;
                case 'a': return 13;
                case 'V': return 14;
                case 'C': return 15;
                case '4': return 16;
                case 'R': return 17;
                case 'q': return 18;
                case '2': return 19;
                case 'I': return 20;
                case 'G': return 21;
                case 'v': return 22;
                case '1': return 23;
                case 'D': return 24;
                case 'j': return 25;
                case 'o': return 26;
                case 'g': return 27;
                case 's': return 28;
                case 'h': return 29;
                case 'k': return 30;
                case '7': return 31;
                case 'E': return 32;
                case 'u': return 33;
                case 'b': return 34;
                case 'W': return 35;
                case 'x': return 36;
                case 'S': return 37;
                case 'T': return 38;
                case 'Y': return 39;
                case 'l': return 40;
                case 'i': return 41;
                case 'f': return 42;
                case 'J': return 43;
                case 'r': return 44;
                case 'U': return 45;
                case 'B': return 46;
                case 'Q': return 47;
            }
        }
        if (second == 52)
        {
            switch (casePar)
            {
                case '3': return 0;
                case 'B': return 1;
                case 'A': return 2;
                case 'K': return 3;
                case 'T': return 4;
                case 'o': return 5;
                case 'w': return 6;
                case 'Y': return 7;
                case 'd': return 8;
                case 'p': return 9;
                case '9': return 10;
                case 'h': return 11;
                case '6': return 12;
                case 'X': return 13;
                case 'j': return 14;
                case 'Q': return 15;
                case 'u': return 16;
                case 'r': return 17;
                case 'U': return 18;
                case 'L': return 19;
                case 'J': return 20;
                case '7': return 21;
                case 'k': return 22;
                case 'b': return 23;
                case '1': return 24;
                case 'F': return 25;
                case 'g': return 26;
                case 'E': return 27;
                case 'q': return 28;
                case 'e': return 29;
                case 'y': return 30;
                case '8': return 31;
                case 'C': return 32;
                case 'M': return 33;
                case '2': return 34;
                case '0': return 35;
                case 't': return 36;
                case 'f': return 37;
                case 'N': return 38;
                case 'O': return 39;
                case '5': return 40;
                case '4': return 41;
                case 'l': return 42;
                case 'G': return 43;
                case 'H': return 44;
                case 'n': return 45;
                case 'D': return 46;
                case 's': return 47;
            }
        }
        if (second == 53)
        {
            switch (casePar)
            {
                case 'N': return 0;
                case 'A': return 1;
                case '0': return 2;
                case 'i': return 3;
                case '4': return 4;
                case 't': return 5;
                case 'e': return 6;
                case 'T': return 7;
                case 'y': return 8;
                case 's': return 9;
                case '6': return 10;
                case 'B': return 11;
                case 'P': return 12;
                case 'V': return 13;
                case 'b': return 14;
                case 'a': return 15;
                case 'r': return 16;
                case 'W': return 17;
                case 'F': return 18;
                case 'j': return 19;
                case '2': return 20;
                case 'M': return 21;
                case '9': return 22;
                case 'q': return 23;
                case 'f': return 24;
                case 'O': return 25;
                case 'U': return 26;
                case 'm': return 27;
                case '8': return 28;
                case 'v': return 29;
                case 'D': return 30;
                case 'E': return 31;
                case '3': return 32;
                case 'R': return 33;
                case 'd': return 34;
                case 'g': return 35;
                case 'I': return 36;
                case 'c': return 37;
                case 'J': return 38;
                case 'u': return 39;
                case 'G': return 40;
                case 'w': return 41;
                case 'k': return 42;
                case 'S': return 43;
                case 'C': return 44;
                case '7': return 45;
                case 'p': return 46;
                case 'L': return 47;
            }
        }
        if (second == 54)
        {
            switch (casePar)
            {
                case 'u': return 0;
                case '3': return 1;
                case 'k': return 2;
                case 'V': return 3;
                case '0': return 4;
                case 'W': return 5;
                case 'l': return 6;
                case '8': return 7;
                case 'L': return 8;
                case '1': return 9;
                case 'R': return 10;
                case '2': return 11;
                case 'h': return 12;
                case 'Y': return 13;
                case 'D': return 14;
                case '6': return 15;
                case 'N': return 16;
                case 't': return 17;
                case 'A': return 18;
                case 'K': return 19;
                case 'p': return 20;
                case '7': return 21;
                case 'd': return 22;
                case 'M': return 23;
                case '5': return 24;
                case 'E': return 25;
                case 'J': return 26;
                case '9': return 27;
                case '4': return 28;
                case 'm': return 29;
                case 'n': return 30;
                case 'Q': return 31;
                case 'X': return 32;
                case 'i': return 33;
                case 'e': return 34;
                case 'S': return 35;
                case 'B': return 36;
                case 'H': return 37;
                case 'C': return 38;
                case 'a': return 39;
                case 'P': return 40;
                case 'j': return 41;
                case 'v': return 42;
                case 'c': return 43;
                case 'q': return 44;
                case 'G': return 45;
                case 's': return 46;
                case 'I': return 47;
            }
        }
        if (second == 55)
        {
            switch (casePar)
            {
                case 'A': return 0;
                case 'S': return 1;
                case '5': return 2;
                case 'i': return 3;
                case 'b': return 4;
                case 'f': return 5;
                case 'r': return 6;
                case 'o': return 7;
                case 'y': return 8;
                case 'C': return 9;
                case 'x': return 10;
                case '9': return 11;
                case '6': return 12;
                case 'l': return 13;
                case 'D': return 14;
                case '0': return 15;
                case '7': return 16;
                case 'v': return 17;
                case 'p': return 18;
                case 'O': return 19;
                case 'Y': return 20;
                case '1': return 21;
                case '4': return 22;
                case 'V': return 23;
                case 'q': return 24;
                case '8': return 25;
                case 'u': return 26;
                case '2': return 27;
                case 'j': return 28;
                case 'n': return 29;
                case 'm': return 30;
                case 's': return 31;
                case 'a': return 32;
                case 'T': return 33;
                case '3': return 34;
                case 'E': return 35;
                case 'U': return 36;
                case 'w': return 37;
                case 'F': return 38;
                case 'W': return 39;
                case 'H': return 40;
                case 'X': return 41;
                case 'B': return 42;
                case 'k': return 43;
                case 'c': return 44;
                case 'L': return 45;
                case 'M': return 46;
                case 'h': return 47;
            }
        }
        if (second == 56)
        {
            switch (casePar)
            {
                case '5': return 0;
                case '8': return 1;
                case 'S': return 2;
                case 'N': return 3;
                case '2': return 4;
                case '1': return 5;
                case 'q': return 6;
                case 'r': return 7;
                case 'B': return 8;
                case 'd': return 9;
                case 'X': return 10;
                case 'U': return 11;
                case '4': return 12;
                case 'g': return 13;
                case 'O': return 14;
                case 'Y': return 15;
                case '9': return 16;
                case '7': return 17;
                case 'h': return 18;
                case 'c': return 19;
                case 'T': return 20;
                case '6': return 21;
                case 'R': return 22;
                case 'a': return 23;
                case 'I': return 24;
                case 'n': return 25;
                case '3': return 26;
                case '0': return 27;
                case 'k': return 28;
                case 'o': return 29;
                case 'E': return 30;
                case 'F': return 31;
                case 't': return 32;
                case 'H': return 33;
                case 'l': return 34;
                case 'J': return 35;
                case 'u': return 36;
                case 'L': return 37;
                case 'M': return 38;
                case 'Q': return 39;
                case 'A': return 40;
                case 'K': return 41;
                case 'm': return 42;
                case 'p': return 43;
                case 'D': return 44;
                case 'f': return 45;
                case 'G': return 46;
                case 'P': return 47;
            }
        }
        if (second == 57)
        {
            switch (casePar)
            {
                case 'X': return 0;
                case 'T': return 1;
                case 'd': return 2;
                case 'm': return 3;
                case '6': return 4;
                case '7': return 5;
                case '3': return 6;
                case 'n': return 7;
                case 'O': return 8;
                case 'V': return 9;
                case 's': return 10;
                case 'e': return 11;
                case 'E': return 12;
                case 'H': return 13;
                case 'Y': return 14;
                case '8': return 15;
                case 'F': return 16;
                case 'l': return 17;
                case '9': return 18;
                case 'J': return 19;
                case 'G': return 20;
                case 'f': return 21;
                case '2': return 22;
                case 'K': return 23;
                case 'C': return 24;
                case '1': return 25;
                case 'g': return 26;
                case '0': return 27;
                case 'A': return 28;
                case 'M': return 29;
                case 't': return 30;
                case '5': return 31;
                case 'c': return 32;
                case 'Q': return 33;
                case 'h': return 34;
                case 'R': return 35;
                case 'r': return 36;
                case '4': return 37;
                case 'N': return 38;
                case 'W': return 39;
                case 'b': return 40;
                case 'o': return 41;
                case 'v': return 42;
                case 'j': return 43;
                case 'L': return 44;
                case 'u': return 45;
                case 'B': return 46;
                case 'S': return 47;
            }
        }
        if (second == 58)
        {
            switch (casePar)
            {
                case 'U': return 0;
                case 'f': return 1;
                case '5': return 2;
                case 'T': return 3;
                case 'c': return 4;
                case '7': return 5;
                case '0': return 6;
                case 'i': return 7;
                case 'I': return 8;
                case 'F': return 9;
                case 'j': return 10;
                case 'V': return 11;
                case 'w': return 12;
                case '3': return 13;
                case 'D': return 14;
                case 'u': return 15;
                case 'Q': return 16;
                case '6': return 17;
                case 'N': return 18;
                case '2': return 19;
                case '9': return 20;
                case 'P': return 21;
                case 'G': return 22;
                case 'b': return 23;
                case 'C': return 24;
                case '8': return 25;
                case '4': return 26;
                case '1': return 27;
                case 'o': return 28;
                case 'K': return 29;
                case 'A': return 30;
                case 'y': return 31;
                case 'R': return 32;
                case 'q': return 33;
                case 'X': return 34;
                case 'p': return 35;
                case 'h': return 36;
                case 'H': return 37;
                case 'M': return 38;
                case 'l': return 39;
                case 'm': return 40;
                case 'L': return 41;
                case 'x': return 42;
                case 'e': return 43;
                case 'k': return 44;
                case 'n': return 45;
                case 'Y': return 46;
                case 'E': return 47;
            }
        }
        if (second == 59)
        {
            switch (casePar)
            {
                case 'j': return 0;
                case 'W': return 1;
                case '9': return 2;
                case '0': return 3;
                case 'a': return 4;
                case '5': return 5;
                case 'w': return 6;
                case '7': return 7;
                case '1': return 8;
                case 'R': return 9;
                case '4': return 10;
                case 'B': return 11;
                case 'K': return 12;
                case '6': return 13;
                case '2': return 14;
                case 'I': return 15;
                case '3': return 16;
                case 'f': return 17;
                case 'l': return 18;
                case 'b': return 19;
                case 'F': return 20;
                case 'n': return 21;
                case 'J': return 22;
                case 'X': return 23;
                case 'D': return 24;
                case 'P': return 25;
                case 'G': return 26;
                case '8': return 27;
                case 'e': return 28;
                case 'x': return 29;
                case 'H': return 30;
                case 'Q': return 31;
                case 'N': return 32;
                case 'V': return 33;
                case 'h': return 34;
                case 'm': return 35;
                case 'O': return 36;
                case 'y': return 37;
                case 'L': return 38;
                case 'T': return 39;
                case 'Y': return 40;
                case 'S': return 41;
                case 'g': return 42;
                case 'i': return 43;
                case 'c': return 44;
                case 'p': return 45;
                case 'o': return 46;
                case 'q': return 47;
            }
        }
 
        return -1;
    }
    
}