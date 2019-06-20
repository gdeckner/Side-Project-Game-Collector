using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.Security
{
    public interface IPasswordHasher
    {
        string ComputeHash(string clearTextPassword, byte[] salt);


        byte[] GenerateRandomSalt();
    }
}
