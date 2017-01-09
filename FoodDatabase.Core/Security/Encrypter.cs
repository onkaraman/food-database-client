using System;
using System.Text;
using FoodDatabase.Core.Patterns;

namespace FoodDatabase.Core.Security
{
    /// <summary>
    /// Simple class to encrypt credentials.
    /// Not final in this current condition!
    /// </summary>
    public class Encrypter : LazyStatic<Encrypter>
	{
        public Encrypter() {}

		/// <summary>
		/// Encrypts the given credentials to a simple base-64 string.
		/// </summary>
		/// <param name="content">Content to encrypt.</param>
        public string Encrypt(string content)
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            return Convert.ToBase64String(bytes);
        }

		/// <summary>
		/// Descrypts given base-64 content.
		/// </summary>
		/// <param name="b64">Encoded content</param>
        public string Decrypt(string b64)
        {
            var data = Convert.FromBase64String(b64);
            var str = Encoding.UTF8.GetString(data, 0, data.Length);
            return str;
        }
	}
}

