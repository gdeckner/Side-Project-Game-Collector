using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Game_Collector.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string ComputeHash(string password, byte[] salt)
        {
            throw new NotImplementedException();
        }

        public byte[] GenerateRandomSalt()
        {
            throw new NotImplementedException();
        }
    }
}
