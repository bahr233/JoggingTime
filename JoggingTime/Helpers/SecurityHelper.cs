using System.Security.Cryptography;
using System.Text;

namespace JoggingTime.Helpers
{
    public class SecurityHelper
    {
        private const string _salt = "Ac7k85rzat9tmmaFxDGmQgbrTgwvHJyt";
        private const string _alg = "HmacSHA256";
        public static int GetUserIDFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return 0;
            int userID = 0;
            try
            {
                string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                string[] parts = key.Split(new char[] { ':' });
                if (parts.Length >= 3)
                {
                    string hash = parts[0];
                    userID = int.Parse(parts[1]);
                    long ticks = long.Parse(parts[2]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userID;
        }
        public static string GetHashedPassword(string password, string salt = "")
        {
            if (string.IsNullOrEmpty(salt))
                salt = _salt;
            string key = string.Join(":", new string[] { password, salt });
            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(_salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));
                return Convert.ToBase64String(hmac.Hash);
            }
        }

        public static string GenerateToken(int userID)
        {

            string hash = string.Join(":", new string[] { userID.ToString(), Guid.NewGuid().ToString(), DateTime.Now.Ticks.ToString() });
            string hashLeft = "";
            string hashRight = "";
            using (HMAC hmac = HMACSHA256.Create("HmacSHA256"))
            {
                hmac.Key = Encoding.UTF8.GetBytes("ABC");
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                hashLeft = Convert.ToBase64String(hmac.Hash);
                //This is public part at access token to validate experation date.
                hashRight = string.Join(":", new string[] { userID.ToString(), DateTime.Now.AddDays(100).Ticks.ToString() });
            }
            string accessToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));


            return accessToken;
        }
        public static string Decrypt(string text, string salt = "")
        {
            if (!string.IsNullOrEmpty(salt))
                salt = _salt;
            string result;
            if (string.IsNullOrEmpty(text))
            {
                result = "";
            }
            else
            {
                UTF8Encoding uTF8Encoding = new UTF8Encoding();
                MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
                byte[] key = mD5CryptoServiceProvider.ComputeHash(uTF8Encoding.GetBytes(salt));
                TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
                tripleDESCryptoServiceProvider.Key = key;
                tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
                tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
                byte[] array = Convert.FromBase64String(text);
                byte[] bytes;
                try
                {
                    ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
                    bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
                }
                catch (Exception ex)
                {
                    bytes = null;
                }
                finally
                {
                    tripleDESCryptoServiceProvider.Clear();
                    mD5CryptoServiceProvider.Clear();
                }
                result = uTF8Encoding.GetString(bytes);
            }
            return result;
        }
    }
}
