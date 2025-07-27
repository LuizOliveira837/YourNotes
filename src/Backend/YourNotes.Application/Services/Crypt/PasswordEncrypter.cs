using System.Security.Cryptography;
using System.Text;

namespace YourNotes.Application.Services.Crypt
{
    public class PasswordEncrypter
    {
        private readonly string _secretKey;

        public PasswordEncrypter(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string Encrypter(string password)
        {


            var newPassword = $"{password}{_secretKey}";

            var newPasswordInBytes = Encoding.UTF8.GetBytes(newPassword);

            var newPasswordEncrypt = SHA512.HashData(newPasswordInBytes);

            return StringBytes(newPasswordEncrypt);


        }


        private static string StringBytes(byte[] newPasswordEncrypt)
        {
            var sb = new StringBuilder();

            foreach (byte b in newPasswordEncrypt)
            {

                var ex = b.ToString("x2");

                sb.Append(ex);

            }

            return sb.ToString();



        }

    }
}
