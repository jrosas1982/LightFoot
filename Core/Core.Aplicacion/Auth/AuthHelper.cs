using System.Linq;
using System.Security.Cryptography;

namespace Core.Aplicacion.Auth
{
    public static class AuthHelper
    {
        public static string GenerateRandomPassword(int length)
        {
            return new string(Enumerable.Repeat("ABCDEFGHJKLMNPQRSTUVWXYZ123456789", length).Select(s => s[RNGCryptoServiceProvider.GetInt32(s.Length)]).ToArray());
        }


    }
}
