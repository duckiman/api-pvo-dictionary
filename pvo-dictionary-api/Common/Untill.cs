using System;

namespace pvo_dictionary_api.Common
{
    public class Untill
    {
        /// <summary>
        /// Information encryption
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
        }
    }
}
