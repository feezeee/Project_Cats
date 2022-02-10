﻿using System.Text;
using XSystem.Security.Cryptography;

namespace BLL.Services
{
    public class EncryptSHA256 : IEncryption
    {
        public string Encrypt(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
