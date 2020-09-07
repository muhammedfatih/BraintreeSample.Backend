using System;

namespace BraintreeSample.APIHelper.Builders
{
    public class PasswordBuilder
    {
        public string Encrpyt(string inputString)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(inputString);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
        }
        public string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
