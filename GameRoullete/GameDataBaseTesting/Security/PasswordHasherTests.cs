using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game_Collector.Security;


namespace GameDataBase.test.Security
{
    //Pulled directly from school lexture example
  [TestClass]
   public class PasswordHasherTests
    {
        private PasswordHasher hasher;
        [TestInitialize]
        public void Before()
        {
            hasher = new PasswordHasher();
        }
        [TestClass]
        public class ComputeHash : PasswordHasherTests
        {
            [TestMethod]
            public void The_hashed_password_is_48_characters_long()
            {
                byte[] salt = hasher.GenerateRandomSalt();
                string hashedPassword = hasher.ComputeHash("TESTPassword123", salt);

                Assert.AreEqual(108, hashedPassword.Length);
            }

            [TestMethod]
            public void A_password_longer_than_48_characters_is_hashed()
            {
                byte[] salt = hasher.GenerateRandomSalt();
                string hashedPassword = hasher.ComputeHash(new string('*', 109), salt);

                Assert.AreEqual(108, hashedPassword.Length);
            }

            [TestMethod]
            public void The_hash_is_calculated_the_same_for_a_given_password_and_salt()
            {
                byte[] salt = hasher.GenerateRandomSalt();
                string password = "A passphrase can sometimes be more secure...";
                string computedHash = hasher.ComputeHash(password, salt);

                string recomputedHash = hasher.ComputeHash(password, salt);

                Assert.AreEqual(computedHash, recomputedHash);
            }
        }
    }
}
