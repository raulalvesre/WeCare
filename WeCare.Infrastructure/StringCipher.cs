using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace WeCare.Infrastructure;

public class StringCipher
{
    private class Crypto
    {
        #region Rijndael Encryption

        /// <summary>
        /// Encrypt the given text and give the byte array back as a BASE64 string
        /// /// </summary>
        /// <param name="text" />The text to encrypt
        /// <param name="salt" />The pasword salt
        /// <returns>The encrypted text</returns>
        public string Encrypt(string text, string salt)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            var aesAlg = NewRijndaelManaged(salt);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(text);
            }

            return Convert.ToBase64String(msEncrypt.ToArray()) + "|" + salt;
        }

        /// <summary>
        /// Encrypt the given text and give the byte array back as a BASE64 string
        /// </summary>
        /// <param name="text" />The text to encrypt
        /// <returns>The encrypted text</returns>
        public string Encrypt(string text)
        {
            return Encrypt(text, Guid.NewGuid().ToString());
        }

        #endregion

        #region Rijndael Dycryption

        /// <summary>
        /// Checks if a string is base64 encoded
        /// </summary>
        /// <param name="base64String" />The base64 encoded string
        /// <returns>Base64 encoded stringt</returns>
        private bool IsBase64String(string base64String)
        {
            base64String = base64String.Trim();
            return (base64String.Length % 4 == 0) &&
                   Regex.IsMatch(base64String, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        /// <summary>
        /// Decrypts the given text
        /// </summary>
        /// <param name="cipherText" />The encrypted BASE64 text
        /// <param name="salt" />The pasword salt
        /// <returns>The decrypted text</returns>
        public string Decrypt(string cipherText, string salt)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            if (!IsBase64String(cipherText))
                throw new Exception("The cipherText input parameter is not base64 encoded");

            string text;

            var aesAlg = NewRijndaelManaged(salt);
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            var cipher = Convert.FromBase64String(cipherText);

            using (var msDecrypt = new MemoryStream(cipher))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        text = srDecrypt.ReadToEnd();
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Decrypts the given text
        /// </summary>
        /// <param name="cipherText" />The encrypted BASE64 text
        /// <returns>The decrypted text</returns>
        public string Decrypt(string cipherText)
        {
            var origHashedParts = cipherText.Split('|');
            if (origHashedParts.Count() != 2)
                throw new InvalidOperationException(nameof(cipherText));

            return Decrypt(origHashedParts[0], origHashedParts[1]);
        }

        #endregion

        #region NewRijndaelManaged

        /// <summary>
        /// Create a new RijndaelManaged class and initialize it
        /// </summary>
        /// <param name="salt" />The pasword salt
        /// <returns></returns>
        private RijndaelManaged NewRijndaelManaged(string salt)
        {
            if (salt == null)
                throw new ArgumentNullException("salt");
            var saltBytes = Encoding.ASCII.GetBytes(salt);
            var key = new Rfc2898DeriveBytes(salt, saltBytes);

            var aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

            return aesAlg;
        }

        #endregion
    }

    public static string Encrypt(string password)
    {
        return new Crypto().Encrypt(password);
    }

    /// <summary>
    /// Bypass Exception and return original value
    /// </summary>
    /// <param name="fullHash"></param>
    /// <returns></returns>
    public static string Decrypt(string fullHash)
    {
        try
        {
            return new Crypto().Decrypt(fullHash);
        }
        catch (Exception)
        {
            return fullHash;
        }
    }
}