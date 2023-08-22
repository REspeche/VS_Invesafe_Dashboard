using System;
using System.Linq;
using System.Text;

namespace BusinessLayer.Classes
{
    public class Code
    {
        private const int _expirationMinutes = 10; //session timeout = 10 minutes

        public static string GenerateCode(long ticks)
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateCode(ticks);
            }
            return string.Join(":", r, ticks.ToString());
        }

        public static bool IsCodeValid(string fullCode, string userCode)
        {
            bool result = false;
            try
            {
                // Split the parts.
                string[] parts = fullCode.Split(new char[] { ':' });
                if (parts.Length == 2)
                {
                    // Get the code, and timestamp.
                    string code = parts[0];
                    long ticks = long.Parse(parts[1]);
                    DateTime timeStamp = new DateTime(ticks);
                    // Ensure the timestamp is valid.
                    bool expired = Math.Abs((DateTime.UtcNow - timeStamp).TotalMinutes) > _expirationMinutes;
                    if (!expired) return (userCode == null || code == userCode) ? true : false;
                }
            }
            catch
            {
            }
            return result;
        }
    }
}
