using common.library.Exception;
using System.Security.Cryptography;
using System.Text;

namespace common.library
{
    public static class Cipher
    {

        public static string Encryptv2(string TextToEncrypt, string key)
        {
            try
            {
                string encryptedText = "";
                MD5 md5 = new MD5CryptoServiceProvider();
                TripleDES des = new TripleDESCryptoServiceProvider();
                des.KeySize = 128;
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;

                byte[] md5Bytes = md5.ComputeHash(Encoding.Unicode.GetBytes(key));

                byte[] ivBytes = new byte[8];


                des.Key = md5Bytes;

                des.IV = ivBytes;

                byte[] clearBytes = Encoding.Unicode.GetBytes(TextToEncrypt);

                ICryptoTransform ct = des.CreateEncryptor();
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptedText = Convert.ToBase64String(ms.ToArray());
                }

                return encryptedText;
            }
            catch (EncryptedException)
            {
                throw new EncryptedException("Error occurred while encrypting");
            }
        }

        public static string Decryptv2(string TextTodecrypt, string key, bool replaceSpace = true)
        {
            try
            {
                string decryptedText = "";
                MD5 md5 = new MD5CryptoServiceProvider();
                TripleDES des = new TripleDESCryptoServiceProvider();
                des.KeySize = 128;
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;

                byte[] md5Bytes = md5.ComputeHash(Encoding.Unicode.GetBytes(key));

                byte[] ivBytes = new byte[8];


                des.Key = md5Bytes;

                des.IV = ivBytes;

                byte[] clearBytes = Convert.FromBase64String(TextTodecrypt);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    decryptedText = UTF8Encoding.UTF8.GetString(ms.ToArray());
                }
                if (!replaceSpace)
                {
                    return decryptedText.Replace("\0", "");
                }
                return decryptedText.Replace("\0", "").Replace(" ", "");
            }
            catch (EncryptedException ex)
            {
                throw new EncryptedException("Invalid encryption details");
            }
        }


        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static string SHA512(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }

        public static string CreateBassToken(string clientSecret, string clientKey)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes($"{clientKey}:{clientSecret}");
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}