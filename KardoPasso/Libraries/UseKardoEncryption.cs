using System;

public static class UseKardoEncryption
{
    public static string getEncryptedString(string submitted) {
        KardoEncryptionASCII kardoEn = new KardoEncryptionASCII(submitted, "encrypt");
        return kardoEn.getEncryptedString();
    }
    public static string getEncryptedString(string submitted, int minChar)
    {
        KardoEncryptionASCII kardoEn = new KardoEncryptionASCII(submitted, "encrypt", minChar);
        return kardoEn.getEncryptedString();
    }
    public static string getEncryptedString(int submitted)
    {
        KardoEncryptionASCII kardoEn = new KardoEncryptionASCII(submitted.ToString(), "encrypt");
        return kardoEn.getEncryptedString();
    }
    public static string getEncryptedString(int submitted, int minChar)
    {
        KardoEncryptionASCII kardoEn = new KardoEncryptionASCII(submitted.ToString(), "encrypt", minChar);
        return kardoEn.getEncryptedString();
    }
    public static string getDecipherString(string submitted)
    {
        if (submitted != null && submitted != "")
        {
            KardoEncryptionASCII kardoEn = new KardoEncryptionASCII(submitted, "decipher");
            return kardoEn.getDecipherString();
        }
        return "";
    }
}