using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common.Security.Ciphers
{
    public static class StringCipher
    {
        /// <summary>
        /// 
        /// </summary>
        public static string PassPhrase => "EKgbeMVUqtxXCgQCGig4";

        /// <summary>
        /// This constant is used to determine the keysize of the encryption algorithm in bits. 
        /// We divide this by 8 within the code below to get the equivalent number of bytes.
        /// </summary>
        private const int Keysize = 256;

        /// <summary>
        /// This constant determines the number of iterations for the password bytes generation function. 
        /// </summary>
        private const int DerivationIterations = 1000;

        /// <summary>
        /// Set the cipher mode to Cipher Block Chaining
        /// </summary>
        private static CipherMode CipherMode => CipherMode.CBC;

        /// <summary>
        /// Set the padding mode
        /// </summary>
        private static PaddingMode PaddingMode => PaddingMode.PKCS7;

        /// <summary>
        /// Generate 256 random bits
        /// </summary>
        /// <returns></returns>
        private static byte[] GenerateBitsOfRandomEntropy()
        {
            // 32 Bytes will give us 256 bits.
            var randomBytes = new byte[Keysize / 8];

            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }

            return randomBytes;
        }

        /// <summary>
        /// Encrypt plain text using a 256 bit keysize
        /// </summary>
        /// <param name="plainText">The string to encrypt</param>
        /// <param name="passPhrase">The pass phrase used to encrypt the plainText string</param>
        /// <returns></returns>
        /// <remarks>
        /// Salt - In cryptography, a salt is random data that is used as an additional input to a one-way function that "hashes" a password or passphrase. 
        /// Salts are closely related to the concept of nonce. The primary function of salts is to defend against dictionary attacks or against its hashed 
        /// equivalent, a pre-computed rainbow table attack.
        /// 
        /// IV - In cryptography, an initialization vector (IV) or starting variable (SV) is a fixed-size input to a cryptographic primitive that is typically 
        /// required to be random or pseudorandom.         
        /// </remarks>
        public static string Encrypt(string plainText, string passPhrase)
        {
            // Salt is randomly generated each time, but is preprended to encrypted cipher text so that the same Salt value can be used when decrypting.  
            var saltStringBytes = GenerateBitsOfRandomEntropy();

            // IV is randomly generated each time, but is preprended to encrypted cipher text so that the same IV value can be used when decrypting.  
            var ivStringBytes = GenerateBitsOfRandomEntropy();

            // Get the plain text as a byte array
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);

                using (var symmetricKey = new RijndaelManaged())
                {
                    // Set the block size to the keysize
                    symmetricKey.BlockSize = Keysize;

                    // Cipher Block Chaining
                    symmetricKey.Mode = CipherMode;

                    // Set the padding for the cipher
                    symmetricKey.Padding = PaddingMode;

                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                                cryptoStream.FlushFinalBlock();

                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();

                                memoryStream.Close();

                                cryptoStream.Close();

                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Decrypt cipher text using a 256 bit keysize
        /// </summary>
        /// <param name="cipherText">The string to decrypt</param>
        /// <param name="passPhrase">The pass phrase used to decrypt the cipherText string</param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [64 bytes of Salt] + [64 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);

            // Get the saltbytes by extracting the first 64 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();

            // Get the IV bytes by extracting the next 64 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();

            // Get the actual cipher text bytes by removing the first 128 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);

                using (var symmetricKey = new RijndaelManaged())
                {
                    // Set the block size to the keysize
                    symmetricKey.BlockSize = Keysize;

                    // Cipher Block Chaining
                    symmetricKey.Mode = CipherMode;

                    // Set the padding for the cipher
                    symmetricKey.Padding = PaddingMode;

                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];

                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                                memoryStream.Close();
                                cryptoStream.Close();

                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }
    }
}