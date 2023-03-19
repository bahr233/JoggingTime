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
                if (parts.Length >= 4)
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
    }
}
