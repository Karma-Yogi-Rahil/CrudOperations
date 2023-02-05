using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace CrudOperations.Encryption
{
    public class PasswordEncryption
    {
        /// <summary>
        /// Encrypts the user enter password. 
        /// </summary>
        /// <param name="password">User entered password.</param>
        /// <param name="saltCode">Salt code.</param>
        /// <returns>Hash code</returns>
        public string Encrypt(string password, out string saltCode)
        {

            saltCode = GenrateSaltCode();
            return GenrateHashCode(password, saltCode);
        }

        /// <summary>
        /// Generates a hash code based on the password.
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="saltCode">Salt code</param>
        /// <returns>Hash code of 256 bit length.</returns>
        private string GenrateHashCode(string password, string saltCode)
        {
            try
            {
                password += saltCode;

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();

                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash - returns byte array  
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                    // Generates a hash code of length 256
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                }

                return builder.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Generates a random salt code . 
        /// </summary>
        /// <returns>Salt code with length 6 bits</returns>
       public static string GenrateSaltCode()
        {
            try
            {
                string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%";
                StringBuilder output = new StringBuilder();
                Random random = new Random();

                // Generates the slat code of 6 bit.
                for (int i = 0; i < 6; i++)
                {
                    output.Append(chars[random.Next(chars.Length)]);
                }

                return output.ToString();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
