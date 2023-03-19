using System.Security.Cryptography;
using System.Text;

namespace JoggingTime.Helpers
{
    public class SecurityHelper
    {
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
        public static string GetHashedPassword(string password)
        {

            string key = string.Join(":", new string[] { password, "ABC" });
            using (HMAC hmac = HMACSHA256.Create("HmacSHA256"))
            {
                hmac.Key = Encoding.UTF8.GetBytes("ABC");
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
    }
}
